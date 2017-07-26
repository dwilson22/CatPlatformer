using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DataCtrl : MonoBehaviour {
	public GameData data;
	string dataFilePath;
	BinaryFormatter bf;
	public static DataCtrl instance = null;
	void Awake(){
		if (instance == null) {
			
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			
			Destroy (gameObject);
		}

		bf = new BinaryFormatter ();
		dataFilePath = Application.persistentDataPath + "/game.dat";
	}

	public void RefreshData(){
		if (File.Exists (dataFilePath)) {
			
			FileStream fs = new FileStream (dataFilePath, FileMode.Open);
			data = (GameData)bf.Deserialize (fs);
			fs.Close ();
		} else {
			data = new GameData ();
			data.coinCount = 0;
			data.score = 0;
			data.keyFound = new bool[3];
			data.levelData = new LevelData[4];
			data.lives = 3;
			for (int i = 0; i < 4; i++) {
				LevelData ld = new LevelData ();
				if (i == 1) {
					ld.isUnlocked = true;
				} else {
					ld.isUnlocked = false;
				}
				ld.levelNum = i;
				ld.numOfStars = 0;
				data.levelData[i] = ld;
			}
			FileStream fs = new FileStream (dataFilePath, FileMode.Create);
			bf.Serialize (fs, data);
			fs.Close ();
		}
	}

	public void SaveData(){
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close ();
	}

	public void SaveData(GameData data){
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close ();
	}

	void OnEnable(){
		Debug.Log (dataFilePath);
		RefreshData ();
	}

	public bool isUnlocked(int levelNumber){
		return data.levelData [levelNumber].isUnlocked;
	}

	public int getStarsAwarded(int levelNumber){
		return data.levelData [levelNumber].numOfStars;
	}

}
