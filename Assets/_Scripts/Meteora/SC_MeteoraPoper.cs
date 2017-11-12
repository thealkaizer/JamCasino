using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MeteoraPoper : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes
    // ------------------------------------------------------------------------
    public bool isRunning;

    public Transform[] landingTargets;
    public Transform meteoraLauncherBarrel;

    public int numberMeteoraPerPhase;
    public float timeBetweenTwoShots;
    public float meteoreFallingDurationInSecond;

    private int m_currentNbLaunchedMeteora;
    private float m_timeAtLastLaunch;

    
    // ------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------
    void Start() {
        this.isRunning = false;
    }

    public void Update() {
        if(this.isRunning) {
            if(this.m_currentNbLaunchedMeteora >= this.numberMeteoraPerPhase) {
                this.isRunning = false;
            }
            else if(this.timeToLaunchAnotherMeteore()) {
                this.launchOneMeteora();
            }
        }
        else {
            this.m_currentNbLaunchedMeteora = 0;
        }
    }
    

    // ------------------------------------------------------------------------
    // Meteora (Like Linking park) Methods
    // ------------------------------------------------------------------------
    private Transform getRandomTransform() {
        float randomIndex = Random.Range(0f, this.landingTargets.Length);
        return this.landingTargets[(int)randomIndex];
    }

    private void launchOneMeteora() {
        Transform targetTransform = this.getRandomTransform();
        Debug.DrawLine(this.meteoraLauncherBarrel.position, targetTransform.position, Color.yellow, 2);
        this.m_timeAtLastLaunch = Time.time;
        // TODO
    }

    private bool timeToLaunchAnotherMeteore() {
        return (Time.time - this.m_timeAtLastLaunch) >= this.timeBetweenTwoShots;
    }
}
