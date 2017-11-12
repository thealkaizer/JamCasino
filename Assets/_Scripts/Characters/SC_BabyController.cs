using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SC_BabyController : MonoBehaviour {

    // ------------------------------------------------------------------------
    // Attributes
    // ------------------------------------------------------------------------
    public float cryDamagePerSecondStage1;
    public float cryDamagePerSecondStage2;
    public float cryDamagePerSecondStage3;

    public float timeBeforeStartCryStage2;
    public float timeBeforeStartCryStage3;

    public float cryLoadingTimeInSecond;

    public float jumpSpeed;
    public Animator cryingAnim;
    
    public bool isAlive;
    public bool isPreparingToCry;
    public bool isCrying;
    public bool isJumping; // If not flying, means holded by player

    private float m_cryingDurationInSecond; //Time in second since baby started crying.
    private float m_timeBeforeStartCryingInSecond;

    private int m_previousCryStage;
    private int m_currentCryStage;

    private GameObject target;

    
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
        this.updateCurrentCryingStage();
        this.showCryingStageUI();
	}

    void FixedUpdate() {
        if (this.HasTarget() && !this.isJumping) {
            Transform babyBucket = this.target.GetComponent<SC_PlayerController>().babyBucket.transform;
            this.transform.position = babyBucket.position;
            this.transform.rotation = Quaternion.LookRotation(babyBucket.forward, Vector3.up);
        }
        else if(this.isJumping) {
            this.UpdateMovement();
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
            AkSoundEngine.PostEvent("Play_Baby_cries", gameObject);
            Debug.Log("Baby start crying...");
            this.isPreparingToCry = false;
            this.isCrying = true;
            this.m_cryingDurationInSecond = 0;
        }
    }

    public void KillPoorBaby() {
        if (this.isAlive) {
            AkSoundEngine.PostEvent("Play_Baby_hit", gameObject);
            // TODO: Stop game + all crap
            this.isAlive            = false;
            this.isPreparingToCry   = false;
            this.isCrying           = false;
            this.isJumping          = false;
        }
    }

    private void updateCurrentCryingStage() {
        this.m_previousCryStage = this.m_currentCryStage;
        this.m_currentCryStage = this.getCurrentCryingStage();
    }

    private void showCryingStageUI() {
        if(this.m_currentCryStage != this.m_previousCryStage) {
            Debug.Log("Cry stage just changed from " + this.m_previousCryStage + " to " + this.m_currentCryStage);
            cryingAnim.SetInteger("crypower", this.m_currentCryStage);
        }
    }
    
    
    // ------------------------------------------------------------------------
    // Move methods
    // ------------------------------------------------------------------------
    public void FlyToTarget(GameObject target) {
        
        // TODO: animationm + sound for baby that start to fly
        AkSoundEngine.PostEvent("Stop_Baby_cries", gameObject);
        this.target             = target;
        this.isJumping          = true;
        this.isPreparingToCry   = false;
        this.isCrying           = false;
        this.GetComponent<Rigidbody>().isKinematic = false;
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(5,5,5);
    }

    public void StickToTarget(GameObject target) {
        this.target             = target;
        this.isJumping          = false;
        this.isCrying           = false;
        this.StartPrepareToCry();
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void UpdateMovement() {
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 targetPosition = GameObject.FindGameObjectWithTag(this.target.tag).transform.position;
        Vector3 dir = targetPosition - this.transform.position;
        dir = Vector3.Normalize(dir);
        Debug.DrawRay(this.transform.position, dir, Color.blue, 0.5f);
        Debug.DrawLine(this.transform.position, targetPosition, Color.cyan);
        this.transform.position = this.transform.position + (dir * jumpSpeed * Time.deltaTime);
    }
    
    
    // ------------------------------------------------------------------------
    // Getter / Setter
    // ------------------------------------------------------------------------
    public bool HasTarget() {
        return this.target != null;
    }

    public string getTargetTag() {
        return this.target.tag;
    }

    public float getCurrentStageDamage() {
        if (!this.isCrying) {
            return 0;
        }
        else if(this.m_cryingDurationInSecond <= this.timeBeforeStartCryStage2) {
            return this.cryDamagePerSecondStage1;
        }
        else if(this.m_cryingDurationInSecond <= this.timeBeforeStartCryStage3) {
            return this.cryDamagePerSecondStage2;
        }
        else {
            return this.cryDamagePerSecondStage3;
        }
    }

    public int getCurrentCryingStage() {
        if (!this.isCrying) {
            return 0;
        }
        else if(this.m_cryingDurationInSecond <= this.timeBeforeStartCryStage2) {
            return 1;
        }
        else if(this.m_cryingDurationInSecond <= this.timeBeforeStartCryStage3) {
            return 2;
        }
        else {
            return 3;
        }
    }
}
