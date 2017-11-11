using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameManager : MonoBehaviour {
    public SC_BabyController babyController;
    public SC_KingController kingController;
	
	// Update is called once per frame
	void Update () {
		if(!this.babyController.isAlive) {
            Debug.Log("GameOver: Baby just die!");
            Time.timeScale = 0.0f;
            // TODO: Game Over right Now!
        }
        else if(!this.kingController.isAlive) {
            Debug.Log("GG Fucker!!");
            Time.timeScale = 0.0f;
        }

        if(this.babyController.isCrying) {
            Debug.DrawLine(Vector3.zero, new Vector3(666,666,666), Color.red, 0.5f);
            this.kingController.takeDamage(this.babyController.cryDamagePerSecond * Time.deltaTime);
        }
	}
}
