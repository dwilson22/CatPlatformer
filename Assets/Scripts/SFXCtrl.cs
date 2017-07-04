using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SFX ctrl. For all sfx's 
/// </summary>
public class SFXCtrl : MonoBehaviour {
	public static SFXCtrl instance;

	public GameObject sfx_coin_pickup;



	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}


	/// <summary>
	/// Shows the effect when a coin is collected by the player.
	/// </summary>
	public void ShowCoinSparkle(Vector3 pos){
		Instantiate (sfx_coin_pickup, pos, Quaternion.identity);
	}
}
