using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SC_GameOverUI : MonoBehaviour {
	public Image top;
	public Image bot;
	public Image bBlack, tBlack;

	private RectTransform toprect, botrect;

	// Use this for initialization
	void Start () {
		toprect = top.GetComponent<RectTransform>();
		botrect = bot.GetComponent<RectTransform>();
	}

    public void showGameOver() {
	    StartCoroutine("SlideGameOver");
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
