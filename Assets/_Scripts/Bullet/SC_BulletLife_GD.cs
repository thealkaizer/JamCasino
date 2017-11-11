using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_BulletLife_GD : MonoBehaviour
{
    public int LifeTime;
    public int RotationSpeed;
    public float minmum = 0.0f;
    public float maximum = 0.25f;
    private bool goLeft = true;

    void Start()
    {
        StartCoroutine("Countdown", LifeTime);
    }

    private void Update()
    {
                if (goLeft == true) {
            transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
        }
       if (goLeft == false) {
            transform.Rotate(0, RotationSpeed * -Time.deltaTime, 0);
        }
       if (transform.rotation.y >= maximum)
        {
            goLeft = false;
        }
       else if (transform.rotation.y <= minmum)
        {
            goLeft = true;
        }
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