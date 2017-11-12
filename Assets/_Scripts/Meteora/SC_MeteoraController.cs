using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_MeteoraController : MonoBehaviour {
    public Transform target;
    public float fallingSpeed;

    private Vector3 vectDirection;

	// Use this for initialization
	void Start() {
        this.vectDirection = this.target.position - this.transform.position;
        this.vectDirection = Vector3.Normalize(this.vectDirection);
        // TODO: Add rotation velocity?
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(this.hasTarget()) {
            this.transform.position += this.vectDirection * fallingSpeed * Time.deltaTime;
        }
	}

    private bool hasTarget() {
        return this.target != null;
    }
}
