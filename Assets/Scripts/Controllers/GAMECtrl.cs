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
	[HideInInspector]
	public GameData data;
	public int coinValue, enemyValue, bigCoinValue, bossValue;
	public UI UI;
	public GameObject bigCoin, bigStar, player, signPlatform, levelCompleteMenu, pausePanel, mobileUI;
	public AudioClip bossBattleMusic;

	public enum Item
	{
		Coin,
		BigCoin,
		Enemy,
		Boss
	}

	bool allKeysFound, timerOn, isPaused;

	string dataFilePath;
	BinaryFormatter bf;
	void Awake(){
		if (instance == null) {
			instance = this;
		}
		bf = new BinaryFormatter();
		dataFilePath = Application.persistentDataPath + "/game.dat";

		Debug.Log (dataFilePath);
		for(int i = 0; i < data.keyFound.Length; i++){
			data.keyFound [i] = false;
		}
	}
	// Use this for initialization
	void Start () {
		DataCtrl.instance.RefreshData ();
		data = DataCtrl.instance.data;
		RefreshUI ();
		timeLeft = maxTime;
		HandleFirstBoot ();
		UpdateHearts ();
		signPlatform.SetActive (false);
		allKeysFound = false;
		timerOn = true;
		isPaused = false;
	//	data.score = 1000;
	//	RefreshUI ();
	//	LevelComplete ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			ResetData ();
		}

		if (timeLeft >= 0 && timerOn) {
			UpdateTimer ();
		}
		if (isPaused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}

	}
	public void SaveData(){
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close ();
	}



	public void RefreshUI(){
		UI.txtCoinCount.text = "X " + data.coinCount;
		UI.txtScore.text = "Score: " + data.score;
	}

	void OnEnable(){
		//Debug.Log ("Loading");
		RefreshUI ();

	}

	void OnDisable(){
		//Debug.Log ("Saving");
		DataCtrl.instance.SaveData(data);
		Time.timeScale = 1;
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
		foreach (LevelData level in data.levelData) {
			level.numOfStars = 0;
			if(level.levelNum != 1){
				level.isUnlocked = false;
			}
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
		SpriteRenderer sr = player.GetComponent<SpriteRenderer> ();
		rb.AddForce (new Vector2 (-150f, 400f));

		player.transform.Rotate (new Vector3 (0, 0, 45f));
		player.GetComponent<PlayerCtrl> ().enabled = false;
		foreach (Collider2D c2d in player.transform.GetComponents<Collider2D>()) {
			c2d.enabled = false;
		}

		foreach (Transform child in player.transform) {
			child.gameObject.SetActive (false);
		}
		sr.sortingOrder = 10;
		AudioCtrl.instance.PLayerDied (player.gameObject.transform.position);
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
		case Item.Boss:
			itemValue = bossValue;
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

	public void BossIsKilled(Transform enemy){
		Vector3 pos = enemy.position;
		pos.z = 20f;
		SFXCtrl.instance.EnemyExplosion (pos);
		Instantiate (bigStar, pos, Quaternion.identity);
		Destroy (enemy.gameObject);
		UpdateScore (Item.Boss);
		timerOn = false;
	}

	public void UpdateKeyCount(int keyNumber){
		allKeysFound = true;

		data.keyFound [keyNumber] = true;
		if (keyNumber == 0)
			UI.key0.sprite = UI.keyFull0;
		else if(keyNumber == 1)
			UI.key1.sprite = UI.keyFull1;
		else if(keyNumber == 2)
			UI.key2.sprite = UI.keyFull2;

		foreach(bool found in data.keyFound){
			if (!found) {
				allKeysFound = false;
				break;
			}
		}
		if (allKeysFound) {
			signPlatform.SetActive (true);
		}
	}

	public void RestartLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void PlayerStompEnemy(GameObject enemy){
		enemy.tag = "Untagged";
		Destroy (enemy);
		UpdateScore (Item.Enemy);
	}
	public void StopCamera(){
		Camera.main.GetComponent<CameraCtrl> ().enabled = false;
		Camera.main.transform.FindChild ("BGMusic").transform.gameObject.GetComponent<AudioSource> ().clip = bossBattleMusic;
		Camera.main.transform.FindChild ("BGMusic").transform.gameObject.GetComponent<AudioSource> ().Play ();
		player.GetComponent<PlayerCtrl> ().isStuck = true; // stops parallax
		//stops from parallax turning on
		player.transform.FindChild ("leftCheck").gameObject.SetActive (false);
		player.transform.FindChild ("rightCheck").gameObject.SetActive (false);
	}

	public int getScore(){
		return data.score;
	}
	public void SetStarsAwarded(int level, int numOfStarsAwarded){
		Debug.Log ("LEVEL " + level + "-" + numOfStarsAwarded);
		data.levelData [level].numOfStars = numOfStarsAwarded;
	}
	public void UnlockNextLevel(int level){
		data.levelData [level].isUnlocked = true;
	}

	public void LevelComplete(){
		levelCompleteMenu.SetActive (true);
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
			DataCtrl.instance.SaveData (data);
			Invoke ("RestartLevel", restartDelay);
		}
	}

	void GameOver(){
		UI.panelGameOver.SetActive (true);
		data.isFirstBoot = true;
		SaveData ();
	}

	public void ShowPauseMenu(){
		if (mobileUI.activeInHierarchy) {
			mobileUI.SetActive (false);
		}
		pausePanel.SetActive (true);
		isPaused = true;
	}

	public void HidePauseMenu(){
		pausePanel.SetActive (false);
		if (!mobileUI.activeInHierarchy) {
			mobileUI.SetActive (true);
		}
		isPaused = false;
	}
}
