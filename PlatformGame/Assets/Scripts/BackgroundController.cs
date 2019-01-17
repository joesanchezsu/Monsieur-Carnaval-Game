using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

	public bool PopUpActive {get; set;}
	public bool GameOverActive{get; set;}
	private float fog = 0.5f; // decrease killing enemies
	private float decrease = 0.03f;
	
	// Use this for initialization
	void Start () {
		PopUpActive = false;
		GameOverActive = false;
		GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(PopUpActive){
			GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
		} else if(GameOverActive){
			GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);
		} else {
			GetComponent<Image>().color = new Color(1f, 1f, 1f, fog);
		} 
	}

	public void SetFogByEnemy(){
		fog -= decrease;
	}

	public float GetFog(){
		return fog;
	}
}
