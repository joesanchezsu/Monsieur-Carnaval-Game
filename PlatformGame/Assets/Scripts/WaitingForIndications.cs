using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingForIndications : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("StartSecondLevel", 10f);
	}
	
	void StartSecondLevel () {
		SceneManager.LoadScene("SecondLevel", LoadSceneMode.Single);
	}
}
