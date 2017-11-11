using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class surpriseScript : MonoBehaviour {


	public GameObject emote;
	public float timer;
	Vector3 punch = new Vector3(0.60f, 0.60f, 0.60f);

	// Use this for initialization
	void Start () {
		emote.transform.DOPunchScale(punch, timer * 0.90f, 10, 1);
		Destroy(gameObject, timer);
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform);
	}
}
