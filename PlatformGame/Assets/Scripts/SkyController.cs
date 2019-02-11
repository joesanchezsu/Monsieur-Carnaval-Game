using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

	public float backgroundSize;
	public float paralaxSpeed;
	public Sprite[] clouds;
	public SpriteRenderer[] images; 

	private Transform cameraTransform;
	private Transform layer;
	private float viewZone = 0;
	//private int leftIndex, rightIndex;
	private float lastCameraY;

	// Use this for initialization
	private void Start () {
		for(int i = 0; i < images.Length; i++){
			images[i].sprite = clouds[(int)Random.Range(0f, 3f)];
			images[i].transform.position = new Vector3(Random.Range(-20f, 10f), Random.Range(-9f, 9f), 0f);
			float cloudScale = Random.Range(4f, 7f);
			images[i].transform.localScale = new Vector3(cloudScale, cloudScale, 0f);
		}

		cameraTransform = Camera.main.transform;
		lastCameraY = cameraTransform.position.y;
		layer = transform.GetChild(0);
	}
	
	// Update is called once per frame
	private void Update () {
		float deltaY = cameraTransform.position.y - lastCameraY;
		//Debug.Log(deltaY);
		transform.position += Vector3.up * (deltaY * paralaxSpeed);
		lastCameraY = cameraTransform.position.y;

		if(cameraTransform.position.y > (layer.transform.position.y - viewZone)){
			layer.position = Vector3.up * (layer.position.y + backgroundSize);
		}
	}
}
