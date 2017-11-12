using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScript : MonoBehaviour {

	public Image top;
	public Image bot;
	public Image bBlack, tBlack;

	public RectTransform toprect, botrect;

	// Use this for initialization
	void Start () {
		toprect = top.GetComponent<RectTransform>();
		botrect = bot.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			StartCoroutine("SlideGameOver");
		}
	}

	IEnumerator SlideGameOver() {
		top.DOFade(1f, 1f);
		bot.DOFade(1f, 1f);
		tBlack.DOFade(0.97f, 1f);
		bBlack.DOFade(0.97f, 1f);
		yield return new WaitForSeconds(0.6f);
		toprect.DOLocalMoveX(-1040, 1.5f);
		Tween bottween = botrect.DOLocalMoveX(1040, 1.5f);
		yield return bottween.WaitForCompletion();
		yield return new WaitForEndOfFrame();
	}

}
