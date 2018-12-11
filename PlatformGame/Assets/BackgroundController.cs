using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour {

	public bool PopUpActive {get; set;}
	
	// Use this for initialization
	void Start () {
		PopUpActive = false;
		GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		if(PopUpActive){
			GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
		} else {
			GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
		} 
	}
}
