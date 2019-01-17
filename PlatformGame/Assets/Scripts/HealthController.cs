using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour {

	public Image[] hearts;
	public Sprite fullHeart, halfHeart, emptyHeart;

	private int numOfHearts;
	private int health;

	void Update(){
		//if(health > numOfHearts*2) health = numOfHearts*2;

		for(int i = 0; i < hearts.Length; i++){
			if (health - 2*i > 1) hearts[i].sprite = fullHeart;
			else if (health - 2*i == 1) hearts[i].sprite = halfHeart;
			else hearts[i].sprite = emptyHeart;

			if(i < numOfHearts) hearts[i].enabled = true; 
			else hearts[i].enabled = false;
		}
	}

	public void SetHealth(int h){
		health = h;
		if(numOfHearts == 0) numOfHearts = h/2;
		if(health > numOfHearts*2 && health <= hearts.Length*2) numOfHearts++;
	}
}
