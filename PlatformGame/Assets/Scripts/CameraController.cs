using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour {

	public GameObject targetToFollow;
	public Vector2 minCamPos, maxCamPos;
	public float smoothTime;

	private Vector2 velocity;
	private float lastPosY = -6f;
	private int switchDie = 0;
	private bool switchCount = false;
	private float minCamPosYDefault = -5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		// if(targetToFollow.transform.position.y < transform.position.y - 5 && switchDie < 1){
		// 	targetToFollow.GetComponent<PlayerController>().OnBecameInvisible();
		// 	switchDie++;
		// }

		float posX = Mathf.SmoothDamp(transform.position.x,
		 targetToFollow.transform.position.x, ref velocity.x, smoothTime);
		
		float posY = Mathf.SmoothDamp(transform.position.y,
		 targetToFollow.transform.position.y, ref velocity.y, smoothTime);
		
		if(SceneManager.GetActiveScene().name == "FirstLevel"){
			if(posY < lastPosY && !targetToFollow.GetComponent<PlayerController>().isRespawn){
				posY = lastPosY;
			} 
			if(targetToFollow.GetComponent<PlayerController>().isRespawn){
				minCamPos.y = minCamPosYDefault;
			}
			
			if(posY - minCamPos.y > 3f ){
				minCamPos.y = posY - 3f;
			}
			if(switchDie > 100){
				switchDie = 0;
				switchCount = false;
			}
			if(targetToFollow.transform.position.y < minCamPos.y - 3f && switchDie < 1 && !targetToFollow.GetComponent<PlayerController>().isDead){
				targetToFollow.GetComponent<PlayerController>().DeadByFalling();
				switchCount = true;
			}
			if(switchCount){
				switchDie++;
			}
		}
		
		transform.position = new Vector3(
			Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), 
			Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), 
			transform.position.z); 

		// if(targetToFollow.GetComponent<PlayerController>().isRespawn || posY - minCamPos.y > 5 ){
		// 	minCamPos.y = posY - 5;
		// 	targetToFollow.GetComponent<PlayerController>().isRespawn = false;
		// }

		lastPosY = posY;
	}
}
