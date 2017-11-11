using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_RingPlayer : MonoBehaviour {

	public Transform target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
	}
}
