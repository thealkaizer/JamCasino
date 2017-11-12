using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_KingHealthBar : MonoBehaviour {
	public Image bar;
	public float health;
    public bool check;
	// Update is called once per frame

	void Start(){
		bar.fillAmount = 1f;
		StartCoroutine("DelayBar");
	}

	void Update () {
		if (check == true) {
			bar.fillAmount = this.health  / 246;
		}
	}

	IEnumerator DelayBar() {
		yield return new WaitForSeconds(1f);
		check = true;
	}
}
