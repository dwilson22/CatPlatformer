using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelOneBossAI : MonoBehaviour {
	public float jumpSpeed, firingDelay;
	public int startJumpAt, jumpdelay, health;
	public Slider bossHealth;
	public GameObject bossBullet;

	Vector3 bulletSpawnPos;
	Rigidbody2D rb;
	SpriteRenderer sr;
	bool canFire, isJumping;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		canFire = false;
		bulletSpawnPos = gameObject.transform.FindChild ("BulletSpawnPos").transform.position;
		Invoke("Reload",Random.Range(1f, firingDelay));
	}
	
	// Update is called once per frame
	void Update () {
		if (canFire) {
			FireBullets ();
			canFire = false;
		}

		if (health < startJumpAt && !isJumping) {
			InvokeRepeating ("Jump", 0, jumpdelay);
			isJumping = true;
		}
		if (isJumping) {
			bulletSpawnPos = gameObject.transform.FindChild ("BulletSpawnPos").transform.position;
		}
	}

	void Reload(){
		canFire = true;
	}

	void FireBullets(){
		Instantiate (bossBullet,bulletSpawnPos, Quaternion.identity);
		Invoke("Reload",Random.Range(1f, firingDelay));
	}
	void RestoreColour(){
		sr.color = Color.white;
	}
	void Jump(){
		rb.AddForce (new Vector2 (0, jumpSpeed));
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.CompareTag ("PlayerBullet")) {
			if (health <= 0) {
				GAMECtrl.instance.BossIsKilled (gameObject.transform);
			}

			if (health > 0) {
				health--;
				bossHealth.value = (float)health;
				sr.color = Color.red;
				Invoke ("RestoreColour", .1f);
			}
		}
	}


}
