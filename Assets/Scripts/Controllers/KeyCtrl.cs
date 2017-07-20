using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCtrl : MonoBehaviour {
	public int keyNumber;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			GAMECtrl.instance.UpdateKeyCount (keyNumber);
			SFXCtrl.instance.ShowKeySparkle (keyNumber);
			Destroy (gameObject);
		}
	}
}
