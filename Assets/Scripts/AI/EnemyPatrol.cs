using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour {
	public Transform leftBound, rightBound;
	public float speed,maxDelay,minDelay;

	bool canTurn;
	float originalSpeed;
	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		SetStartingDirection ();

		canTurn = true;

	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		FlipOnEdges ();
	}

	void SetStartingDirection(){
		if (speed > 0) {
			sr.flipX = true;
		} else if (speed < 0) {
			sr.flipX = false;
		}
	}

	void FlipOnEdges(){
		if (sr.flipX && transform.position.x >= rightBound.transform.position.x) {
			if (canTurn) {
				canTurn = false;
				originalSpeed = speed;
				speed = 0;
				StartCoroutine ("TurnLeft", originalSpeed);
			}
		} else if (!sr.flipX && transform.position.x <= leftBound.transform.position.x) {
			if (canTurn) {
				canTurn = false;
				originalSpeed = speed;
				speed = 0;
				StartCoroutine ("TurnRight", originalSpeed);
			}
		}
	}
	IEnumerator TurnLeft(float ospeed){
		anim.SetBool ("isIdle", true);
		yield return new WaitForSeconds (Random.Range (minDelay, maxDelay));
		anim.SetBool ("isIdle", false);
		sr.flipX = false;
		speed = -ospeed;
		canTurn = true;
	}

	IEnumerator TurnRight(float ospeed){
		anim.SetBool ("isIdle", true);
		yield return new WaitForSeconds (Random.Range (minDelay, maxDelay));
		anim.SetBool ("isIdle", false);
		sr.flipX = true;
		speed = -ospeed;
		canTurn = true;
	}

	void Move(){
		Vector2 temp = rb.velocity;
		temp.x = speed;
		rb.velocity = temp;
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine (leftBound.position, rightBound.position);
	}
}
