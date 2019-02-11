using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGround : MonoBehaviour {

	private PlayerController player;
	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<PlayerController>();
		rb = GetComponentInParent<Rigidbody2D>();
		
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "Platform"){
			rb.velocity = new Vector3(0f, 0f, 0f);
			if(SceneManager.GetActiveScene().name != "FinalLevel"){
				player.transform.parent = col.transform; // this object (platform) becomes his father and he can move with
			}			
			player.grounded = true;
		}
	}
	
	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.CompareTag("Ground")){
			player.grounded = true;
		}
		if(col.gameObject.tag == "Platform"){
			if(SceneManager.GetActiveScene().name != "FinalLevel"){
				player.transform.parent = col.transform; // this object (platform) becomes his father and he can move with
			}
			player.grounded = true;
		}
	}

	void OnCollisionExit2D(Collision2D col){
		if(col.gameObject.tag == "Ground"){
			player.grounded = false;
		}
		if(col.gameObject.tag == "Platform"){
			player.transform.parent = null;
			player.grounded = false;
		}
	}
}
