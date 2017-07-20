using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeadCtrl : MonoBehaviour {
	public GameObject enemy;

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.tag);
		if(other.gameObject.CompareTag("Feet")){
			GAMECtrl.instance.PlayerStompEnemy (enemy);
			SFXCtrl.instance.EnemyExplosion (enemy.transform.position);
			AudioCtrl.instance.Stomp (other.gameObject.transform.position);
		}
	}
}
