﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed = 2f;
    public float maxSpeed = 5f;
    public bool grounded;
    public float jumpPower = 6.5f;
    public int maxHealth = 6; // it have to be a pair number (10 max)
    public GameObject healthBar;
    //public Text lifeText;
    //public GameObject ennemy;
    //float time = 0.0f;
    //int randonTime = 4;
    //public Vector3 positionEnnemy = new Vector3(8, 6, 0);
    public Rigidbody2D arrow;
    public GameObject gameOverPopUp;

	private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spr;
    private bool jump; // to get the value in Update() but use it in FixedUpdate
    private bool doubleJump;
    private bool mouvement = true;
    private int health;
    private int checkpoint;

    // Use this for initialization
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        health = maxHealth;
        healthBar.GetComponent<HealthController>().SetHealth(health);
    }

    // Update is called once per frame
    void Update() {
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("grounded", grounded);

        if (grounded) {
            doubleJump = true; // one jump on memory (warning jump ^^)
        }

		// To Jump (and double jump)
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if (grounded) {
                jump = true;
                doubleJump = true;
            } else if (doubleJump) {
                jump = true;
                doubleJump = false;
            }
        }

		// To Shoot an arrow
        if (Input.GetKeyDown(KeyCode.Space)) {
			if(anim.GetBool("arrow") == false){
				anim.SetBool("arrow", true);
				Invoke("Shoot", 0.60f); // shoot an arrow after 0.41 seconds
			}
        }
    }

    void FixedUpdate() {
        if(gameOverPopUp.GetComponent<GameOverController>().GetRestart()){
            Respawn(0);
        }
        // else restart game
        //positionEnnemy = new Vector3(8, 6, 0);
        Vector3 fixedVelocity = rb.velocity;
        fixedVelocity.x *= 0.75f; // it affects only X!

        if (grounded) {
            rb.velocity = fixedVelocity;
        }

		// Get Input of arrow keys
        float h = Input.GetAxis("Horizontal");

		// if it's blocked because of an enemy
        if (!mouvement) {
            h = 0;
        }
		// Set velocity
        rb.AddForce(Vector2.right * speed * h);
        float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
        rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

		// Set direction which the player is looking
        if (h > 0.1f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (h < -0.1f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

		// Jump
        if (jump) {
            rb.velocity = new Vector2(rb.velocity.x, 0); // to avoid impulses together in double jump
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
        }
        // if (ennemy != null)
        // {
        //     time = time + Time.deltaTime;
        //     if (time > randonTime)
        //     {
        //         Instantiate(ennemy, positionEnnemy, transform.rotation);
        //         randonTime = Random.Range(5, 15);
        //         time = 0;
        //         Debug.Log(randonTime);
        //         Debug.Log(positionEnnemy);
        //     }
        // }
    }

    void OnBecameInvisible() {
        if (health <= 0){ // Game over
            gameOverPopUp.GetComponent<GameOverController>().ShowPopUp();
            healthBar.GetComponent<HealthController>().SetHealth(health);
        } else { // mejorar con los checkpoints 
            checkpoint = 1;
            Respawn(checkpoint);
        }
    }

    void Respawn(int cp){
        if(cp == 0){
            gameOverPopUp.GetComponent<GameOverController>().HidePopUp();
            health = maxHealth;
            if(healthBar != null){
                healthBar.GetComponent<HealthController>().SetHealth(health);
            }
            transform.position = new Vector3(-14.05f, -1.46f, 0f);
            SetMoving(true);
        } else {
            health -= 2;
            if(healthBar != null){
                healthBar.GetComponent<HealthController>().SetHealth(health);
            }
            transform.position = new Vector3(-5f, -1.46f, 0);
        }
    }

	void Shoot(){
		// Set position correspoding the position where the arrow go out in the animation
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.34f, transform.position.z);
		// Create clone
		Rigidbody2D clone = Instantiate(arrow, pos, transform.rotation) as Rigidbody2D;
		// Set direction and velocity
		clone.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, 1f);
		clone.velocity = transform.TransformDirection(new Vector3(-10f * gameObject.transform.localScale.x, 1f, 1f));
        anim.SetBool("arrow", false); // stop animation
    }

	public void EnemyJump(){
		jump = true;
	}

	public void EnemyKnockBack(float enemyPosX){
		jump = true;
        //float startTime = 0.0f;
        float side = Mathf.Sign(enemyPosX - transform.position.x);
		rb.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
		mouvement = false;
		Invoke("EnableMouvement", 0.7f);
		// Color color = new Color(R, G, B, A); /255 because 255 -> 1
		spr.color = Color.red;
        health--;
        healthBar.GetComponent<HealthController>().SetHealth(health);
        //if(life < 1.5){
            //spr.color = Color.green;
            //gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            //gameObject.GetComponentInChildren<CircleCollider2D>().isTrigger = true;
            // Invoke("Respawn", 2f);
        //}
        //UpDateTextLife();
        // if (life < 0.5){
        //     spr.color = Color.green;
        //     double down = -0.01;
        //     while (startTime < 2){
        //         transform.localPosition = transform.position + new Vector3(0, System.Convert.ToSingle(down), 0);
        //         startTime = startTime + Time.deltaTime;
        //     }
        // }
    }

	void EnableMouvement(){
		mouvement = true;
		spr.color = Color.white;
	}

	public void SetMoving(bool move){
		mouvement = move;
	}
}
