using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_KingHealthBar : MonoBehaviour {
	public Image bar;
	public float health;
    
	// Update is called once per frame
	void Update () {
		bar.fillAmount = this.health  / 246;
	}
}
