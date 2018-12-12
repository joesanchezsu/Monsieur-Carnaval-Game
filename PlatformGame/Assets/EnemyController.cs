using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed = 1f; // 2 variables because speed changes the direction
	public float maxSpeed = 1f;


    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        
		rb.AddForce(Vector2.right * speed);

		float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
		rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

		if(rb.velocity.x > -0.01f && rb.velocity.x < 0.01f){
			speed = -speed;
			transform.localScale = new Vector3(speed > 0 ? -1 : 1, 1, 1);
			rb.velocity = new Vector2(speed, rb.velocity.y);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			float yOffset = 0.25f;
			if(transform.position.y + yOffset < col.transform.position.y){
				col.SendMessage("EnemyJump");
				Destroy(gameObject);
			} else {
				col.SendMessage("EnemyKnockBack", transform.position.x);
			}
		} else if(col.gameObject.tag == "Arrow"){
			//Destroy(gameObject);
		}
	}
}
