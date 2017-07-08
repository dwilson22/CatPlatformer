using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera ctrl.
/// </summary>
public class CameraCtrl : MonoBehaviour {

	public Transform player;
	private float yAxis;
	private float xAxis;
	public float offSetY;
	public float offSetX;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player.position.y < 0) {
			yAxis = 0.0f;
		}else if(player.position.y > 4){
			yAxis = 4.0f;
		} else {
			yAxis = player.position.y;
		}
		if (player.position.x < 0) {
			xAxis = 0.0f;
		}else if (player.position.x > 115 ){
			xAxis = 115.0f;
		} else {
			xAxis = player.position.x;
		}
		//my solution, check to see boundries.
		transform.position = new Vector3(xAxis+offSetX,yAxis+offSetY,transform.position.z);
		//only follows x axis for course
		//transform.position = new Vector3(player.position.x,transform.position.y,transform.position.z);
		//course's solution, ffset y axis.
		//transform.position = new Vector3(player.position.x,player.position.y + offSet,transform.position.z);
	}
}
