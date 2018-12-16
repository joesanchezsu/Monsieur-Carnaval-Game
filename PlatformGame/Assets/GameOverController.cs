﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	public Sprite spriteYes;
	public Sprite spriteNo;
	public Image answer;
	public Image background;
	private bool yesSelected = true;
	private bool restart = false;
	
	// Update is called once per frame
	void Update () {
		// Choice 
		if (Input.GetKeyDown(KeyCode.RightArrow) && yesSelected) {
			SelectNo(false);
        }
		if (Input.GetKeyDown(KeyCode.LeftArrow) && !yesSelected) {
			SelectYes(false);
        }

		// Answer by Keyboard
		if (Input.GetKeyDown(KeyCode.Return)) {
			if(yesSelected){
				restart = true;
			} else {
				restart = false;
			}
        }
	}
	
	public void ShowPopUp(){
		background.GetComponent<BackgroundController>().PopUpActive = true;
        gameObject.SetActive(true);
	}

	public void HidePopUp(){
		background.GetComponent<BackgroundController>().PopUpActive = false;
        gameObject.SetActive(false);
	}

	public void SelectYes(bool click){
		answer.sprite = spriteYes;
		yesSelected = true;
		if(click) restart = true;
	}

	public void SelectNo(bool click){
		answer.sprite = spriteNo;
		yesSelected = false;
		if(click) restart = false;
	}

	public bool GetRestart(){
		return restart;
	}
}
