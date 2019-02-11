using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KingController : MonoBehaviour {

	[SerializeField]
	string attackSound = "KingAttack";
    [SerializeField]
	string hitSound = "KingHit";
    [SerializeField]
	string dieSound = "KingDie";

	public int nbInitEnemies;
	public GameObject player;
	public GameObject alligatorClose;
	public GameObject alligatorOpen;
	public GameObject healthBar;
	public float healthMax = 6;
	public Rigidbody2D enemy1;
	public Rigidbody2D enemy2;
	public GameObject mode1, mode2, mode3;
	public bool isDead = false;
	public GameObject popUp;
	public Image background;

	private float timer = 0;
	private int attackFrecuency = 20; // seconds
	private Animator anim;
	private int nbEnemies;
	private bool isAttacking = false;
	private float health;
	AudioManager audioManager;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		nbEnemies = nbInitEnemies;
		health = healthMax;
		audioManager = AudioManager.instance;
		if(audioManager == null){
			Debug.LogError("No audiomanager found!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if((int)timer % attackFrecuency == 0 && !isAttacking && !isDead){
			Attack();
		} 
	}

	void Attack(){
		isAttacking = true;
		// Alligator changes
		Invoke("AlligatorOpenMouth", 2f);
		Invoke("AlligatorCloseMouth", 10f);
	    // Generate enemies
		Invoke("GenerateEnemies", 2f);
		anim.SetBool("attack", true);
		Invoke("FinishAttackAnim", 2f);
	}

	void AlligatorCloseMouth(){
		alligatorClose.SetActive(true);
		alligatorOpen.SetActive(false);
		isAttacking = false;
	}

	void AlligatorOpenMouth(){
		alligatorClose.SetActive(false);
		alligatorOpen.SetActive(true);
	}

	void GenerateEnemies(){
		if(nbEnemies > 6) nbEnemies = 3;

		for(int i = 0; i < nbEnemies; i++){
			Vector3 pos = new Vector3(9f + Random.Range(-2, 1), -4.6f, 0f);
			if(i % 2 == 0){
				Rigidbody2D clone = Instantiate(enemy1, pos, transform.rotation) as Rigidbody2D;
				clone.transform.localScale = new Vector3(Random.Range(-1, 1) < 0 ? -1 : 1, 1f, 1f);
			} else {
				Rigidbody2D clone = Instantiate(enemy2, pos, transform.rotation) as Rigidbody2D;
				clone.transform.localScale = new Vector3(Random.Range(-1, 1) < 0 ? -1 : 1, 1f, 1f);
			}
		}
		nbEnemies += 1;
	}

	void FinishAttackAnim(){
		audioManager.PlaySound(attackSound);
		anim.SetBool("attack", false);
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Arrow"){
			audioManager.PlaySound(hitSound);
			anim.SetBool("hit", true);
			Invoke("FinishHitAnim", 0.3f);
			healthBar.GetComponent<HealthBarKing>().TakeDamage(100 / healthMax);
			health--;
			if((int)health == 2 * (int)healthMax / 3){
				mode1.SetActive(false);
				mode2.SetActive(true);
			} else if((int)health == (int)healthMax / 3){
				//player.transform.parent = null;
				mode2.SetActive(false);
				mode3.SetActive(true);
			} else if(health <= 0){
				audioManager.PlaySound(dieSound);
				isDead = true;
				anim.SetBool("dead", true);
				Invoke("ShowPopUp", 3f);
			}

		}
	}

	void FinishHitAnim(){
		anim.SetBool("hit", false);
	}

	void ShowPopUp(){
		background.GetComponent<BackgroundController>().PopUpActive = true;
		popUp.SetActive(true);
		audioManager.StopSound("Music");
		audioManager.PlaySound("GameOver");
	}
}
