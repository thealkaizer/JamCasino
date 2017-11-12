using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletPattern1Creation_GD : MonoBehaviour {

        public bool once = false;
        public float delay;
        public float delayPermanent;
        public GameObject[] bulletPatterns;


    // Use this for initialization
    void Start () {
        once = false;
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
        
	 if (once == false)
        {
            StartCoroutine("Spawner", delay);
            once = true;
        }
      }
}
