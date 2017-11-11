using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speedNormal;
    public float speedBaby;
    private bool isWalking;
	
	void FixedUpdate () {
        float horizontal    = Input.GetAxis("Horizontal");
        float vertical      = Input.GetAxis("Vertical");
        this.handleMovement(horizontal, vertical);
	}

    private void handleMovement(float horizontal, float vertical) {
        this.isWalking = false;

        if(horizontal  != 0.0f || vertical != 0.0f) {
            this.isWalking = true;
            this.rotate(horizontal, vertical);
            Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
            movement *= Time.deltaTime * speedNormal;
            transform.position +=  movement;
            Debug.DrawRay(transform.position, movement, Color.blue, 1.0f);
        }
    }

    private void rotate(float horizontal, float vertical) {
        float angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, angle, 0f));
    }
}
