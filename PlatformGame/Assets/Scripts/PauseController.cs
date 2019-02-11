// <div>Icons made by <a href="https://www.flaticon.com/authors/smashicons" title="Smashicons">Smashicons</a> from <a href="https://www.flaticon.com/" 			    
// title="Flaticon">www.flaticon.com</a> is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" 			   
// title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a></div>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour {

	public Sprite play;
	public Sprite pause;

	private Image image;
	private bool switched = false;

	void Start(){
		image = GetComponent<Image>();
	}
	// Update is called once per frame
	void Update () {
		if(switched){
			if(Time.timeScale == 1){
				image.sprite = play;
				Time.timeScale = 0; // pause the game
			} else {
				image.sprite = pause;
				Time.timeScale = 1;
			}
			switched = false;
		}
	}

	public void Switch(){
		switched = true;
	}
}
