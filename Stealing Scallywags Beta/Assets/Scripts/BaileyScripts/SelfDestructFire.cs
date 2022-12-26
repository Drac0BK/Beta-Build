using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructFire : MonoBehaviour
{
    public float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
