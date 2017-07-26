using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioCtrl : MonoBehaviour {
	public static AudioCtrl instance;
	public PlayerAudio playerAudio;
	public AudioFx audioFx;
	public Transform player;
	public GameObject bgMusic, btnMusic, btnSound;
	public Sprite musicON, musicOff, sfxOn, sfxOff;

	[Tooltip("Used to toggle sound on/off")]
	public bool soundOn, bgMusicOn;
	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}
		if (DataCtrl.instance.data.playMusic) {
			bgMusic.SetActive (true);
			btnMusic.GetComponent<Image> ().sprite = musicON;
		} else {
			bgMusic.SetActive (false);
			btnMusic.GetComponent<Image> ().sprite = musicOff;
		}
		if (DataCtrl.instance.data.playSound) {
			
			btnSound.GetComponent<Image> ().sprite = sfxOn;
		} else {
			
			btnSound.GetComponent<Image> ().sprite = sfxOff;
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

	public void ToggleSound(){
		if (DataCtrl.instance.data.playSound) {
			soundOn = false;
			btnSound.GetComponent<Image> ().sprite = sfxOff;
			DataCtrl.instance.data.playSound = false;
		} else {
			soundOn = true;
			btnSound.GetComponent<Image> ().sprite = sfxOn;
			DataCtrl.instance.data.playSound = true;
		}
	}

	public void ToggleMusic(){
		if (DataCtrl.instance.data.playMusic) {
			bgMusic.SetActive (false);
			btnMusic.GetComponent<Image> ().sprite = musicOff;
			DataCtrl.instance.data.playMusic = false;
		} else {
			bgMusic.SetActive (true);
			btnMusic.GetComponent<Image> ().sprite = musicON;
			DataCtrl.instance.data.playMusic = true;
		}
	}
}
