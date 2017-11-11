using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletLife_GD : MonoBehaviour
{
    public int LifeTime;
    public int RotationSpeed;

    void Start()
    {
        StartCoroutine("Countdown", LifeTime);
    }

    private void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);

    }

    private IEnumerator Countdown(int time)
    {
        while (time > 0)
        {
            //Debug.Log(time--);
            yield return new WaitForSeconds(1);
        }
        //Debug.Log("Countdown Complete!");
        Destroy(gameObject);
    }

}