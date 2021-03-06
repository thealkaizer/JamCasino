﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_KingController : MonoBehaviour {
    public float maxHP;
    public bool isAlive;
    public float currentHP;

    public SC_KingHealthBar healthBarScript;

	// Use this for initialization
	void Start () {
		this.currentHP = this.maxHP;
        healthBarScript.health = currentHP;
        this.isAlive = true;
	}

    public void takeDamage(float dammageValue) {
        this.currentHP -= dammageValue;
        this.currentHP = Mathf.Clamp(this.currentHP, 0, this.maxHP);
        healthBarScript.health = currentHP;
        if(this.currentHP <= 0f) {
            this.isAlive = false;
        }
    }
}
