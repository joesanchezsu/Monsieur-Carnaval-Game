using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCounter : MonoBehaviour {

	public int numOfArrows;
	public Image dozens, units;
	public Sprite[] digits;
	private int dozensDigit, unitsDigit;
	private int recharge = 5;
	
	// Update is called once per frame
	void Update () {
		dozensDigit = numOfArrows / 10;
		unitsDigit = numOfArrows % 10;
		
		dozens.sprite = digits[dozensDigit];
		units.sprite = digits[unitsDigit];
	}

	public void Decrease(){
		numOfArrows--;
	}

	public void Recharge(){
		numOfArrows += recharge;
	}

	public int GetNumOfArrows(){
		return numOfArrows;
	}
}
