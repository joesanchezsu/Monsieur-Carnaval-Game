using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour {

	[SerializeField]
	string chestOpenSound = "ChestOpen";
	[SerializeField]
	string rewardGotSound = "RewardGot";

	public Sprite chestOpen;
	public Sprite heart, arrow;
	public GameObject reward;
	public GameObject player;

	private SpriteRenderer spriteRenderer;
	private bool isOpen = false;
	private int switchReward = 0;
	private int getReward = 0;
	private float posYStart;
	private float rewardWon = 0f;
	private bool heartWon = false;
	private bool arrowWon = false;
	private bool firstEnterCollision = true;
	AudioManager audioManager;

	// Use this for initialization
	void Start () {
		audioManager = AudioManager.instance;
		if(audioManager == null){
			Debug.LogError("No audiomanager found!");
		}
		spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
		posYStart = reward.transform.position.y;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		DiscoverReward();
	}

	void DiscoverReward(){
		if(isOpen){
			switchReward++;
			if(reward.transform.position.y - posYStart < 3.7){
				reward.transform.Rotate(0f, 10f, 0f);
				reward.transform.Translate(0f, 0.07f, 0f);
				
				if(switchReward < 10){
					reward.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
					reward.GetComponent<SpriteRenderer>().sprite = heart;
				} else if(switchReward < 20 && SceneManager.GetActiveScene().name == "SecondLevel"){
					reward.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
					reward.GetComponent<SpriteRenderer>().sprite = arrow;
				} else {
					switchReward = 0;
				}
			} else {
				getReward++;
				if(getReward < 30){
					if(rewardWon > 0){
						reward.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
						reward.GetComponent<SpriteRenderer>().sprite = heart;
						heartWon = true;
					} else {
						if (SceneManager.GetActiveScene().name == "SecondLevel"){
							reward.transform.localScale = new Vector3(1.3f, 1.3f, 1f);
							reward.GetComponent<SpriteRenderer>().sprite = arrow;
							arrowWon = true;
						} else {
							reward.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
							reward.GetComponent<SpriteRenderer>().sprite = heart;
							heartWon = true;
						}
					}
				} else if(getReward < 70){
					reward.transform.Rotate(0f, 0f, 20f);
				} else {
					reward.GetComponent<SpriteRenderer>().sprite = null;
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Player"){
			spriteRenderer.sprite = chestOpen;
			isOpen = true;
			rewardWon = Random.Range(-1f, 1f);
			col.SendMessage("GetCheckpoint", gameObject.transform.position);
			if(firstEnterCollision){
				audioManager.PlaySound(chestOpenSound);
				Invoke("WinReward", 3f);
				firstEnterCollision = false;
			}
		}
	}

	void WinReward(){
		if(heartWon){
			player.SendMessage("WinHeart");
		} else if(arrowWon) {
			player.SendMessage("WinArrow");
		}
		audioManager.PlaySound(rewardGotSound);
	}
}
