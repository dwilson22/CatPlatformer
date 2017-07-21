using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCtrl : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Player")) {
			SFXCtrl.instance.ShowBulletSparkle (gameObject.transform.position);
			Destroy (gameObject);
		}
	}
}
