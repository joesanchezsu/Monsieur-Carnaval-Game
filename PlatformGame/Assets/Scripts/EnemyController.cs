﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public float speed = 1f; // 2 variables because speed changes the direction
	public float maxSpeed = 1f;
	public int type; // 0 for red enemies, 1 for blue enemies
	public GameObject pointsUp;
	public Image background;

    private Rigidbody2D rb;
	private Animator anim;
	private bool isDead = false;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}
	
	void FixedUpdate () {
		// Increase points moving up the start "pointsUp"
		if(isDead){
			pointsUp.transform.Rotate(0f, 10f, 0f);
			pointsUp.transform.Translate(0f, 0.1f, 0f);
		}

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
			
			// if player is on it and it's a red enemy, it dies crushed
			if(transform.position.y + yOffset < col.transform.position.y && type == 0){
				isDead = true;
				anim.SetBool("crushed", true);
				Invoke("Fall", 0.25f); // Fall after 0.25 seconds
				col.SendMessage("EnemyJump"); // Rebound
				background.GetComponent<BackgroundController>().SetFogByEnemy();
			}
			// Knockback for player and health reduction 
			else {
				col.SendMessage("EnemyKnockBack", transform.position.x);
			}
			
		} else if(col.gameObject.tag == "Arrow"){
			// Arrow only kills when it has more horizontal velocity (to avoid killing after rebounding)
			if(rb.velocity.x < col.GetComponent<Rigidbody2D>().velocity.x){
				isDead = true;
				anim.SetBool("hit", true);
				Invoke("Fall", 0.25f);
				background.GetComponent<BackgroundController>().SetFogByEnemy();
			}
		}
	}

	void Fall(){
		foreach(Collider2D col in GetComponents<Collider2D>()){
			col.isTrigger = true;
		}
		//for(int i = 0; i < 10; i++){
			//pointsUp.transform.Rotate(Vector3.up * Time.deltaTime);
			//pointsUp.transform.position = new Vector3(pointsUp.transform.position.x, pointsUp.transform.position.y * Time.deltaTime, 0f);
		//}
	}

	void OnBecameInvisible(){
		if(gameObject.transform.position.y < -10f){
			Destroy(gameObject);
		}
	}
}
