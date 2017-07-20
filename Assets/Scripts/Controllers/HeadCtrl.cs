using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCtrl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.CompareTag ("Breakable")) {
			SFXCtrl.instance.HandleBoxBreaking (other.gameObject.transform.parent.transform.position);

			gameObject.transform.parent.transform.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			Destroy (other.gameObject.transform.parent.gameObject);
		}
	}
}
