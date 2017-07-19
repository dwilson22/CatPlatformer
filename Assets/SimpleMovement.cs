using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour {
	public float speed;

	SpriteRenderer sr;
	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		SetStartingDirection ();
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
	}

	void Move(){
		Vector2 temp = rb.velocity;
		temp.x = speed;
		rb.velocity = temp;
	}

	void SetStartingDirection(){
		if (speed > 0) {
			sr.flipX = true;
		} else if (speed < 0) {
			sr.flipX = false;
		}
	}
	void FlipOnCollision(){
		speed = -speed;
		SetStartingDirection ();
	}
	void OnCollisionEnter2D(Collision2D other){
		if(!other.gameObject.CompareTag("Player")){
			FlipOnCollision ();
		}
	}
}
