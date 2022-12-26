using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityCore;
using UnityCore.Audio;

public class EventScript : MonoBehaviour
{
    public GameObject krakens;

    public Kraken kraken;
    public FallingEvent fallEvent;
    public Wave wave;

    [SerializeField]
    bool Lv1, Lv2;

    [SerializeField]
    private Image focus, krakenAnnounce, tsunamiAnnounce, cursedAnnounce, collapseAnnounce;
    private Image imageInAction;
    private float announcementTimer;

    //public ShipCannon cannon;
    List<GameObject> scoreZones = new List<GameObject>();
    public List<GameObject> cursedTreasure = new List<GameObject>();
    float eventTimer = 100;

    public float minTimeInclusive = 5;
    public float maxTimeExclusive = 12;
    bool hasOccured = false;
    bool isKraken = false;
    bool isWave = false;
    bool isCursed = false;
    bool isStalactite = false;

    public AudioManager audioManager;
    [SerializeField]
    private AudioSource bgmAudioSource;
    private bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        scoreZones = GetComponent<GameManager>().scoreZones;
        int chance = Random.Range(0, 100);
        if (Lv1)
        {
            if (chance >= 0 && chance <= 32)
                isKraken = true;
            else if (chance >= 33 && chance <= 65)
                isWave = true;
            else if (chance >= 66 && chance <= 99)
                isCursed = true;
        }
        else if (Lv2)
        {
            if (chance >= 0 && chance <= 32)
                isKraken = true;
            else if (chance >= 33 && chance <= 65)
                isStalactite = true;
            else if (chance >= 66 && chance <= 99)
                isCursed = true;
        }


        eventTimer = Random.Range(minTimeInclusive, maxTimeExclusive);
    }

    // Update is called once per frame
    void Update()
    {

        eventTimer -= Time.deltaTime;
        


        if (eventTimer <= 0 && !hasOccured)
        {
            if (isKraken)
            {
                krakens.SetActive(true);

                int voicelineNumber;
                voicelineNumber = Random.Range(0, 2);
                switch (voicelineNumber)
                {
                    case 0:
                        audioManager.PlayAudio(UnityCore.Audio.AudioType.Announcer, "KrakenAnnouce_Line1", false, 0.0f);
                        break;

                    case 1:
                        audioManager.PlayAudio(UnityCore.Audio.AudioType.Announcer, "KrakenAnnouce_Line2", false, 0.0f);
                        break;
                }
                imageInAction = krakenAnnounce;
                announcementTimer = 3f;
                //Debug.Log(voicelineNumber);
                new WaitForSeconds(5.5f);
                kraken.FindPlayerSpot();
            }
            else if (isWave)
            {
                wave.WaveBegin();

                int voicelineNumber;
                voicelineNumber = Random.Range(0, 2);
                switch (voicelineNumber)
                {
                    case 0:
                        audioManager.PlayAudio(UnityCore.Audio.AudioType.Announcer, "TsunamiAnnounce_Line1", false, 0.0f);
                        break;

                    case 1:
                        audioManager.PlayAudio(UnityCore.Audio.AudioType.Announcer, "TsunamiAnnounce_Line2", false, 0.0f);
                        break;
                }
                imageInAction = tsunamiAnnounce;
                announcementTimer = 3f;
            }
            else if (isCursed)
            {
                SpawnCursedTreasure();

                int voicelineNumber;
                voicelineNumber = Random.Range(0, 2);
                switch (voicelineNumber)
                {
                    case 0:
                        audioManager.PlayAudio(UnityCore.Audio.AudioType.Announcer, "CursedAnnounce_Line1", false, 0.0f);
                        break;

                    case 1:
                        audioManager.PlayAudio(UnityCore.Audio.AudioType.Announcer, "CursedAnnounce_Line2", false, 0.0f);
                        break;
                }
                imageInAction = cursedAnnounce;
                announcementTimer = 3f;
            }
            else if(isStalactite)
            {
                imageInAction = collapseAnnounce;
                fallEvent.gameObject.SetActive(true);
                announcementTimer = 3f;
            }
            hasOccured = true;  
        }


        if(bgmAudioSource != null)
        if (bgmAudioSource.isPlaying == false)
        {
            if (toggle == true)
            {
                if (!isKraken && !isWave && !isCursed)
                {
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Main_Theme", false, 0.0f);
                }
                else if (isKraken)
                {
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Kraken_Theme", false, 0.0f);
                }
                else if (isWave || isStalactite)
                {
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Tsunami_Theme", false, 0.0f);
                }
                else if (isCursed)
                {
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Cursed_Treasure_Theme", false, 0.0f);
                }
                else
                {
                    Debug.LogError("System is in event time: " + hasOccured + ". System is not registering any song to play. Also, this error was made by Michael, blame him");
                }
                toggle = false;
            }
            else
            {
                toggle = true;
            }

        }

        if (imageInAction != null)
        {

            if (announcementTimer > 0)
            {
                announcementTimer -= Time.deltaTime;
                imageInAction.gameObject.SetActive(true);
                focus.gameObject.SetActive(true);
            }
            else if (imageInAction.gameObject.activeInHierarchy == true)
            {
                imageInAction.gameObject.SetActive(false);
                focus.gameObject.SetActive(false);
            }
        }
    }

    void SpawnCursedTreasure()
    {
        int cursedTreasureSpot = 0;
        if (cursedTreasure.Count != 0)
        {
            foreach (var item in scoreZones)
            {
                cursedTreasureSpot = Random.Range(0, cursedTreasure.Count);
                Instantiate(cursedTreasure[cursedTreasureSpot], item.transform.position + (Vector3.up * 20), item.transform.rotation, transform);
            }
        }
    }
}
