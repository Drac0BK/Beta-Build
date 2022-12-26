using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRange : MonoBehaviour
{
    public List<GameObject> treasurePrefabs;
    public float x , y;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Vector3 size = new Vector3(x, 0.5f, y);
        Gizmos.DrawCube(transform.position, size);
    }
}
