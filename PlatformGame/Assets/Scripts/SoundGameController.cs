// <div>Icons made by <a href="https://www.flaticon.com/authors/smashicons" title="Smashicons">Smashicons</a> from <a href="https://www.flaticon.com/" 			    
// title="Flaticon">www.flaticon.com</a> is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" 			    
// title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a></div>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundGameController : MonoBehaviour {

	public Sprite soundInactive;
	public Sprite soundActive;

	private Image image;
	private bool switched = false;

	// Use this for initialization
	void Start () {
		image = GetComponent<Image>();
	}

	public void Switch(){
		switched = !switched;
		if(switched){
			image.sprite = soundInactive;
		} else {
			image.sprite = soundActive;
		}
	}
}
