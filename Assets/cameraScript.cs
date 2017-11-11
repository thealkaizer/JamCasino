using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cameraScript : MonoBehaviour {

	bool oSequenceGoing = true;
	Camera cam;
	public float openingSequenceDuration;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Space)) {
			StartCoroutine("OpeningSequence");
			while(oSequenceGoing == true) {
				return;
			}
		}
	}

	IEnumerator OpeningSequence() {
		Tween rotation = transform.DORotate(Vector2.zero, openingSequenceDuration).SetEase(Ease.InSine);
		Tween translation = transform.DOMove(Vector2.zero, openingSequenceDuration).SetEase(Ease.InSine);
		oSequenceGoing = false;
		yield return new WaitForEndOfFrame();
	}
}
