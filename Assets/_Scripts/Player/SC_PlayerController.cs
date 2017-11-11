using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlayerController : MonoBehaviour {
    public float speedNormal;
    public float speedBaby;
    public bool hasBaby;

    public GameObject player2; // Ref to the other player.
    public GameObject baby;

    private bool isWalking;

    private float m_effectiveSpeed;

    void Awake() {
        this.hasBaby = false;
        this.isWalking = false;
        this.m_effectiveSpeed = this.speedNormal;
    }

    void Update() {
        if (Input.GetButton("Fire1")) {
            this.TossBaby();
        }
    }
	
	void FixedUpdate() {
        float horizontal    = Input.GetAxis("Horizontal");
        float vertical      = Input.GetAxis("Vertical");
        this.HandleMovement(horizontal, vertical);
	}

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

    private void TossBaby() {
        if(this.hasBaby) {
            // TODO ANIMATION: Play animation
            // TODO SOUND: Play sound
            Debug.Log("PLayer " + GetInstanceID() + "toss the baby");
            this.hasBaby = false;

        }
        else {
            Debug.Log("PLayer " + GetInstanceID() + ", can't toss baby.");
        }
    }

    private void catchBaby() {
        this.hasBaby = true;
    }
}
