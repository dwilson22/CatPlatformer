using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCtrl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			GAMECtrl.instance.PLayerDied (other.gameObject);
		} else {
			Destroy (other.gameObject);
		}
	}
}
