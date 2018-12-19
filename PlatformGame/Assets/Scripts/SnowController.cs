using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowController : MonoBehaviour {

	public Image background;
	public float densityMax;
	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		var emission = ps.emission;
		float value = background.GetComponent<BackgroundController>().GetFog() * densityMax / 0.5f;
		//Debug.Log(value);
		emission.rateOverTime = value;
	}
}
