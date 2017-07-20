using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour {
	public static AudioCtrl instance;
	public PlayerAudio playerAudio;
	public AudioFx audioFx;
	public Transform player;
	public GameObject bgMusic;

	[Tooltip("Used to toggle sound on/off")]
	public bool soundOn, bgMusicOn;
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
		if (bgMusicOn) {
			bgMusic.SetActive (true);
		}
	}
	
	public void PlayerJump(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.playerJump, playerPos);
		}
	}
	public void CoinPickup(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.coinPickup, playerPos);
		}
	}
	public void FireBullet(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.fireBullets, playerPos);
		}
	}
	public void EnemyExplosion(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.enemyExplosion, playerPos);
		}
	}
	public void BreakCrate(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.breakCrates, playerPos);
		}
	}

	public void Stomp(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.stomp, playerPos);
		}
	}
	public void Splash(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.splash, playerPos);
		}
	}
	public void PLayerDied(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.playerDied, playerPos);
		}
	}
	public void KeyFound(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.keyFound, playerPos);
		}
	}
	public void PowerUp(Vector3 playerPos){
		if (soundOn) {
			AudioSource.PlayClipAtPoint (playerAudio.powerUp, playerPos);
		}
	}
}
