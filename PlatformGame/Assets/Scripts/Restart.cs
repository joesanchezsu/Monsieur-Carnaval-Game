using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
 public class Restart : MonoBehaviour
 {
	public bool Replay {get; set;}

	void Start(){
		Replay = false;
	}
	void Update (){
		if(Replay){
			SceneManager.LoadScene(SceneManager.GetActiveScene().name );
		}
	}
 }
