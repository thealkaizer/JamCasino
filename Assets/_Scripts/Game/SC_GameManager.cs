﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameManager : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes
    // ------------------------------------------------------------------------
    public SC_BabyController babyController;
    public SC_KingController kingController;
    public SC_BulletPattern1Creation_GD bulletPhase1;

    public float phase1DurationInSecond;
    public float phase2DurationInSecond;
    
    public int m_currentPhase;
    public int m_lastPhase;
    

    // ------------------------------------------------------------------------
    // Unity methods
    // ------------------------------------------------------------------------
    public void Start() {
        this.m_currentPhase = 1;
        this.m_lastPhase = 1;
    }
    

	// Update is called once per frame
	void Update () {
		if(!this.babyController.isAlive) {
            Debug.Log("GameOver: Baby just die! You suck!!");
            Time.timeScale = 0.0f;
            // TODO: Call Game Over right Now!
        }
        else if(!this.kingController.isAlive) {
            Debug.Log("GG Fucker!!");
            Time.timeScale = 0.0f;
            // TODO: Call GG panel Right Now!
        }

        if(this.babyController.isCrying) {
            this.kingController.takeDamage(this.babyController.getCurrentStageDamage() * Time.deltaTime);
        }
        
        this.updatePhaseData();
        this.processPhaseSwitching();
	}
    

    // ------------------------------------------------------------------------
    // Phase methods
    // ------------------------------------------------------------------------
    private void updatePhaseData() {
        float timeWholeSequenceInSec = this.phase2DurationInSecond + this.phase1DurationInSecond;
        float currentPosInSequence = Time.time % timeWholeSequenceInSec;

        this.m_lastPhase = this.m_currentPhase;
        bool isSecondPhase = (timeWholeSequenceInSec - currentPosInSequence <= this.phase2DurationInSecond);
        this.m_currentPhase = isSecondPhase ? 2 : 1;
    }

    private void processPhaseSwitching() {
        if(this.m_currentPhase != this.m_lastPhase) {
            if(this.m_currentPhase == 1) {
                this.startPhase1();
            }
            else {
                this.startPhase2();
            }
        }
    }

    private void startPhase1() {
        // TODO: Play event (Back to phase 1)
        Debug.Log("Start phase 1");
    }

    private void startPhase2() {
        // TODO: Play event cuz we just entered phase 2!!
        Debug.Log("Start phase 2");
    }
}
