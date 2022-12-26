using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZones : MonoBehaviour
{
    public GameObject myPlayer;
    int score = 0;
    int highScore = 0;
    float playerMovementSpeed;
    float playerJumpVelocity;
    float modifiedMovementSpeed;
    float modifiedJumpVelocity;
    bool isEmpty = true;

    List<GameObject> collectedTreasure = new List<GameObject>();
    List<GameObject> newTreasure = new List<GameObject>();
    // Start is called before the first frame update
    public void IntializeScoreZone()
    {
        if (myPlayer != null)
        {
            playerMovementSpeed = myPlayer.GetComponent<MyPlayer>().movementSpeed;
            playerJumpVelocity = myPlayer.GetComponent<MyPlayer>().jumpVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3);
        newTreasure.Clear();
        foreach (var colliders in hitColliders)
        {
            if (!colliders.isTrigger)
                if (colliders.GetComponent<SpawnableObjects>() != null && !colliders.GetComponent<SpawnableObjects>().IsPowerUp())
                {
                    newTreasure.Add(colliders.gameObject);
                    colliders.GetComponent<SpawnableObjects>().mass = 10000;
                    score += colliders.GetComponent<SpawnableObjects>().GetScore();
                }
                else if (colliders.GetComponent<SpawnableObjects>() != null && colliders.GetComponent<SpawnableObjects>().IsPowerUp())
                {
                    isEmpty = false;
                    modifiedMovementSpeed += colliders.GetComponent<SpawnableObjects>().powerUps[0].speed;
                    modifiedJumpVelocity += colliders.GetComponent<SpawnableObjects>().powerUps[0].jump;
                }
        }

        StatChange();

        isEmpty = true;


        if (score > highScore || score < highScore)
            highScore = score;
        score = 0;
        modifiedMovementSpeed = 0;
        modifiedJumpVelocity = 0;
        bool isGone = false;
        for(int i = 0; i < collectedTreasure.Count; i++)
        {
            foreach(var treasure in newTreasure)
            {
                if(treasure == collectedTreasure[i])
                    isGone = true;

            }
            if (isGone)
                collectedTreasure[i].GetComponent<SpawnableObjects>().mass = collectedTreasure[i].GetComponent<SpawnableObjects>().orignalWeight;
        }

        collectedTreasure = newTreasure;
    }

    public void StatChange()
    {
        if (!isEmpty)
            myPlayer.GetComponent<MyPlayer>().movementSpeed = playerMovementSpeed + modifiedMovementSpeed;
        else if (isEmpty && myPlayer.GetComponent<MyPlayer>().treasureCarried == null)
            myPlayer.GetComponent<MyPlayer>().movementSpeed = playerMovementSpeed;
        else
            myPlayer.GetComponent<MyPlayer>().movementSpeed = playerMovementSpeed * 0.9f;

        if (!isEmpty)
            myPlayer.GetComponent<MyPlayer>().jumpVelocity = playerJumpVelocity + modifiedJumpVelocity;
        else if (isEmpty && myPlayer.GetComponent<MyPlayer>().treasureCarried == null)
            myPlayer.GetComponent<MyPlayer>().jumpVelocity = playerJumpVelocity;
    }

    //public void CursedChange()
    //{
    //    if (modifiedMovementSpeed + playerMovementSpeed < playerMovementSpeed)
    //        myPlayer.GetComponent<MyPlayer>().movementSpeed = playerMovementSpeed - modifiedMovementSpeed;
    //    else
    //        myPlayer.GetComponent<MyPlayer>().movementSpeed = playerMovementSpeed;

    //    if (modifiedJumpVelocity + playerJumpVelocity < playerJumpVelocity)
    //        myPlayer.GetComponent<MyPlayer>().jumpVelocity = playerJumpVelocity - modifiedJumpVelocity;
    //    else
    //        myPlayer.GetComponent<MyPlayer>().movementSpeed = playerMovementSpeed;
    //}


    //void OnDrawGizmosSelected()
    //{
    //// Draw a yellow sphere at the transform's position
    //Gizmos.color = Color.blue;
    //Gizmos.DrawSphere(transform.position, 3);
    //}

    public void SetPlayer(GameObject player) { myPlayer = player; }
    public int  GetScore() { return highScore; }
}
