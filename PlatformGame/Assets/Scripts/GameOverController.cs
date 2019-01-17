using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour {

	[SerializeField]
	string hoverOverSound = "ButtonHover";

	[SerializeField]
	string pressButtonSound = "ButtonPress";

	public Sprite spriteYes;
	public Sprite spriteNo;
	public Image answer;
	public Image background;
	private bool yesSelected = true;
	private bool restart = false;
	private bool quit = false;
	AudioManager audioManager;

	void Start(){
		audioManager = AudioManager.instance;
		if(audioManager == null){
			Debug.LogError("No audiomanager found!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Choice 
		if (Input.GetKeyDown(KeyCode.RightArrow) && yesSelected) {
			SelectNo(false);
			OnMouseOver();
			
        }
		if (Input.GetKeyDown(KeyCode.LeftArrow) && !yesSelected) {
			SelectYes(false);
			OnMouseOver();
        }

		// Answer by Keyboard
		if (Input.GetKeyDown(KeyCode.Return)) {
			audioManager.PlaySound(pressButtonSound);
			if(yesSelected){
				restart = true;
			} else {
				quit = true;
			}
        }
	}
	
	public void ShowPopUp(){
		background.GetComponent<BackgroundController>().GameOverActive = true;
        gameObject.SetActive(true);
		restart = false;
		quit = false;
	}

	public void HidePopUp(){
		background.GetComponent<BackgroundController>().GameOverActive = false;
        gameObject.SetActive(false);
	}

	public void SelectYes(bool click){
		answer.sprite = spriteYes;
		yesSelected = true;
		if(click) {
			restart = true;
			audioManager.PlaySound(pressButtonSound);
		}
	}

	public void SelectNo(bool click){
		answer.sprite = spriteNo;
		yesSelected = false;
		if(click) {
			quit = true;
			audioManager.PlaySound(pressButtonSound);
		}
	}

	public bool GetRestartLevel(){
		return restart;
	}

	public bool GetQuitLevel(){
		return quit;
	}

	public void OnMouseOver(){
		audioManager.PlaySound(hoverOverSound);
	}
}
