using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoraPoper : MonoBehaviour {
    public Transform[] landingTargets;

    public bool isRunning;

    void Start() {
        this.isRunning = false;
    }
}
