using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMECtrl : MonoBehaviour {
	public static GAMECtrl instance;
	public float restartDelay;

	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// Restarts the level when player dies.
	/// </summary>
	public void PLayerDied(GameObject player){
		
		player.SetActive (false);
		Invoke ("RestartLevel", restartDelay);
	}

	public void RestartLevel(){
		SceneManager.LoadScene ("Gameplay");
	}
		
}
