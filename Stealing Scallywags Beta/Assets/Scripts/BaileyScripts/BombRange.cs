using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRange : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.parent.position + Vector3.up, -Vector3.up);

        int layermask = 1 << 6;

        Physics.Raycast(downRay, out hit, Mathf.Infinity, layermask);
        if (hit.collider != null)
        {
            gameObject.SetActive(true);
            transform.position = hit.point + new Vector3(0, 0.2f, 0);
        }
        else
            gameObject.SetActive(false);

    }
}
