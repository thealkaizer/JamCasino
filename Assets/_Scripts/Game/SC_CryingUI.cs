using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CryingUI : MonoBehaviour {
	public GameObject baby;
	Vector3 axe;
	
	// Update is called once per frame
	void Update () {
		//axe = new Vector3(Camera.main.transform.position - baby.transform.position);
		transform.position = new Vector3(baby.transform.position.x, 1.35f, baby.transform.position.z);
		transform.LookAt(Camera.main.transform);
	}
}
