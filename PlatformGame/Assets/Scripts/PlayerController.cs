using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public bool aleave;
	public float speed = 2f;
	public float maxSpeed = 5f;
	public bool grounded;
	public float jumpPower = 6.5f;
	public double life = 3;
	public Text lifeText;
   


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
        UpDateTextLife();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
		anim.SetBool("grounded", grounded);

		if(grounded){
			doubleJump = true; // one jump on memory (warning jump ^^)
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			if(grounded){
				jump = true; 
				doubleJump = true;
			} else if(doubleJump){
				jump = true;
				doubleJump = false;
			}
			
		}
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
			transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if(h < -0.1f){
			transform.localScale = new Vector3(-1f, 1f, 1f);
		}

		if(jump){
			rb.velocity = new Vector2(rb.velocity.x, 0); // to avoid impulses together
			rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
			jump = false;
		}
	}

	void OnBecameInvisible(){
        if (life < 0.5)
        {
            life = 3;
            UpDateTextLife();
            transform.position = new Vector3(System.Convert.ToSingle(-14.12), System.Convert.ToSingle(-1.28), 0);
        }
        else
        {
            transform.position = new Vector3(-8, System.Convert.ToSingle(-1.5), 0);
            life = life - 1;
            UpDateTextLife();
        }

	}

	public void EnemyJump(){
		jump = true;
	}

	public void EnemyKnockBack(float enemyPosX){
		jump = true;
        float startTime= 0.0f;
		float side = Mathf.Sign(enemyPosX - transform.position.x);
		rb.AddForce(Vector2.left * side * jumpPower, ForceMode2D.Impulse);
		mouvement = false;
		Invoke("EnableMouvement", 0.7f);
		// Color color = new Color(R, G, B, A); /255 because 255 -> 1
		spr.color = Color.red;
		life = life - 0.5;
        UpDateTextLife();
        if (life < 0.5)
        {
            spr.color = Color.green;
            double down = -0.01;
            while (startTime < 2)
            {
                
                transform.localPosition = transform.position + new Vector3(0, System.Convert.ToSingle(down), 0);
                startTime = startTime + Time.deltaTime;
            }
		}
	}

	void EnableMouvement(){
		mouvement = true;
		spr.color = Color.white;
	}

    void UpDateTextLife()
    {
        lifeText.text = "Vie restante : " + life.ToString();
    }
}
