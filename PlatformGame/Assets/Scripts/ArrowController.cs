using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	void OnCollisionStay2D(Collision2D col){
		if(col.gameObject.CompareTag("Ground")){
			Invoke("DestroyArrow", 1f);
		}
		else if(col.gameObject.CompareTag("Enemy")){
			DestroyArrow();
		}
	}

	void DestroyArrow(){
		Destroy(gameObject);
	}

	void OnBecameInvisible(){
		DestroyArrow();
	}
}
