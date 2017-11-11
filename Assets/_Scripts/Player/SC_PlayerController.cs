using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes (Note: all public for debug purpose)
    // ------------------------------------------------------------------------
    public float speedNormal;
    public float speedBaby;
    public bool hasBaby;

    public GameObject player2; // Ref to the other player.
    public GameObject baby;
    public GameObject groundRing;
    public GameObject babyBucket;

    public bool isWalking;

    private float m_effectiveSpeed;
    
    private string m_btn_fire1;
    private string m_btn_vertical;
    private string m_btn_horizontal;

    
    // ------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------
    void Awake() {
        this.hasBaby = false;
        this.isWalking = false;
        this.m_effectiveSpeed = this.speedNormal;
    }

    void Start() {
        if(this.gameObject.CompareTag("Player1")) {
            this.m_btn_fire1        = "Fire1";
            this.m_btn_horizontal   = "Horizontal";
            this.m_btn_vertical     = "Vertical";
        }
        else {
            this.m_btn_fire1        = "p2_Fire1";
            this.m_btn_horizontal   = "p2_Horizontal";
            this.m_btn_vertical     = "p2_Vertical";
        }
    }

    void Update() {
        if (Input.GetButton(this.m_btn_fire1)) {
            this.TossBaby();
        }
    }
	
	void FixedUpdate() {
        float horizontal    = Input.GetAxis(this.m_btn_horizontal);
        float vertical      = Input.GetAxis(this.m_btn_vertical);
        this.HandleMovement(horizontal, vertical);
	}

    
    // ------------------------------------------------------------------------
    // Move Methods
    // ------------------------------------------------------------------------
    private void HandleMovement(float horizontal, float vertical) {
        this.isWalking = false;

        if(horizontal  != 0.0f || vertical != 0.0f) {
            this.m_effectiveSpeed = this.hasBaby ? this.speedBaby : this.speedNormal;
            // TODO ANIMATION: place walking animation
            this.isWalking = true;
            this.Rotate(horizontal, vertical);
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            movement *= Time.deltaTime * m_effectiveSpeed;
            transform.position +=  movement;
            Debug.DrawRay(transform.position, movement, Color.blue, 1.0f);
        }
    }

    private void Rotate(float horizontal, float vertical) {
        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    
    // ------------------------------------------------------------------------
    // Baby Methods
    // ------------------------------------------------------------------------
    private void TossBaby() {
        if(this.hasBaby) {
            // TODO ANIMATION: Play animation
            // TODO SOUND: Play sound
            Debug.Log("PLayer " + this.gameObject.tag + " toss the baby");
            this.hasBaby = false;
            this.groundRing.SetActive(false);
            SC_BabyController babyScript = this.baby.GetComponent<SC_BabyController>();
            babyScript.FlyToTarget(this.player2);
        }
    }

    private void catchBaby() {
        // TODO: Play sound / animation
        if (!this.hasBaby) {
            SC_BabyController babyScript = this.baby.GetComponent<SC_BabyController>();
            if(this.CanCatchBaby(babyScript)) {
                Debug.Log("Player " + this.gameObject.tag + " catch baby");
                this.hasBaby = true;
                this.groundRing.SetActive(true);
                babyScript.StickToTarget(this.gameObject);
            }
        }
    }

    private bool CanCatchBaby(SC_BabyController baby) {
        return !baby.HasTarget() || this.gameObject.CompareTag(baby.target.tag);
    }

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Baby") {
            this.catchBaby();
        }
    }
}
