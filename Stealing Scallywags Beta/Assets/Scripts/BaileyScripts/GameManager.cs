using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject levelObject;
    public GameObject pauseMenu;


    public List<GameObject> players = new List<GameObject>();
    List<GameObject> spawnedPlayers = new List<GameObject>();
    public List<GameObject> playerPrefabs = new List<GameObject>();
    public List<GameObject> scoreZones;
    public List<GameObject> prefabBoats;
    public List<Transform> boatSpots;
    public List<GameObject> ramps = new List<GameObject>();
    public List<TMPro.TMP_Text> scoreTexts;

    
    [SerializeField]
    float timer = 120.0f;
    bool timerStop = false;

    PlayerConfigManager playerConfigManager;

    List<PlayerConfiguration> playerConfiguration;

    //public GameObject audioManager;

    bool gameOver = false;

    public bool isPaused = false;

    public IconManager iconManager;
    public WinnerSetScreen winnerSet;

    int playerCount = 0;
    public TMPro.TMP_Text timerText;
    public GameObject timerUI;
    public GameObject waterEndScreen;
    private void Start()
    {
        playerConfigManager = FindObjectOfType<PlayerConfigManager>();
        GetPlayers();
        Camera cam = Camera.main;
        iconManager.AddIconList();
        if (scoreZones != null)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] != null)
                {
                    //playerConfiguration[i].
                    scoreZones[i].gameObject.SetActive(true);
                    GameObject newPlayer = Instantiate(players[i], scoreZones[i].transform.position + (Vector3.up * 2), scoreZones[i].transform.rotation);
                    scoreZones[i].GetComponent<ScoreZones>().SetPlayer(newPlayer);
                    newPlayer.GetComponent<MyPlayer>().gameManager = this;
                    newPlayer.GetComponent<MyPlayer>().InitializePlayer(playerConfiguration[i]);
                    spawnedPlayers.Add(newPlayer);
                    cam.GetComponent<CameraZoom>().targets.Add(newPlayer.transform);
                    scoreZones[i].GetComponent<ScoreZones>().IntializeScoreZone();
                }
                else
                {
                    scoreZones[i].gameObject.SetActive(false);
                }
            }

            int j = 0;
            foreach(var listSpot in players)
            {
                if(players[j].gameObject == null)
                    players.RemoveAt(j);
            }
            if(boatSpots != null)
            SetLevel();
        }
    }

    void GetPlayers()
    {
        if (playerConfigManager != null)
        {
            playerConfiguration = playerConfigManager.GetPlayerPrefabs();
            foreach (var player in playerConfiguration)
            {
                if (player.playerPrefab != null)
                {
                    players.Add(player.playerPrefab);
                    playerCount++;
                }
            }
            playerCount = 0;
        }
    }

    void SetLevel()
    {
        int position = 0;
        int iconSpot = 0;
        foreach(var objectIcon in iconManager.objectList)
        {
            objectIcon.gameObject.SetActive(false);
        }

        foreach (var player in players)
        {
            if (player != null)
            {
                for (int i = 0; i < playerPrefabs.Count; i++)
                {
                    if (player.gameObject == playerPrefabs[i])
                    {
                        Debug.Log(iconSpot);
                        if (player.gameObject.name.Contains("Shell"))
                        {
                            iconManager.objectList[iconSpot].gameObject.SetActive(true);
                            Instantiate(prefabBoats[3], boatSpots[position].position, boatSpots[position].rotation);
                            iconManager.iconList[iconSpot].playerIcon.sprite = iconManager.iconSprite[0];
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().swordIcon = iconManager.iconList[iconSpot].swordIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().bombIcon = iconManager.iconList[iconSpot].bombIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().gunIcon = iconManager.iconList[iconSpot].gunIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().playerIcon = iconManager.iconList[iconSpot].playerIcon;
                            players[iconSpot].GetComponent<MyPlayer>().SetWeapon(0);
                            ramps[position].gameObject.SetActive(true);
                            position += 1; iconSpot += 1;
                            
                        }
                        if (player.gameObject.name.Contains("Lori"))
                        {
                            iconManager.objectList[iconSpot].gameObject.SetActive(true);
                            Instantiate(prefabBoats[2], boatSpots[position].position, boatSpots[position].rotation);
                            iconManager.iconList[iconSpot].playerIcon.sprite = iconManager.iconSprite[1];
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().swordIcon = iconManager.iconList[iconSpot].swordIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().bombIcon = iconManager.iconList[iconSpot].bombIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().gunIcon = iconManager.iconList[iconSpot].gunIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().playerIcon = iconManager.iconList[iconSpot].playerIcon;
                            player.GetComponent<MyPlayer>().SetWeapon(0);
                            ramps[position].gameObject.SetActive(true);
                            position += 1; iconSpot += 1;
                        }

                        if (player.gameObject.name.Contains("Bob"))
                        {
                            iconManager.objectList[iconSpot].gameObject.SetActive(true);
                            Instantiate(prefabBoats[1], boatSpots[position].position, boatSpots[position].rotation);
                            iconManager.iconList[iconSpot].playerIcon.sprite = iconManager.iconSprite[2];
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().swordIcon = iconManager.iconList[iconSpot].swordIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().bombIcon = iconManager.iconList[iconSpot].bombIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().gunIcon = iconManager.iconList[iconSpot].gunIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().playerIcon = iconManager.iconList[iconSpot].playerIcon;
                            player.GetComponent<MyPlayer>().SetWeapon(0);
                            ramps[position].gameObject.SetActive(true);
                            position += 1; iconSpot += 1;
                        }

                        if (player.gameObject.name.Contains("Beard"))
                        {
                            iconManager.objectList[iconSpot].gameObject.SetActive(true);
                            Instantiate(prefabBoats[0], boatSpots[position].position, boatSpots[position].rotation);
                            iconManager.iconList[iconSpot].playerIcon.sprite = iconManager.iconSprite[3];
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().swordIcon = iconManager.iconList[iconSpot].swordIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().bombIcon = iconManager.iconList[iconSpot].bombIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().gunIcon = iconManager.iconList[iconSpot].gunIcon;
                            spawnedPlayers[iconSpot].GetComponent<MyPlayer>().playerIcon = iconManager.iconList[iconSpot].playerIcon;
                            player.GetComponent<MyPlayer>().SetWeapon(0);
                            ramps[position].gameObject.SetActive(true);
                            position +=1; iconSpot += 1;
                        }                       
                    }
                }
            }
        }
    }
    public void SetPause(bool setPause) { isPaused = setPause; }
    public bool GetPause() { return isPaused; }

    private void Update()
    {
        if (!isPaused && !timerStop)
        {
            if(pauseMenu.activeSelf)
                pauseMenu.SetActive(false);
            Time.timeScale = 1;
            timer -= Time.deltaTime;
        }
        else if (isPaused && !timerStop)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;

        }
        float sec = timer % 60;
        int mins = ((int)timer / 60);
        string seconds = "what";
        string minutes = "WHAT";
        if (sec < 60 && sec > 59.5f)
            minutes = (mins + 1).ToString();
        else
            minutes = mins.ToString();

        if (sec < 9.5f)
            seconds =  "0" + sec.ToString("f0");
        else if(sec < 60 && sec > 59.5f)
            seconds = "00";
        else
            seconds = sec.ToString("f0");
        timerText.text = minutes + ":" + seconds;

        if (scoreZones != null)
        {
            for (int i = 0; i < scoreZones.Count; i++)
            {
                scoreTexts[i].text = scoreZones[i].GetComponent<ScoreZones>().GetScore().ToString();

            }
        }
        if (timerText != null && !gameOver)
        {
            if (timer < 0)
            {
                //Camera.main.gameObject.SetActive(false);
                timerText.gameObject.SetActive(false);
                timerStop = true;
                Time.timeScale = 0;
                timerUI.gameObject.SetActive(false);
                //foreach (var scores in scoreZones)
                //{
                //    if (scores.GetComponent<ScoreZones>().GetScore() > topScore)
                //    {
                //        topScore = scores.GetComponent<ScoreZones>().GetScore();
                //        topPlayer = currentPlayer;
                //    }
                //    currentPlayer += 1;
                //}
                //timerText.text = "Player " + players[topPlayer].name + " Wins with a score of " + topScore.ToString();
                winScreen.SetActive(true);

                waterEndScreen.SetActive(true);
                int j = 1;
                for (int i = 0; i < players.Count; i++)
                {
                    MyPlayer player = spawnedPlayers[i].GetComponent<MyPlayer>();
                    string name = "Player " + j;
                    winnerSet.AddPlayers(player.playerIcon, scoreZones[i].GetComponent<ScoreZones>().GetScore(), name, player.name);
                    j += 1;
                }
                winnerSet.SortScores(players.Count);
                gameOver = true;
               for(int i = 0; i < players.Count; i ++)
                    spawnedPlayers[i].SetActive(false);
                levelObject.SetActive(false);
            }
        }
    }



}
