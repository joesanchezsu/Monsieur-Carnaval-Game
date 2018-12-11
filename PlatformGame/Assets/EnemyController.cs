using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float speed = 1f; // 2 variables because speed changes the direction
	public float maxSpeed = 1f;
    float time = 0.0f;
    int randonTime = 4;
    public GameObject ennemy; 

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        time = time + Time.deltaTime;
		rb.AddForce(Vector2.right * speed);

		float limitedSpeed = Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed);
		rb.velocity = new Vector2(limitedSpeed, rb.velocity.y);

		if(rb.velocity.x > -0.01f && rb.velocity.x < 0.01f){
			speed = -speed;
			transform.localScale = new Vector3(speed > 0 ? -1 : 1, 1, 1);
			rb.velocity = new Vector2(speed, rb.velocity.y);
		}
        if (time > randonTime)
        {
            Instantiate(ennemy, transform.TransformPoint(System.Convert.ToSingle(0), 6, 0), transform.rotation);
            //randonTime = Random.RandomRange(5, 15);
            time = 0;
            Debug.Log(randonTime);
            Debug.Log(time);
        }
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			float yOffset = 0.3f;
			if(transform.position.y + yOffset < col.transform.position.y){
				col.SendMessage("EnemyJump");
				Destroy(gameObject);
			} else {
				col.SendMessage("EnemyKnockBack", transform.position.x);
			}
		}
	}
}
