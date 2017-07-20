using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
	public float speed;
	public GameObject player;
	float offSetX;
	Material mat;
	PlayerCtrl playerCtrl;


	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;
		playerCtrl = player.GetComponent<PlayerCtrl> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerCtrl.isStuck) {
			float savedOffset = offSetX;
			offSetX += Input.GetAxisRaw ("Horizontal") * speed;



			//Debug.Log ("player LOCATION " + player.transform.position.x);
			if (player.transform.position.x > 0 && player.transform.position.x < 115) {
				
				if (playerCtrl.leftPressed) {
					offSetX += -speed;
				} else if (playerCtrl.rightPressed) {
					offSetX += speed;
				}
			} else {
				offSetX = savedOffset;
			}
			mat.SetTextureOffset ("_MainTex", new Vector2 (offSetX, 0));
		}
	}
}
