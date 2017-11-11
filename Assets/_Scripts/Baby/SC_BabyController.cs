using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SC_BabyController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes (Note: all public for debug purpose)
    // ------------------------------------------------------------------------
    public float cryDamagePerSecond;
    public float cryLoadingTimeInSecond;

    public float jumpPower;
    public float jumpDuration;
    public int jumpNumberOfJumps;
    
    public bool isAlive;
    public bool isPreparingToCry;
    public bool isCrying;
    public bool isJumping; // If not flying, means holded by player

    public GameObject target;
    
    public float m_cryingDurationInSecond; //Time in second since baby started crying.
    public float m_timeBeforeStartCryingInSecond;

    
    // ------------------------------------------------------------------------
    // Unity methods
    // ------------------------------------------------------------------------
    void Awake() {
        this.target = null;
    }
	
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
        if (this.HasTarget() && !this.isJumping) {
            this.transform.position = this.target.transform.position;
        }
        else if(this.isJumping) {
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
            this.isAlive            = false;
            this.isPreparingToCry   = false;
            this.isCrying           = false;
            this.isJumping          = false;
        }
    }
    
    
    // ------------------------------------------------------------------------
    // Move methods
    // ------------------------------------------------------------------------
    public void FlyToTarget(GameObject target) {
        // TODO: animationm + sound for baby that start to fly
        this.target             = target;
        this.isJumping          = true;
        this.isPreparingToCry   = false;
        this.isCrying           = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.ApplyJump();
    }

    public void StickToTarget(GameObject target) {
        this.target             = target;
        this.isJumping          = false;
        this.isCrying           = false;
        this.StartPrepareToCry();
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ApplyJump() {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 targetPosition = GameObject.FindGameObjectWithTag(this.target.tag).transform.position;
        Debug.DrawLine(this.transform.position, targetPosition, Color.cyan, 1);
        this.transform.DOJump(targetPosition, jumpPower, jumpNumberOfJumps, jumpDuration);
    }
    
    
    // ------------------------------------------------------------------------
    // Getter / Setter
    // ------------------------------------------------------------------------
    public bool HasTarget() {
        return this.target != null;
    }
}
