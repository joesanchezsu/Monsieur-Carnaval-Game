using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinPoint : MonoBehaviour {

	public GameObject popUp;
	public Image background;

	private Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
		if(SceneManager.GetActiveScene().name == "SecondLevel"){
			anim.SetBool("secondLevel", true);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			col.GetComponent<PlayerController>().SetMoving(false);
			gameObject.SetActive(false);
			ShowPopUp();
		}
	}

	void ShowPopUp(){
		background.GetComponent<BackgroundController>().PopUpActive = true;
		popUp.SetActive(true);
	}
}
