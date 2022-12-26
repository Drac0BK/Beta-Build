using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime;
    public bool camZoom = true;

    public float minZoom = 30f;
    public float maxZoom;
    public float zoomLimiter = 50f;
    public float newXZoom;
    public float newZZoom;
    public float xClampLeft;
    public float xClampRight;
    public float zClampLeft;
    public float zClampRight;
    public float lerpControlX;
    public float lerpControlZ;

    [SerializeField] Vector3 centerPoint;
    private Vector3 velocity;
    private Camera cam;

    public List<GameObject> playerIcons;

    private void Start()
    {
        xClampLeft = -38.9f;
        xClampRight = 3.1f;
        zClampLeft = -44.2f;
        zClampRight = -18.1f;

        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (maxZoom == 18)
        {
            xClampLeft = -38.9f;
            xClampRight = 3.1f;
            zClampLeft = -49.2f;
            zClampRight = -18.1f;
        }
        else
        {
            xClampLeft = -31.6f;
            xClampRight = -7.6f;
            zClampLeft = -41.6f;
            zClampRight = -32.1f;
        }

        if (newXZoom <= 28 && newZZoom <= 28 && camZoom == true)
        {
            camZoom = false;
            maxZoom = 18;
        }
        else if (newXZoom >= 21 || newZZoom >= 21 && camZoom == false)
        {
            camZoom = true;
            maxZoom = 27;
        }

        for(int i = 0; i < playerIcons.Count; i++)
        {
            GameObject icon = playerIcons[i];

            float zoom = (newXZoom + newZZoom) / 2;
            zoom = zoom / minZoom;

            Color color = new Vector4(1,1,1,zoom);
            icon.GetComponent<Image>().color = color;
        }
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Zoom();
        Move();
    }

    void Zoom()
    {
        
        newZZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestZDistance() / zoomLimiter);
        newXZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestXDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newXZoom, Time.deltaTime);
    }

    void Move()
    {
        centerPoint = GetCenterPoint();
        
        Vector3 newPosition = centerPoint + offset;

        newPosition = new Vector3(Mathf.Clamp(newPosition.x, xClampLeft, xClampRight), newPosition.y, Mathf.Clamp(newPosition.z, zClampLeft, zClampRight));


        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestXDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    float GetGreatestZDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.z;
    }

    Vector3 GetCenterPoint()
    {
        
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
