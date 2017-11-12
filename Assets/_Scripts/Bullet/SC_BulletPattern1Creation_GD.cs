using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletPattern1Creation_GD : MonoBehaviour {

        public bool once = false;

        public float delay;
        public GameObject[] bulletPatterns;
        public GameObject[] bulletDurationBank;
        public bool canPlay = true;
        public float normalStart;
        public float hardStart;

        private bool once2 = false;
        private bool once3 = false;
        private float stage1Complete;
        private float stage2Complete;
        

        public SC_KingController kingController;


    // Use this for initialization
    void Start () {
        once = false;
        stage1Complete = (kingController.maxHP / 100 * normalStart);
        stage2Complete = (kingController.maxHP / 100 * hardStart);

    }

    private IEnumerator Spawner(int time)
    {
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1);
        }
        
        float xp = Random.Range(-3f, 3f);
        float yp = 0f;
        float zp = -1f;
        transform.localPosition = new Vector3(xp, yp, zp);

        for(int k = 0; k < bulletPatterns.Length; k++) {
            bulletPatterns[k].transform.position = this.transform.position;
            bulletPatterns[k].transform.rotation = this.transform.rotation;
        }
        
        int randomIndice = Random.Range(0, bulletPatterns.Length);
        GameObject o = this.bulletPatterns[randomIndice];
        Instantiate (o);
        once = false;
    }

    // Update is called once per frame
   void Update () {
        
	 if (once == false && canPlay == true)
        {
            StartCoroutine("Spawner", delay);
            once = true;
        }
        if (kingController.currentHP <= stage1Complete && once2==false)
        {
            once2 = true;
            delay -=2;
        }
        //retirer le if si trop difficile
        if (kingController.currentHP <= stage2Complete && once3 == false)
            {
            once3 = true;
            delay -= 2;
        }
        if (canPlay == false)
        {
            foreach (GameObject g in bulletDurationBank)
            {
                ParticleSystem ps = g.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.duration = 1.0f;
            }
        }
        if (canPlay == true)
        {
            foreach (GameObject g in bulletDurationBank)
            {
                ParticleSystem ps = g.GetComponent<ParticleSystem>();
                var main = ps.main;
                main.duration = 1.0f;
            }
        }
    }
}
