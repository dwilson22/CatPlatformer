using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishJumperCtrl : MonoBehaviour {
	public float jumpDelay;
	public GameObject fish;
	FishAI fai;
	Rigidbody2D rb;
	void Start(){
		rb = fish.GetComponent<Rigidbody2D> ();
		fai = fish.GetComponent<FishAI> ();
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy")) {
			
			rb.isKinematic = true;
			rb.velocity = Vector2.zero;
			Invoke ("CallFishJump", jumpDelay);
		}
	}

	void CallFishJump(){
		rb.isKinematic = false;
		fai.FishJump ();
	}
}
