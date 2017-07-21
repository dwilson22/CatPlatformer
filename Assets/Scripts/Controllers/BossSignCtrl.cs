using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSignCtrl : MonoBehaviour {
	GameObject invisibleWall;
	public GameObject boss;
	public Slider bossHealth;
	void Start(){
		invisibleWall = gameObject.transform.FindChild ("InvisibleWall").transform.gameObject;
		bossHealth.gameObject.SetActive (false);
		boss.GetComponent<LevelOneBossAI> ().enabled = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			GAMECtrl.instance.StopCamera();
			BoxCollider2D bc2d = invisibleWall.GetComponent<BoxCollider2D> ();
			bc2d.isTrigger = false;
			bossHealth.gameObject.SetActive (true);
			boss.GetComponent<LevelOneBossAI> ().enabled = true;
			gameObject.SetActive (false);
		}
	}
}
