using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GAMECtrl : MonoBehaviour {
	public static GAMECtrl instance;
	public float restartDelay;
	public GameData data;
	public int coinValue;
	public UI UI;

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
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			ResetData ();
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
		Invoke ("RestartLevel", restartDelay);
	}

	public void PLayerDrowned(GameObject player){
		Invoke ("RestartLevel", restartDelay);
	}

	public void UpdateCointCount(){
		data.coinCount += 1;
		UI.txtCoinCount.text = "X " + data.coinCount;
		UpdateScore (coinValue);
	}

	public void UpdateScore(int val){
		data.score += val;
		UI.txtScore.text = "Score: " + data.score;
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
		
}
