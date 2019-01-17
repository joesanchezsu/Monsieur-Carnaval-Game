using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

	public Sprite[] clouds;
	public SpriteRenderer[] images; 

	// Use this for initialization
	void Start () {
		for(int i = 0; i < images.Length; i++){
			images[i].sprite = clouds[(int)Random.Range(0f, 3f)];
			images[i].transform.position = new Vector3(Random.Range(-20f, 10f), Random.Range(-9f, 9f), 0f);
			float cloudScale = Random.Range(4f, 7f);
			images[i].transform.localScale = new Vector3(cloudScale, cloudScale, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
