using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWithDelay : MonoBehaviour {
	public float delay;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, delay);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
