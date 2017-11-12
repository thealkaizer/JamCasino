using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletPattern1Creation_GD : MonoBehaviour {

        public GameObject bulletPattern1Slot;
        public GameObject bulletPattern2Slot;
        public bool once = false;
        public float delay;
        public float delayPermanent;



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

        bulletPattern1Slot.transform.position = this.transform.position;
        bulletPattern1Slot.transform.rotation = this.transform.rotation;
        Instantiate(bulletPattern1Slot);
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
