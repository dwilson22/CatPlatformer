﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GAMECtrl : MonoBehaviour {
	public static GAMECtrl instance;
	public float restartDelay;
	public GameData data;

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
		bf.Serialize (fs, data);
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

	public void RestartLevel(){
		SceneManager.LoadScene ("Gameplay");
	}
		
}
