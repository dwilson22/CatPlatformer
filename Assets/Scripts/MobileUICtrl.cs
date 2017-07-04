using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileUICtrl : MonoBehaviour {
	public GameObject player;

	PlayerCtrl playerCtrl;
	// Use this for initialization
	void Start () {
		playerCtrl = player.GetComponent<PlayerCtrl> ();
	}
	
	// Update is called once per frame
	public void MobileMoveLeft(){
		playerCtrl.MobileMoveLeft ();
	}
	public void MobileMoveRight(){
		playerCtrl.MobileMoveRight ();
	}
	public void MobileMoveStop(){
		playerCtrl.MobileMoveStop ();
	}

	public void MobileFire(){
		playerCtrl.MobileFireBullets ();
	}

	public void MobileJump(){
		playerCtrl.MobileJump ();
	}
}
