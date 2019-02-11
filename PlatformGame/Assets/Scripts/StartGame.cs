using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

	public Sprite menu, rules;
	private Image bg;

	void Start(){
		bg = gameObject.GetComponent<Image>();
		bg.sprite = menu;
	}

	public void GoToRules(){
		if(SceneManager.GetActiveScene().name == "Menu"){
			Invoke("StartFirstLevel", 10);
			bg.sprite = rules;
		} 
	}

	void StartFirstLevel(){
		SceneManager.LoadScene("FirstLevel", LoadSceneMode.Single);
	}

}
