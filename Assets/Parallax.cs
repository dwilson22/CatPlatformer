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
			offSetX += Input.GetAxisRaw ("Horizontal") * speed;

			if (playerCtrl.leftPressed) {
				offSetX += -speed;
			} else if (playerCtrl.rightPressed) {
				offSetX += speed;
			}
			mat.SetTextureOffset ("_MainTex", new Vector2 (offSetX, 0));
		}
	}
}
