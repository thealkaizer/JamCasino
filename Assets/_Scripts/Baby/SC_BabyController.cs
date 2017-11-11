using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BabyController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes (Note: all public for debug purpose)
    // ------------------------------------------------------------------------
    public float cryDamagePerSecond;
    public float cryLoadingTimeInSecond;
    public float flySpeed;
    
    public bool isAlive;
    public bool isPreparingToCry;
    public bool isCrying;
    public bool isFlying; // If not flying, means holded by player

    public GameObject target;
    
    public float m_cryingDurationInSecond; //Time in second since baby started crying.
    public float m_timeBeforeStartCryingInSecond;

    
    // ------------------------------------------------------------------------
    // Unity methods
    // ------------------------------------------------------------------------
	
	// Update is called once per frame
	void Update() {
        if(this.isCrying) {
            // TODO: Play cry animation / sound --> (Cry ongoing)
            this.m_cryingDurationInSecond += Time.deltaTime;
        }
        else if(this.isPreparingToCry) {
            // TODO: Play animation / sound --> (prepare to cry ongoing)
            this.m_timeBeforeStartCryingInSecond -= Time.deltaTime;
            if(this.m_timeBeforeStartCryingInSecond <= 0) {
                this.StartCrying();
            }
        }
	}

    void FixedUpdate() {
        if (this.hasTarget()) {
            // TODO Update movement
            this.updateMovement();
        }
    }
    
    
    // ------------------------------------------------------------------------
    // Crying methods
    // ------------------------------------------------------------------------
    public void StartPrepareToCry() {
        if (!this.isCrying) {
            // TODO: Add feedback! (Sound, anim etc...)
            Debug.Log("Baby prepare to cry...");
            this.isPreparingToCry = true;
            this.m_timeBeforeStartCryingInSecond = cryLoadingTimeInSecond;
        }
    }

    public void StartCrying() {
        if (!this.isCrying) {
            // TODO: Add feedback! (Sound, anim etc...)
            Debug.Log("Baby start crying...");
            this.isPreparingToCry = false;
            this.isCrying = true;
            this.m_cryingDurationInSecond = 0;
        }
    }

    public void KillPoorBaby() {
        if (this.isAlive) {
            // TODO: Stop game + all crap
            this.isAlive = false;
            this.isPreparingToCry = false;
            this.isCrying = false;
            this.isFlying = false;
        }
    }
    
    
    // ------------------------------------------------------------------------
    // Move methods
    // ------------------------------------------------------------------------
    public void FlyToTarget(GameObject target) {
        this.target = target;
        this.isFlying = true;
        this.isPreparingToCry = false;
        this.isCrying = false;
    }

    public void UnsetTarget() {
        // Weird, but this is the internal way to say there is no more target.
        this.isFlying = false;
    }

    private void updateMovement() {
        Vector3 dir = this.target.transform.position - this.transform.position;
        dir = Vector3.Normalize(dir);
        Debug.DrawRay(this.transform.position, dir, Color.blue, 0.5f);
        this.transform.position = this.transform.position + dir * flySpeed * Time.deltaTime;
    }
    
    
    // ------------------------------------------------------------------------
    // Getter / Setter
    // ------------------------------------------------------------------------
    public bool hasTarget() {
        return this.isFlying;
    }
}
