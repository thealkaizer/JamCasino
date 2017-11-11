using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletPattern1Creation_GD : MonoBehaviour {

        public GameObject bulletPattern1Slot;
        public bool once = false;
    public int delay;
        public int delayPermanent;



    // Use this for initialization
    void Start () {
        once = false;
    }

    private IEnumerator Spawner(int time)
    {
        while (time > 0)
        {
            Debug.Log(time--);
            yield return new WaitForSeconds(1);
        }
        Debug.Log("reset");
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
