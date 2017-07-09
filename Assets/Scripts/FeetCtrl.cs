using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeetCtrl : MonoBehaviour {
	public Transform dustParticlePosition;
	public GameObject player;
	void OnTriggerEnter2D(Collider2D other){

		Debug.Log (other.gameObject.tag);
		if(other.gameObject.CompareTag("Ground")){
			SFXCtrl.instance.ShowPlayerLanding (dustParticlePosition.position);
		}

		if(other.gameObject.CompareTag("Platform")){
			SFXCtrl.instance.ShowPlayerLanding (dustParticlePosition.position);
			player.transform.parent = other.gameObject.transform;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.CompareTag("Platform")){
			player.transform.parent = null;
		}
	}
}
