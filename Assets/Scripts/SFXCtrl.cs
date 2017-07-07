using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// SFX ctrl. For all sfx's 
/// </summary>
public class SFXCtrl : MonoBehaviour {
	public static SFXCtrl instance;
	public SFX sfx;


	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}


	/// <summary>
	/// Shows the effect when a coin is collected by the player.
	/// </summary>
	public void ShowCoinSparkle(Vector3 pos){
		Instantiate (sfx.sfx_coin_pickup, pos, Quaternion.identity);
	}
	public void ShowBulletSparkle(Vector3 pos){
		Instantiate (sfx.sfx_bullet_pickup, pos, Quaternion.identity);
	}
	public void ShowPlayerLanding(Vector3 pos){
		Instantiate (sfx.sfx_playerlands, pos, Quaternion.identity);
	}
}
