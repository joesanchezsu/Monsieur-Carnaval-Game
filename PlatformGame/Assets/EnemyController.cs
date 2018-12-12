using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed = 1f; // 2 variables because speed changes the direction
	public float maxSpeed = 1f;

    private Rigidbody2D rb;
	private Animator anim;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		// It moves constantly
		rb.AddForce(Vector2.right * speed);
		float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
		rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

		// When it find a wall, change direction
		if(rb.velocity.x > -0.01f && rb.velocity.x < 0.01f){
			speed = -speed;
			transform.localScale = new Vector3(speed > 0 ? -1 : 1, 1, 1);
			rb.velocity = new Vector2(speed, rb.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			float yOffset = 0.25f;
			// if player is on it, it dies crushed
			if(transform.position.y + yOffset < col.transform.position.y){
				anim.SetBool("crushed", true);
				Invoke("Fall", 0.25f); // Fall after 0.25 seconds
				col.SendMessage("EnemyJump"); // Rebound
			}
			// Knockback for player and life reduction 
			else {
				col.SendMessage("EnemyKnockBack", transform.position.x);
			}
		} else if(col.gameObject.tag == "Arrow"){
			// Arrow only kills when it has more horizontal velocity (to avoid killing after rebounding)
			if(rb.velocity.x < col.GetComponent<Rigidbody2D>().velocity.x){
				anim.SetBool("hit", true);
				Invoke("Fall", 0.25f);
			}
		}
	}

	void Fall(){
		gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
		gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
	}

	void OnBecameInvisible(){
		if(gameObject.transform.position.y < -10f){
			Destroy(gameObject);
		}
	}
}
