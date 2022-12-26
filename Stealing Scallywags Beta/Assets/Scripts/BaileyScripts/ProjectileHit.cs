using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    Rigidbody body;
    private void OnCollisionEnter(Collision collision)
    {
        //body.AddForce(Vector3.forward, ForceMode.Impulse)
        //float velocity = (body.velocity.x + body.velocity.x + body.velocity.x)/ 3;
        if (collision.transform.GetComponent<MyPlayer>() != null)
        {
            if (!collision.transform.GetComponent<MyPlayer>().GetInvul())
            {
                collision.transform.GetComponent<MyPlayer>().StartCoroutine("InvulFrames", 3f);
                collision.transform.GetComponent<MyPlayer>().hitPoints -= 1;
                collision.transform.position += Vector3.forward * 2;
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}
