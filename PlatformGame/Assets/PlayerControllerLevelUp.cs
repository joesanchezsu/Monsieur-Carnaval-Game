using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerLevelUp : MonoBehaviour {
    //public Text lifeText;
    public GameObject ennemy;
    float time = 0.0f;
    int randonTime = 4;
    public Vector3 positionEnnemy = new Vector3(8, 6, 0);

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
         if (ennemy != null)
         {
             time = time + Time.deltaTime;
             if (time > randonTime)
             {
                 Instantiate(ennemy, positionEnnemy, transform.rotation);
                 randonTime = Random.Range(5, 15);
                 time = 0;
                 Debug.Log(randonTime);
                 Debug.Log(positionEnnemy);
             }
         }

    }
}
