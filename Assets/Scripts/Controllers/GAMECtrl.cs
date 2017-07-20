using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GAMECtrl : MonoBehaviour {
	public static GAMECtrl instance;
	public float restartDelay, maxTime, timeLeft;
	public GameData data;
	public int coinValue, enemyValue, bigCoinValue;
	public UI UI;
	public GameObject bigCoin;

	public enum Item
	{
		Coin,
		BigCoin,
		Enemy
	}



	string dataFilePath;
	BinaryFormatter bf;
	void Awake(){
		if (instance == null) {
			instance = this;
		}
		bf = new BinaryFormatter();
		dataFilePath = Application.persistentDataPath + "/game.dat";

		Debug.Log (dataFilePath);
	}
	// Use this for initialization
	void Start () {
		timeLeft = maxTime;
		HandleFirstBoot ();
		UpdateHearts ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			ResetData ();
		}

		if (timeLeft >= 0) {
			UpdateTimer ();
		}


	}
	public void SaveData(){
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close ();
	}



	public void LoadData(){
		if(File.Exists(dataFilePath)){ 
			FileStream fs = new FileStream (dataFilePath, FileMode.Open);
			data = (GameData) bf.Deserialize (fs);
			UI.txtCoinCount.text = "X " + data.coinCount;
			UI.txtScore.text = "Score: " + data.score;
			Debug.Log ("NUM OF COINS" + data.coinCount);
			fs.Close();
		}
	}

	void OnEnable(){
		Debug.Log ("Loading");
		LoadData ();

	}

	void OnDisable(){
		Debug.Log ("Saving");
		SaveData ();
	}

	void ResetData(){
	FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		data.coinCount = 0;
		data.score = 0;
		timeLeft = maxTime;
		data.lives = 3;
		UpdateHearts ();
		for (int keyNumber = 0; keyNumber <= 2; keyNumber++) {
			data.keyFound [keyNumber] = false;
		}
		bf.Serialize (fs, data);
		UI.txtCoinCount.text = "X " + data.coinCount;
		UI.txtScore.text = "Score: " + data.score;
		fs.Close ();
		Debug.Log ("Reset");
	}
	/// <summary>
	/// Restarts the level when player dies.
	/// </summary>
	public void PlayerDied(GameObject player){
		
		player.SetActive (false);
		CheckLives ();
		//Invoke ("RestartLevel", restartDelay);
	}
	public void PlayerDiedAnimation(GameObject player){
		Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();
		rb.AddForce (new Vector2 (-150f, 400f));

		player.transform.Rotate (new Vector3 (0, 0, 45f));
		player.GetComponent<PlayerCtrl> ().enabled = false;
		foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>()) {
			c2d.enabled = false;
		}

		foreach (Transform child in player.transform) {
			child.gameObject.SetActive (false);
		}

		Camera.main.GetComponent<CameraCtrl> ().enabled = false;
		rb.velocity = Vector2.zero;
		StartCoroutine ("PauseBeforeReload",player);

	}
	IEnumerator PauseBeforeReload(GameObject player){
		yield return new WaitForSeconds (1.5f);
		PlayerDied (player);
	}
	public void PlayerDrowned(GameObject player){
		CheckLives ();
		//Invoke ("RestartLevel", restartDelay);
	}

	public void UpdateCointCount(){
		data.coinCount += 1;
		UI.txtCoinCount.text = "X " + data.coinCount;
	}

	public void UpdateScore(Item item){
		int itemValue = 0;
		switch (item) {
		case Item.BigCoin:
			itemValue = bigCoinValue;
			break;
		case Item.Coin:
			itemValue = coinValue;
			break;
		case Item.Enemy:
			itemValue = enemyValue;
			break;
		default:
			break;
		}
		data.score += itemValue;
		UI.txtScore.text = "Score: " + data.score;
	}

	public void BulletHitEnemy(Transform enemy){
		Vector3 pos = enemy.position;
		pos.z = 20f;
		SFXCtrl.instance.EnemyExplosion (pos);
		Instantiate (bigCoin, pos, Quaternion.identity);
		Destroy (enemy.gameObject);


	}

	public void UpdateKeyCount(int keyNumber){
		
		data.keyFound [keyNumber] = true;
		if (keyNumber == 0)
			UI.key0.sprite = UI.keyFull0;
		else if(keyNumber == 1)
			UI.key1.sprite = UI.keyFull1;
		else if(keyNumber == 2)
			UI.key2.sprite = UI.keyFull2;
	}

	public void RestartLevel(){
		SceneManager.LoadScene ("Gameplay");
	}

	public void PlayerStompEnemy(GameObject enemy){
		enemy.tag = "Untagged";
		Destroy (enemy);
		UpdateScore (Item.Enemy);
	}

	void UpdateTimer(){
		timeLeft -= Time.deltaTime;
		UI.txtTimer.text = "Timer: " + (int)timeLeft;

		if (timeLeft <= 0) {
			UI.txtTimer.text = "Timer: 0";
			GameObject player = GameObject.Find ("Cat");
			PlayerDied (player);
		}
	}
	void HandleFirstBoot(){
		if (data.isFirstBoot) {
			data.lives = 3;
			data.coinCount = 0;
			data.score = 0;
			data.keyFound [0] = false;
			data.keyFound [1] = false;
			data.keyFound [2] = false;
			data.isFirstBoot = false;
			UI.txtCoinCount.text = "X " + data.coinCount;
			UI.txtScore.text = "Score: " + data.score;
		}
	}

	void UpdateHearts(){
		if (data.lives == 3) {
			UI.heart0.sprite = UI.heartFull;
			UI.heart1.sprite = UI.heartFull;
			UI.heart2.sprite = UI.heartFull;
		}
		if (data.lives == 2)
			UI.heart0.sprite = UI.heartEmpty;
		else if (data.lives == 1) {
			UI.heart1.sprite = UI.heartEmpty;
			UI.heart0.sprite = UI.heartEmpty;
		} else if (data.lives == 0) {
			UI.heart2.sprite = UI.heartEmpty;
		}
	}

	void CheckLives(){
		int updatedLives = data.lives;
		updatedLives -= 1;
		data.lives = updatedLives;

		if (data.lives == 0) {
			Invoke ("GameOver", restartDelay);
		} else {
			SaveData ();
			Invoke ("RestartLevel", restartDelay);
		}
	}

	void GameOver(){
		UI.panelGameOver.SetActive (true);
		data.isFirstBoot = true;
		SaveData ();
	}


}
