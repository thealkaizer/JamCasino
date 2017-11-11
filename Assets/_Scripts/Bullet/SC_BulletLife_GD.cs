using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletLife_GD : MonoBehaviour
{
    public int LifeTime;
    public int RotationSpeed;
    public float minmum = 0.0f;
    public float maximum = 90.0f;

    void Start()
    {
        StartCoroutine("Countdown", LifeTime);
    }

    /*private void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
        if (transform.rotation.y <= maximum)
        {
            RotationSpeed = RotationSpeed * - 1;
        }
    }*/

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