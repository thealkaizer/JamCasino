using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerController : MonoBehaviour {
    // ------------------------------------------------------------------------
    // Attributes (Note: all public for debug purpose)
    // ------------------------------------------------------------------------
    public float speedNormal;
    public float speedBaby;
    public float speedGoBackToValidPosition;

    public Animator animPlayer;

    public Transform emotepoint;

    public GameObject player2; // Ref to the other player.
    public GameObject baby;
    public GameObject groundRing;
    public GameObject babyBucket;
    public GameObject surprise;

    private bool hasBaby;
    private bool isWalking;
    private bool canMove;

    private float m_effectiveSpeed;
    
    private string m_btn_fire1;
    private string m_btn_vertical;
    private string m_btn_horizontal;

    public float cowldownBabyTossInSecond;
    private float m_lastUseBabyToss;

    
    // ------------------------------------------------------------------------
    // Unity Methods
    // ------------------------------------------------------------------------
    void Awake() {
        this.canMove = true;
        this.hasBaby = false;
        this.isWalking = false;
        this.m_effectiveSpeed = this.speedNormal;
        this.m_lastUseBabyToss = -10; // Just a low number, so that can toss now
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
        this.canMove = false;
	}

    
    // ------------------------------------------------------------------------
    // Move Methods
    // ------------------------------------------------------------------------
    private void HandleMovement(float horizontal, float vertical) {
        this.isWalking = false;

        if(this.canMove == false) {
            // Add move toward closest movable position
            animPlayer.SetInteger("anim", 0);
            this.moveTowardClosestValidPosition();
            return;
        }

        if(horizontal  != 0.0f || vertical != 0.0f) {
            // TODO ANIMATION: place walking animation
            animPlayer.SetInteger("anim", 1);
            this.m_effectiveSpeed = this.hasBaby ? this.speedBaby : this.speedNormal;
            this.isWalking = true;
            this.Rotate(horizontal, vertical);
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            movement *= Time.deltaTime * m_effectiveSpeed;
            transform.position +=  movement;
            Debug.DrawRay(transform.position, movement, Color.blue, 1.0f);
        } else {
            animPlayer.SetInteger("anim", 0);
        }
    }
    
    private void moveTowardClosestValidPosition() {
        int layerMask = 1 << 9; // 9 is ground id
        Collider[] collidersArray = Physics.OverlapSphere(this.transform.position, 5, layerMask);

        int minColliderIndex = 0;
        float minDistance = 10000f; // Just something big enough
        for(int k = 0; k < collidersArray.Length; k++) {
            Debug.DrawLine(this.transform.position, collidersArray[k].gameObject.transform.position, Color.blue, 0.5f);
            float distance = Vector3.Distance(this.transform.position, collidersArray[k].gameObject.transform.position);
            if(distance < minDistance) {
                minColliderIndex = k;
                minDistance = distance;
            }
        }
        Debug.DrawLine(this.transform.position, collidersArray[minColliderIndex].gameObject.transform.position, Color.blue, 1f);

        Vector3 dir = collidersArray[minColliderIndex].transform.position - this.transform.position;
        dir = Vector3.Normalize(dir);
        dir.y = 0;
        this.transform.position += dir * this.speedGoBackToValidPosition * Time.deltaTime;
    }

    private void Rotate(float horizontal, float vertical) {
        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }

    
    // ------------------------------------------------------------------------
    // Baby Methods
    // ------------------------------------------------------------------------
    private void TossBaby() {
        if(this.hasBaby && this.isBabyTossCowldownReloaded()) {
            if (gameObject.CompareTag("Player1")) {
                AkSoundEngine.PostEvent("Play_Throw_P2", gameObject);
            } else {
                AkSoundEngine.PostEvent("Play_Throw_P1", gameObject);
            }
            animPlayer.SetBool("hasBaby", false);
            GameObject emote = Instantiate(surprise, emotepoint.position, Quaternion.identity);
            emote.transform.SetParent(emotepoint);
            // TODO ANIMATION: Play animation
            // TODO SOUND: Play sound
            Debug.Log("PLayer " + this.gameObject.tag + " toss the baby");
            this.m_lastUseBabyToss = Time.time;
            this.hasBaby = false;
            this.groundRing.SetActive(false);
            SC_BabyController babyScript = this.baby.GetComponent<SC_BabyController>();
            babyScript.FlyToTarget(this.player2);
        }
    }

    private void catchBaby() {
        if (!this.hasBaby) {
            SC_BabyController babyScript = this.baby.GetComponent<SC_BabyController>();
            if(this.CanCatchBaby(babyScript)) {
                    if (gameObject.CompareTag("Player1")) {
                        AkSoundEngine.PostEvent("Play_Catch_P2", gameObject);
                    } else {
                        AkSoundEngine.PostEvent("Play_Catch_P1", gameObject);
                    }
                animPlayer.SetBool("hasBaby", true);
                // TODO: Play sound / animation
                GameObject emote = Instantiate(surprise, emotepoint.position, Quaternion.identity);
                emote.transform.SetParent(emotepoint);
                Debug.Log("Player " + this.gameObject.tag + " catch baby");
                this.hasBaby = true;
                this.groundRing.SetActive(true);
                babyScript.StickToTarget(this.gameObject);
            }
        }
    }

    private bool CanCatchBaby(SC_BabyController baby) {
        return !baby.HasTarget() || this.gameObject.CompareTag(baby.getTargetTag());
    }

    private bool isBabyTossCowldownReloaded() {
        return (Time.time - this.m_lastUseBabyToss) >= this.cowldownBabyTossInSecond;
    }

    
    // ------------------------------------------------------------------------
    // Unity triggers Methods
    // ------------------------------------------------------------------------
    public void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Baby")) {
            this.catchBaby();
        }
        if (other.CompareTag("Tile")) {
            this.canMove = true;
        }
    }

    public void OnTriggerStay(Collider other) {
        if(other.CompareTag("Baby")) {
            this.catchBaby();
        }
        if (other.CompareTag("Tile")) {
            this.canMove = true;
        }
    }
}
