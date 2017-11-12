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
    public GameObject meteoraPrefab;
    
    public float meteoreFallingSpeed;
    public int numberMeteoraPerPhase;
    public float timeBetweenTwoShots;

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
        float randomIndex = 0;
        do {
            // This is to get only valid existing position.
            // Dev note: game may freeze if all tiles destroyed, but this can't occure in our case of gameplay.
            randomIndex = Random.Range(0f, this.landingTargets.Length);
        } while (this.landingTargets[(int)randomIndex] == null);
        return this.landingTargets[(int)randomIndex];
    }

    private void launchOneMeteora() {
        Transform targetTransform = this.getRandomTransform();
        Debug.DrawLine(this.meteoraLauncherBarrel.position, targetTransform.position, Color.yellow, 2);
        this.m_timeAtLastLaunch = Time.time;
        this.m_currentNbLaunchedMeteora++;
        
        GameObject obj = Instantiate(this.meteoraPrefab, this.meteoraLauncherBarrel.position, Quaternion.identity) as GameObject;
        SC_MeteoraController mc = obj.GetComponent<SC_MeteoraController>();
        mc.target = targetTransform;
        mc.fallingSpeed = this.meteoreFallingSpeed;
    }

    private bool timeToLaunchAnotherMeteore() {
        return (Time.time - this.m_timeAtLastLaunch) >= this.timeBetweenTwoShots;
    }
}
