﻿using System.Collections;
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
	public void ShowSplash(Vector3 pos){
		Instantiate (sfx.sfx_splash, pos, Quaternion.identity);
		AudioCtrl.instance.Splash (pos);
	}
	public void EnemyExplosion(Vector3 pos){
		Instantiate (sfx.sfx_enemy_explosion, pos, Quaternion.identity);
	}
	public void ShowKeySparkle(int keyNumber){
		Vector3 pos = Vector3.zero;
		if (keyNumber == 0)
			pos = sfx.key0.position;
		else if(keyNumber == 1)
			pos = sfx.key1.position;
		else if(keyNumber == 2)
			pos = sfx.key2.position;

		//pos.x -= 1;
		Instantiate (sfx.sfx_bullet_pickup,pos,Quaternion.identity);
	}

	public void HandleBoxBreaking(Vector3 pos){
		Vector3 pos1 = pos;
		pos1.x -= .3f;
		Vector3 pos2 = pos;
		pos2.x += .3f;
		Vector3 pos3 = pos;
		pos3.x -= .3f;
		pos3.y -= .3f;
		Vector3 pos4 = pos;
		pos4.x += .3f;
		pos4.y += .3f;

		Instantiate (sfx.sfx_box_fragment_1, pos1, Quaternion.identity);
		Instantiate (sfx.sfx_box_fragment_2, pos2, Quaternion.identity);
		Instantiate (sfx.sfx_box_fragment_2, pos3, Quaternion.identity);
		Instantiate (sfx.sfx_box_fragment_1, pos4, Quaternion.identity);
		AudioCtrl.instance.BreakCrate (pos);
	}
}
