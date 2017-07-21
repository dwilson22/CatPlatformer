using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSignCtrl : MonoBehaviour {
	GameObject invisibleWall;

	void Start(){
		invisibleWall = gameObject.transform.FindChild ("InvisibleWall").transform.gameObject;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			GAMECtrl.instance.StopCamera();
			BoxCollider2D bc2d = invisibleWall.GetComponent<BoxCollider2D> ();
			bc2d.isTrigger = false;
		}
	}
}
