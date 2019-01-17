using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PopUpController : MonoBehaviour {

	public Image background;
	public VideoPlayer video;
	public Text skipTime;
	public Button closeBtn;
	private float time = 5.0f;

	void Update(){
		skipTime.text = "";
		if(background.GetComponent<BackgroundController>().PopUpActive){
			video.GetComponent<StreamVideo>().StartVideo();
			int timeInt = (int)time;
			if(time > 0){
				skipTime.text = "Tu peux sauter la video en " + timeInt.ToString() + " secondes ";
				time = time - 1 * Time.deltaTime;
			} else {
				closeBtn.gameObject.SetActive(true);
			}
			
		}
		
		if(Input.GetKeyDown(KeyCode.Return)){
			ClosePopUp();
		}
	}

	public void ClosePopUp(){
		background.GetComponent<BackgroundController>().PopUpActive = false;
		video.Stop();
		gameObject.SetActive(false);
		 SceneManager.LoadScene("SecondLevel", LoadSceneMode.Single);

	}
}
