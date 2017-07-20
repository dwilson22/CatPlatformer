using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCtrl : MonoBehaviour {
	Rigidbody2D rb;
	public Vector2 velocity;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Enemy")) {
			GAMECtrl.instance.BulletHitEnemy (other.gameObject.transform);
			GAMECtrl.instance.UpdateScore (GAMECtrl.Item.Enemy);
			AudioCtrl.instance.EnemyExplosion (other.transform.position);
			Destroy (gameObject);
		} else if (!other.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Enemy")) {
			GAMECtrl.instance.BulletHitEnemy (other.gameObject.transform);
			GAMECtrl.instance.UpdateScore (GAMECtrl.Item.Enemy);
			AudioCtrl.instance.EnemyExplosion (other.transform.position);
			Destroy (gameObject);
		}
	}
}
