using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CryingUIscript : MonoBehaviour {
	public GameObject baby;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(baby.transform.position.x, 1.35f, baby.transform.position.z);
		transform.LookAt(Camera.main.transform);
	}
}
