using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCtrl : MonoBehaviour {
	public enum CoinFX{
		Vanish,
		Fly
	}
	public CoinFX coinFX;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.CompareTag("Player")){
			if (coinFX == CoinFX.Vanish) {
				Destroy (gameObject);
			}
		}
	}
}
