using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFalling : MonoBehaviour {

	public float fallDelay = 1f;
	public float respawnDelay = 5f;

	private Rigidbody2D rb;
	private PolygonCollider2D pc;
	private Vector3 start;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		pc = GetComponent<PolygonCollider2D>();
		start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Player")){
			Invoke("Fall", fallDelay);
			Invoke("Respawn", fallDelay + respawnDelay);
		}
	}

	void Fall(){
		rb.isKinematic = false;
		pc.isTrigger = true;
	}

	void Respawn(){
		transform.position = start;
		rb.isKinematic = true;
		rb.velocity = Vector3.zero;
		pc.isTrigger = false;
	}
}
