using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using TMPro;

public class WinnerSetScreen : MonoBehaviour
{
    class ScoreList
    {
        public List<PlayerScore> list;
    }
    class PlayerScore
    {
        public string charName;
        public float score;
        public Image icon;
        public string prefabName;

        public PlayerScore(Image a_Icon, float a_score, string a_charName, string a_prefabName)
        {
            icon = a_Icon;
            score = a_score;
            charName = a_charName;
            prefabName = a_prefabName;
        }

    }
    public List<GameObject> winnerSpots = new List<GameObject>();
    public List<Image> images = new List<Image>();
    public List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> scores = new List<TextMeshProUGUI>();
    public List<Transform> playerSpots = new List<Transform>();
    public List<GameObject> playerResultsPrefab = new List<GameObject> ();


    ScoreList scoreList = new ScoreList();
    ScoreList newscoreList = new ScoreList();

    public void AddPlayers(Image a_Icon, float a_score, string a_charName, string a_prefabName)
    {
        if(scoreList.list == null)
        {
            scoreList.list = new List<PlayerScore>();
        }
        PlayerScore playerScore = new PlayerScore(a_Icon, a_score,a_charName,a_prefabName);
        scoreList.list.Add(playerScore);
    }

    public void SortScores(int playerCount)
    {
        newscoreList = scoreList;

        for(int i = 0; i < playerCount; i ++)
        {
            if(scoreList.list[i].prefabName.Contains("Shell"))
                Instantiate(playerResultsPrefab[0], playerSpots[i].position, playerSpots[i].rotation, transform);
            if (scoreList.list[i].prefabName.Contains("Lori"))
                Instantiate(playerResultsPrefab[1], playerSpots[i].position, playerSpots[i].rotation, transform);
            if (scoreList.list[i].prefabName.Contains("Bob"))
                Instantiate(playerResultsPrefab[2], playerSpots[i].position, playerSpots[i].rotation, transform);
            if (scoreList.list[i].prefabName.Contains("Angry"))
                Instantiate(playerResultsPrefab[3], playerSpots[i].position, playerSpots[i].rotation, transform);
        }



        newscoreList.list = scoreList.list.OrderBy(x => x.score).ToList();
        newscoreList.list.Reverse();


        for (int i = 0; i < playerCount; i++)
            SetUi(i, newscoreList.list[i].icon, newscoreList.list[i].score, newscoreList.list[i].charName);


        //int i = 0;
        //foreach(var item in scoreList.list)
        //{
        //    SetUi(i, item.icon,item.score,item.charName);
        //    i += 1;
        //}
    }

    void SetUi(int a_spot, Image a_Icon, float a_score, string a_charName)
    {
        winnerSpots[a_spot].SetActive(true);
        images[a_spot].sprite = a_Icon.sprite;
        names[a_spot].text = a_charName;
        scores[a_spot].text = a_score.ToString();
    }

}

