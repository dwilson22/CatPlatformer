using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera ctrl.
/// </summary>
public class CameraCtrl : MonoBehaviour {

	public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.position.x,player.position.y,transform.position.z);
	}
}
