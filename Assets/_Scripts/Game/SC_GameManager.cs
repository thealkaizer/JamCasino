using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameManager : MonoBehaviour {
    public SC_BabyController babyController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!this.babyController.isAlive) {
            Debug.Log("GameOver: Baby just die!");
            // TODO: Game Over right Now!
        }
	}
}
