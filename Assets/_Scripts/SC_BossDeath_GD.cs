using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BossDeath_GD : MonoBehaviour {

    public Material death;
    public Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            rend.sharedMaterial = death;
        }
    }
}
