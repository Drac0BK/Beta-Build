using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnableObjects : MonoBehaviour
{
    [SerializeField]
    private int score = 0;


    [SerializeField]
    private int spawnRangeMin = 0, spawnRangeMax = 10;

    [SerializeField] private bool isPowerUp;
    [SerializeField] private bool isChest;

    public GameObject Spawner = null;

    public int mass = 5;
    public int orignalWeight;

    public List<PowerUps> powerUps;
    public bool isInside = false;
    public Vector2 returnPoint = Vector2.zero;
    public Transform respawnLocation;

    private void Start()
    {
        orignalWeight = mass;
        transform.GetComponent<Rigidbody>().mass = mass;
    }

    private void Update()
    {

        if (transform.position.y < 0)
        {
            if (!isPowerUp)
            {
                Destroy(gameObject);
                Spawner.GetComponent<Spawner>().spawnLimit += 1;
            }
            else if(respawnLocation != null)
            {
                transform.position = respawnLocation.position;
            }

        }

        transform.GetComponent<Rigidbody>().mass = mass;
        if(!isCarried)
        transform.Rotate(0,1,0);
    }

    private void OnCollisionExit(Collision collision)
    {
        transform.GetComponent<Rigidbody>().mass = mass;
    }

    public int GetScore() { return score; }

    public int GetMin() { return spawnRangeMin;  }
    public int GetMax() { return spawnRangeMax; }

    public bool IsPowerUp() { return isPowerUp; }
    public bool IsChest() { return isChest; }


    public bool isCarried = false;
}
