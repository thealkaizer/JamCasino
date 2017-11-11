using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletPattern1Creation_GD : MonoBehaviour {

        public GameObject bulletPattern1Slot;
        public bool bulletPattern1Alive = false;




    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	 if (Input.GetKeyDown(KeyCode.Space)){
            bulletPattern1Alive = true;         
        }

     if (bulletPattern1Alive == true)
        {
            Instantiate(bulletPattern1Slot);
        }
       if (Input.GetKeyUp(KeyCode.Space)){
            bulletPattern1Alive = false;
        }
    }
}
