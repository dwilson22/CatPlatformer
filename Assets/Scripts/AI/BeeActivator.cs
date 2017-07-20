using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeActivator : MonoBehaviour {
 
	public GameObject bee;

	BomberBeeAI bbai;

	void Start(){
		bbai = bee.GetComponent<BomberBeeAI> ();

	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			bbai.ActivateBee (other.transform.position);


		}
	}



}
