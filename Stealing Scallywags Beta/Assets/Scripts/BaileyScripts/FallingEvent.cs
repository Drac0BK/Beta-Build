using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingEvent : MonoBehaviour
{
    public List<Transform> positions = new List<Transform>();
    [SerializeField]
    GameObject targetPrefab, fallingObjectPrefab;
    int fallListPosition;
    [SerializeField]
    float fallTimerSet, fallTimer, iterations;
    float iterationsMax;
    List<GameObject> fallingObjects = new List<GameObject>();

    private void Start()
    {
        iterationsMax = iterations;
    }

    private void Update()
    {
        if (fallingObjectPrefab != null)
        {
            fallTimer -= Time.deltaTime;
            if (fallTimer < 0 && iterations > 0)
            {
                fallTimer = fallTimerSet;

                fallListPosition = Random.Range(0, positions.Count);
                GameObject FallObject = Instantiate(fallingObjectPrefab, positions[fallListPosition].position + Vector3.up * 100,
                    fallingObjectPrefab.transform.rotation, transform);
                float count = iterationsMax - iterations;
                FallObject.name = FallObject.name + " " + count.ToString();
                FallObject.GetComponent<FallingObjects>().targetPos = positions[fallListPosition].position;
                FallObject.GetComponent<FallingObjects>().fallManager = gameObject;

                GameObject shadow = Instantiate(targetPrefab, positions[fallListPosition].position,
                    targetPrefab.transform.rotation, positions[fallListPosition].transform);

                FallObject.GetComponent<FallingObjects>().shadow = shadow;
                fallingObjects.Add(FallObject);
                iterations -= 1;
            }
            foreach (var obj in fallingObjects)
                obj.GetComponent<FallingObjects>().GoTowardsPoint();
            
        }
    }

    public void DeleteObject(GameObject a_fallObject)
    {
        fallingObjects.Remove(a_fallObject);
        Destroy(a_fallObject);
    }
}
