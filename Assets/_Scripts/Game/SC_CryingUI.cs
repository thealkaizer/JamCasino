using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CryingUI : MonoBehaviour {
	public GameObject baby;
	Vector3 axe;
	public Camera cam;
	
	// Update is called once per frame
	void Start(){
		cam = Camera.main;
	}

	void Update () {
		
		axe = Vector3.Normalize(cam.transform.position - baby.transform.position);
		axe = axe * 3f;
		transform.position = baby.transform.position + axe;
		transform.LookAt(Camera.main.transform);
	}
}
