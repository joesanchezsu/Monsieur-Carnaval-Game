using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 2f;
	public float maxSpeed = 5f;
	public bool grounded;
	public float jumpPower = 6.5f;
	public Rigidbody2D arrow;

	private Rigidbody2D rb;
	private Animator anim;
	private SpriteRenderer spr;
	private bool jump; // to get the value in Update() but use it in FixedUpdate
	private bool doubleJump;
	private bool mouvement = true;
	
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		spr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
		anim.SetBool("grounded", grounded);

		if(grounded){
			doubleJump = true; // one jump on memory (warning jump ^^)
		}

		if(Input.GetKeyDown(KeyCode.UpArrow)){
			if(grounded){
				jump = true; 
				doubleJump = true;
			} else if(doubleJump){
				jump = true;
				doubleJump = false;
			}
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			anim.SetBool("arrow", true);
			StartCoroutine(Shoot());
		}
		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
	}

	void FixedUpdate(){
		Vector3 fixedVelocity = rb.velocity;
		fixedVelocity.x *= 0.75f; // it affects only X! OJO!!

		if(grounded){
			rb.velocity = fixedVelocity;
		}

		float h = Input.GetAxis("Horizontal");
		
		if(!mouvement){
			h = 0;
		}

		rb.AddForce(Vector2.right * speed * h);

		float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
		rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

		if(h > 0.1f){
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		if(h < -0.1f){
			transform.localScale = new Vector3(1f, 1f, 1f);
		}

		if(jump){
			rb.velocity = new Vector2(rb.velocity.x, 0); // to avoid impulses together
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			jump = false;
		}
	}

	IEnumerator Shoot()
    {
        yield return new WaitForSeconds(0.41f);
		Rigidbody2D clone;
		Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.34f, transform.position.z);
		clone = Instantiate(arrow, pos, transform.rotation) as Rigidbody2D;
		clone.transform.localScale = new Vector3(gameObject.transform.localScale.x, 1f, 1f);
		clone.velocity = transform.TransformDirection(new Vector3(-10f * gameObject.transform.localScale.x, 1f, 1f));
		//yield return new WaitForSeconds(0.1f);
        anim.SetBool("arrow", false);
    }


	void OnBecameInvisible(){
		transform.position = new Vector3(-1, 0, 0);
	}

	public void EnemyJump(){
		jump = true;
	}

	public void EnemyKnockBack(float enemyPosX){
		jump = true;
		float side = Mathf.Sign(enemyPosX - transform.position.x);
		rb.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
		mouvement = false;
		Invoke("EnableMouvement", 0.7f);
		// Color color = new Color(R, G, B, A); /255 because 255 -> 1
		spr.color = Color.red;
	}

	void EnableMouvement(){
		mouvement = true;
		spr.color = Color.white;
	}

	public void SetMoving(bool move){
		mouvement = move;
	}
}
