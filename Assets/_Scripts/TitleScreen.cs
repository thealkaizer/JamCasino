using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleScreen : MonoBehaviour {

	Vector3 one = new Vector3(1f, 1f, 1f);
	Vector3 punch = new Vector3(0.3f, 0.3f, 0.3f);

	public bool p1ready, p2ready;
	public bool p1lock, p2lock;
	public bool launching;

	public Image player1icon, player2icon;
	public Image player1fill, player2fill;
	public Image black;
	public Text player1text, player2text;

	public string p1string, p2string;

	// Use this for initialization
	void Start () {

		Time.timeScale = 1.0f;
		AkSoundEngine.PostEvent("Music", gameObject);
		AkSoundEngine.SetState("Music", "Menu");
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			Debug.Log("input 1");
			if (p1lock != true && launching != true) {
				if(p1ready == true) {
					StartCoroutine(Empty(1));
				} else {
					StartCoroutine(Fill(1));
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha2)) {
			Debug.Log("input 2");
			if (p2lock != true && launching != true) {
				if(p2ready == true) {
					StartCoroutine(Empty(2));
				} else {
					StartCoroutine(Fill(2));
				}
			}
		}
	}

	IEnumerator Fill(int number) {
		//DEFINE TEMP VARIABLES

		Debug.Log("fill" + number);

		Image icon;
		Image fill;
		Text text;

		if (number == 1){
			p1lock = true;
			icon = player1icon;
			fill = player1fill;
			text = player1text;
			player1text.text = p1string;
		} else {
			p2lock = true;
			icon = player2icon;
			fill = player2fill;
			text = player2text;
			player2text.text = p2string;
		}

		//DO STUFF
		AkSoundEngine.SetSwitch("menu", "on", gameObject);
		AkSoundEngine.PostEvent("Play_Menu", gameObject);


		icon.transform.localScale = new Vector3(1f, 1f, 1f);
		fill.transform.localScale = new Vector3(1f, 1f, 1f);
		yield return new WaitForEndOfFrame();
		Tween filltween = icon.transform.DOPunchScale(punch, 0.5f, 10, 1);
		fill.transform.DOPunchScale(punch, 0.5f, 10, 1);
		for (int x = 0; x <= 100; x++) {
			fill.fillAmount = x / 10f;
		}
		Debug.Log("TEST");
		yield return filltween.WaitForCompletion();
		yield return new WaitForEndOfFrame();
		//icon.transform.localScale = new Vector3(1f, 1f, 1f);
		//fill.transform.localScale = new Vector3(1f, 1f, 1f);


		if (number == 1) {
			p1ready = true;
			p1lock = false;
		} else {
			p2ready = true;
			p2lock = false;
		}
		StartCoroutine("CheckReadies");
	}

	IEnumerator Empty(int number) {
		//DEFINE TEMP VARIABLES

		Debug.Log("empty" + number);

		Image icon;
		Image fill;
		Text text;

		if (number == 1){
			p1lock = true;
			icon = player1icon;
			fill = player1fill;
			text = player1text;
			p1ready = false;
			player1text.text = null;
		} else {
			p2lock = true;
			icon = player2icon;
			fill = player2fill;
			text = player2text;
			p2ready = false;
			player2text.text = null;
		}

		//DO STUFF

		AkSoundEngine.SetSwitch("menu", "off", gameObject);
		AkSoundEngine.PostEvent("Play_Menu", gameObject);

		icon.transform.localScale = new Vector3(1f, 1f, 1f);
		fill.transform.localScale = new Vector3(1f, 1f, 1f);
		yield return new WaitForEndOfFrame();
		icon.transform.DOPunchScale(punch, 0.5f, 10, 1);
		Tween emptytween = fill.transform.DOPunchScale(punch, 0.5f, 10, 1);
		for (int x = 100; x > 0; x--) {
			fill.fillAmount = x / 10f;
			//yield return new WaitForEndOfFrame();
		}


		yield return emptytween.WaitForCompletion();
		yield return new WaitForEndOfFrame();
		icon.transform.localScale = new Vector3(1f, 1f, 1f);
		fill.transform.localScale = new Vector3(1f, 1f, 1f);

		if (number == 1) {
			p1lock = false;
		} else {
			p2lock = false;
		}
		StartCoroutine("CheckReadies");
	}

	IEnumerator CheckReadies() {
		if (p1ready == true && p2ready == true) {
			Color newalpha = black.color;
			launching = true;
			Tween toBlack = black.DOFade(1f, 3f);
			yield return toBlack.WaitForCompletion();
		//load level
			AkSoundEngine.SetState("Music", "Game");
			Application.LoadLevel("MainScene");

		}

	}
}
