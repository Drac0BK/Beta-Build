using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MyPlayer>() != null)
        {
            collision.gameObject.GetComponent<MyPlayer>().Respawn();
        }
    }
}
