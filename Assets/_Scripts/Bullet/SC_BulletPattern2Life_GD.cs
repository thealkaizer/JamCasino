using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletPattern2Life_GD : MonoBehaviour {

    public int LifeTime;

    void Start()
    {
        StartCoroutine("Countdown", LifeTime);
    }

    private IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1);
        }
        Destroy(gameObject);
    }
}
