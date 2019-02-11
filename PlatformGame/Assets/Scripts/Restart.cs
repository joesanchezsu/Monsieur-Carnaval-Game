using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class Restart : MonoBehaviour
 {
	public bool Replay {get; set;}
	public bool GoHome {get; set;}

	void Start(){
		Replay = false;
		GoHome = false;
	}
	void Update (){
		if(Replay){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name );
		}
		if(GoHome){
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
		}
	}
 }
