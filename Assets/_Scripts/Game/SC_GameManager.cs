using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_GameManager : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes
    // ------------------------------------------------------------------------
    public SC_BabyController babyController;
    public SC_KingController kingController;
    public SC_PlayerController playerControllerP1;
    public SC_PlayerController playerControllerP2;
    public SC_BulletPattern1Creation_GD bulletPhase1ManagerHand1;
    public SC_BulletPattern1Creation_GD bulletPhase1ManagerHand2;
    public SC_MeteoraPoper meteoraPhase2Manager;
    public SC_GameOverUI gameOverUI;

    public float phase1DurationInSecond;
    public float phase2DurationInSecond;

    public bool isVictory;
    public bool isGameOver;
    
    public int m_currentPhase;
    public int m_lastPhase;
    

    // ------------------------------------------------------------------------
    // Unity methods
    // ------------------------------------------------------------------------
    public void Start() {
        this.m_currentPhase = 1;
        this.m_lastPhase = 1;
        this.isVictory = false;
        this.isGameOver = false;
        Time.timeScale = 1.0f;
    }
    

	// Update is called once per frame
	void Update () {
        if (this.isGameOver) {
            Debug.Log("GameOver: Baby just die! You suck!!");
            this.PauseControls();
            this.gameOverUI.showGameOver();
            // TODO: Call Game Over right Now!
        }
        else if(this.isVictory) {
            Debug.Log("GG Fucker!!");
            this.PauseControls();
            Time.timeScale = 0.0f;
            // TODO: Call GG panel Right Now!
        }
		else if(!this.babyController.isAlive) {
            this.isGameOver = true;
        }
        else if(!this.kingController.isAlive) {
            this.isVictory = true;
        }
        else if(this.babyController.isCrying) {
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
        this.bulletPhase1ManagerHand1.canPlay = true;
        this.bulletPhase1ManagerHand2.canPlay = true;
        this.meteoraPhase2Manager.isRunning = false;
    }

    private void startPhase2() {
        // TODO: Play event cuz we just entered phase 2!!
        Debug.Log("Start phase 2");
        this.bulletPhase1ManagerHand1.canPlay = false;
        this.bulletPhase1ManagerHand2.canPlay = false;
        this.meteoraPhase2Manager.isRunning = true;
    }
    

    // ------------------------------------------------------------------------
    // Control methods methods
    // ------------------------------------------------------------------------
    public void StartControls() {
        this.playerControllerP1.enableAllControls();
        this.playerControllerP2.enableAllControls();
        this.babyController.enableAllControls();
    }

    public void PauseControls() {
        this.playerControllerP1.disableAllControls();
        this.playerControllerP2.disableAllControls();
        this.babyController.disableAllControls();
    }
}
