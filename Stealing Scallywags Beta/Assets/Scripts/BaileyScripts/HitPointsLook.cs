using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointsLook : MonoBehaviour
{
    public Transform cameraPoint;

    private void Start() { cameraPoint = Camera.main.transform; }
    void Update(){ transform.LookAt(cameraPoint);}
}
