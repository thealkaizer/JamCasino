using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraScript : MonoBehaviour {

	bool oSequenceGoing = true;
	Camera cam;
	public float openingSequenceDuration, waitTime;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		StartCoroutine("OpeningSequence");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OpeningSequence() {
		yield return new WaitForSeconds(waitTime);
		Tween rotation = transform.DORotate(Vector2.zero, openingSequenceDuration).SetEase(Ease.InSine);
		Tween translation = transform.DOMove(Vector2.zero, openingSequenceDuration).SetEase(Ease.InSine);
		oSequenceGoing = false;
		yield return new WaitForEndOfFrame();
		//Start GAME
	}
}
