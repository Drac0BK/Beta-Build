using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> treasurePrefabs;
    public List<GameObject> spawnPoints;

    Vector3 pos;
    [SerializeField]
    float setSpawnTimer = 10.0f, spawnTimer = 0, stopSpawning = 120;
    public float spawnLimit = 20;
    bool canSpawn = false;
    Vector3 randSpawn = new Vector3(0, 0);
    float xPoint , yPoint;

    private void Start()
    {
        spawnTimer = setSpawnTimer;
    }

    private void Update()
    {
        if (stopSpawning > 0)
        {
            stopSpawning -= Time.deltaTime;
            spawnTimer -= Time.deltaTime;
        }
        if (spawnLimit > 0)
        {
            if (spawnTimer < 0 && stopSpawning > 0)
            {
                SpawnTreasure();
                spawnTimer = setSpawnTimer;
                spawnLimit -= 1;
            }
        }

    }


    void SpawnTreasure()
    {
        int randomSpawner = Random.Range(0, spawnPoints.Count);
        pos = spawnPoints[randomSpawner].transform.position;
        if (spawnPoints[randomSpawner].GetComponent<SpawnerRange>() != null)
        {
            xPoint = spawnPoints[randomSpawner].GetComponent<SpawnerRange>().x/2;
            yPoint = spawnPoints[randomSpawner].GetComponent<SpawnerRange>().y/2;
            treasurePrefabs = spawnPoints[randomSpawner].GetComponent<SpawnerRange>().treasurePrefabs;
        }

        int failSpawn = 0;
        int randTreasure = Random.Range(0, treasurePrefabs.Count);
        //int spawnRangeMin = treasurePrefabs[randTreasure].GetComponent<SpawnableObjects>().GetMin();
        int spawnRangeMax = treasurePrefabs[randTreasure].GetComponent<SpawnableObjects>().GetMax();

        while (!canSpawn)
        {
            randSpawn = new Vector3(Random.Range(pos.x - xPoint, pos.x + xPoint),
                pos.y, Random.Range(pos.z - yPoint, pos.z + yPoint));
            if (randSpawn.x < -0 || randSpawn.x > 0 ||
                randSpawn.z < -0 || randSpawn.z > 0)
            {
                Collider[] hitColliders = Physics.OverlapSphere(randSpawn, 1);
                foreach (var colliders in hitColliders)
                {
                    if (colliders.gameObject.layer == 6)
                    {
                        canSpawn = true;

                    }
                }
            }
            failSpawn += 1;
            if (failSpawn > 5)
            {
                canSpawn = false;
                return;
            }
        }
        //Debug.Log("Treasure Num: " + treasurePrefabs[randTreasure].name + ", Pos x: " + randSpawn.x + ", Pos z: " + randSpawn.z);
        GameObject treasure = Instantiate(treasurePrefabs[randTreasure], randSpawn, spawnPoints[randomSpawner].transform.rotation, null);
        treasure.GetComponent<SpawnableObjects>().Spawner = gameObject;
        canSpawn = false;
    }

}
