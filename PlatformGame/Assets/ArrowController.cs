using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Enemy")){
			Invoke("DestroyArrow", 1f);
		}
	}

	void DestroyArrow(){
		Destroy(gameObject);
	}

	void OnBecameInvisible(){
		DestroyArrow();
	}
}
