using System.Collections;
using System.Collections.Generic;
using UnityEngine;
///<summary>
/// Controls character, left to right.
///</summary>
public class PlayerCtrl : MonoBehaviour {

	[Tooltip("This is an postive int that is used to speed up player movement by the amount multiplied.")]
	public int speedBoost;
	public float jumpSpeed;
	public bool isGrounded, SFXOn, canFire, isStuck;
	public Transform feet;
	public float feetRadius;
	public float boxWidth;
	public float boxHeight;
	public LayerMask whatIsGround;
	public float delayForDoubleJump;
	public Transform leftBulletSpawnPos, rightBulletSpawnPos;
	public GameObject leftBullet, rightBullet, garbageCollector;


	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;
	bool isJumping;
	bool canDoubleJump;
	public bool rightPressed, leftPressed;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		//isGrounded = Physics2D.OverlapCircle (feet.position, feetRadius, whatIsGround);
		isGrounded = Physics2D.OverlapBox(new Vector2(feet.position.x,feet.position.y), new Vector2(boxWidth,boxHeight), 360.0f, whatIsGround);

		float playerSpeed = Input.GetAxisRaw ("Horizontal");
		playerSpeed *= speedBoost;



		if (playerSpeed != 0) {
			MoveHorizontal (playerSpeed);
		} else {
			
			StopMoving ();
		}
		if (Input.GetButtonDown ("Jump")) {
			anim.SetInteger ("State", 2);
			Jump ();
		}

		if (Input.GetButtonDown ("Fire1")) {
			fireBullet ();
		}

		showFalling ();

		if (leftPressed) {
			MoveHorizontal (-speedBoost);
		}

		if (rightPressed) {
			MoveHorizontal (speedBoost);
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (feet.position, feetRadius);

		Gizmos.DrawWireCube (feet.position, new Vector3 (boxWidth, boxHeight, 0));
	}

	void MoveHorizontal(float playerSpeed){
		rb.velocity = new Vector2 (playerSpeed, rb.velocity.y);
		if (playerSpeed < 0) {
			sr.flipX = true;
		} else {
			sr.flipX = false;
		}

		if(!isJumping)
			anim.SetInteger ("State", 1);
	}

	void showFalling(){
		if (rb.velocity.y < 0 ) {
			anim.SetInteger ("State", 3);
		}
	}

	void StopMoving(){
		rb.velocity = new Vector2 (0, rb.velocity.y);
		if (!isJumping) {
			anim.SetInteger ("State", 0);
		}
	}

	void Jump(){
		anim.SetInteger ("State", 2);
		isJumping = true;
		if (isGrounded) {
			isJumping = true;
			rb.AddForce (new Vector2 (0,jumpSpeed));
			anim.SetInteger ("State", 2);
			AudioCtrl.instance.PlayerJump (gameObject.transform.position);
			Invoke ("CanDoubleJump", delayForDoubleJump);
		}

		if (canDoubleJump && !isGrounded) {

			rb.velocity = Vector2.zero;
			rb.AddForce (new Vector2 (0,jumpSpeed));
			anim.SetInteger ("State", 2);
			AudioCtrl.instance.PlayerJump (gameObject.transform.position);
			canDoubleJump = false;
		}
	}

	void fireBullet(){
		if (canFire) {
			if (sr.flipX) {
				Instantiate (leftBullet, leftBulletSpawnPos.position, Quaternion.identity);
			} else {
				Instantiate (rightBullet, rightBulletSpawnPos.position, Quaternion.identity);

			}
			AudioCtrl.instance.FireBullet (gameObject.transform.position);
		}
	}

	public void MobileMoveLeft(){
		leftPressed = true;
	}
	public void MobileMoveRight(){
		rightPressed = true;
	}
	public void MobileMoveStop(){
		leftPressed = false;
		rightPressed = false;
		StopMoving ();
	}

	public void MobileFireBullets(){
		fireBullet ();
	}

	public void MobileJump(){
		Jump ();
	}

	void CanDoubleJump(){
		canDoubleJump = true;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("Ground") || other.gameObject.CompareTag ("Platform")) {
			isJumping = false;
		}
		if (other.gameObject.CompareTag ("Enemy")) {
			GAMECtrl.instance.PlayerDiedAnimation (gameObject);
		}
		if (other.gameObject.CompareTag ("BigCoin")) {
			GAMECtrl.instance.UpdateCointCount ();
			SFXCtrl.instance.ShowBulletSparkle (other.gameObject.transform.position);
			AudioCtrl.instance.CoinPickup (gameObject.transform.position);
			Destroy (other.gameObject);
			GAMECtrl.instance.UpdateScore (GAMECtrl.Item.BigCoin);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("HELLO "+other.gameObject.tag);
		switch (other.gameObject.tag) {
		case "Coin":
			GAMECtrl.instance.UpdateCointCount ();
			GAMECtrl.instance.UpdateScore (GAMECtrl.Item.Coin);
			AudioCtrl.instance.CoinPickup (gameObject.transform.position);

			if(SFXOn)
				SFXCtrl.instance.ShowCoinSparkle (other.gameObject.transform.position);
			break;
		case "Powerup_Bullet":
			canFire = true;
			AudioCtrl.instance.PowerUp (gameObject.transform.position);
			Vector3 powerupPos = other.gameObject.transform.position;
			Destroy (other.gameObject);
			if(SFXOn)
				SFXCtrl.instance.ShowBulletSparkle (powerupPos);
			
			break;
		case "Water":
			garbageCollector.SetActive (false);
			GAMECtrl.instance.PlayerDrowned (gameObject);
			if(SFXOn)
				SFXCtrl.instance.ShowSplash (gameObject.transform.position);
			break;
		case"Enemy":
			GAMECtrl.instance.PlayerDiedAnimation (gameObject);
			break;
		default:
			break;
		}
	}
}
