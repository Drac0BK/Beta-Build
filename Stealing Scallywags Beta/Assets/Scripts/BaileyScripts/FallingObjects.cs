using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    public Vector3 targetPos;
    public float timer;
    public GameObject fallManager;
    public float speed;
    public GameObject shadow;


    public void GoTowardsPoint()
    {
        if(transform.position.y > targetPos.y)
            transform.position -= transform.up * speed * Time.deltaTime;
        else
        {
            if(shadow != null)
                Destroy(shadow);

            timer -= Time.deltaTime;
            if(timer < 0)
            {
                fallManager.GetComponent<FallingEvent>().DeleteObject(gameObject);
            }
        }
    }
}
