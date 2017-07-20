using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCtrl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			GAMECtrl.instance.PlayerDied (other.gameObject);
		} else if(!other.gameObject.CompareTag ("Ground") && !other.gameObject.CompareTag ("Water") ) {
			Destroy (other.gameObject);
		}
	}
}
