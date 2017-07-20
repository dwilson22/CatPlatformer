using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformCtrl : MonoBehaviour {
	public Transform pos1, pos2, startPos;
	public float speed;
	Vector3 nextPos;
	// Use this for initialization
	void Start () {
	//	startPos = pos1;
		nextPos = startPos.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == pos1.position) { 
			nextPos = pos2.position;
		} else if(transform.position == pos2.position){
			nextPos = pos1.position;
		}

		transform.position = Vector3.MoveTowards (transform.position, nextPos, speed * Time.deltaTime);
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine (pos1.position, pos2.position);
	}
}
