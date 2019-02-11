using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PopUpController : MonoBehaviour {

	public Image background;
	//public VideoPlayer video;
	//public Text skipTime;
	public Sprite popUp1;
	public Sprite popUp2;
	public Button toPopUp2;
	public Button closeBtn;
	//private float time = 5.0f;
	private Image image;
	private int popUpNumber = 0;

	void Start(){
		image = gameObject.GetComponent<Image>();
	}

	void Update(){
		// skipTime.text = "";
		// if(background.GetComponent<BackgroundController>().PopUpActive){
		// 	video.GetComponent<StreamVideo>().StartVideo();
		// 	int timeInt = (int)time;
		// 	if(time > 0){
		// 		skipTime.text = "Tu peux sauter la video en " + timeInt.ToString() + " secondes ";
		// 		time = time - 1 * Time.deltaTime;
		// 	} else {
		// 		closeBtn.gameObject.SetActive(true);
		// 	}
		// }
		if(Input.GetKeyDown(KeyCode.Return) && popUpNumber == 0){
			popUpNumber++;
			GetGiftArrow();
		} else if(Input.GetKeyDown(KeyCode.Return)){
			ClosePopUp();
		}
	}

	public void GetGiftArrow(){
		if(SceneManager.GetActiveScene().name == "FinalLevel"){
			ClosePopUp();
		} else {
			image.sprite = popUp2;
			toPopUp2.gameObject.SetActive(false);
			closeBtn.gameObject.SetActive(true);
		}
	}

	public void ClosePopUp(){
		background.GetComponent<BackgroundController>().PopUpActive = false;
		//video.Stop();
		gameObject.SetActive(false);
		if(SceneManager.GetActiveScene().name == "FirstLevel"){
			SceneManager.LoadScene("IndicationsArrow", LoadSceneMode.Single);
		} else if(SceneManager.GetActiveScene().name == "SecondLevel"){
			SceneManager.LoadScene("FinalLevel", LoadSceneMode.Single);
		} else if (SceneManager.GetActiveScene().name == "FinalLevel"){
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		}
		

	}
}
