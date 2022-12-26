using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Treasure : MonoBehaviour
{
    [SerializeField]
    private int score = 0;


    [SerializeField]
    private int spawnRangeMin = 0, spawnRangeMax = 10;

    [SerializeField] private bool isPowerUp;

    public GameObject Spawner = null;

    public int mass = 5;

    public List<PowerUps> powerUps;
    public bool isInside = false;
    public Vector2 returnPoint = Vector2.zero;

    private void Start()
    {
        transform.GetComponent<Rigidbody>().mass = mass;
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            Destroy(gameObject);
            Spawner.GetComponent<Spawner>().spawnLimit += 1;
        }


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


    public bool isCarried = false;
}
