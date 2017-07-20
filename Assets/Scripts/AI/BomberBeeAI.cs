using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BomberBeeAI : MonoBehaviour {
	public float destroybeeDelay, speed;
	public GameObject player;

	SpriteRenderer sr;

	void Start(){
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}
	void Update(){
		float beeX = gameObject.transform.position.x;
		float playerX = player.transform.position.x;
		if (beeX > playerX) {
			sr.flipX = false;
		} else if (beeX < playerX) {
			sr.flipX = true;
		}
	}

	public void ActivateBee(Vector3 playerPos){
		
		transform.DOMove (playerPos, speed, false);




	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Player")){
			SFXCtrl.instance.EnemyExplosion (gameObject.transform.position);

			Destroy (gameObject, destroybeeDelay);
		}
	}
}
