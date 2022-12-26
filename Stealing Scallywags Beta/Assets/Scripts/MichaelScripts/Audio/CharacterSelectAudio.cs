using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityCore;
using UnityCore.Audio;

public class CharacterSelectAudio : MonoBehaviour
{
    public AudioManager audioManager;
    [SerializeField]
    private AudioSource audioSource;

    private bool toggle;

    // Start is called before the first frame update
    void Start()
    {
        int rand = ((int)Random.Range(1, 4));
        switch (rand)
        {
            case 1:
                audioManager.RestartAudio(UnityCore.Audio.AudioType.Announcer, "EnteringCharacterSelect_Line1", false, 0.0f);
                break;

            case 2:
                audioManager.RestartAudio(UnityCore.Audio.AudioType.Announcer, "EnteringCharacterSelect_Line2", false, 0.0f);
                break;

            case 3:
                audioManager.RestartAudio(UnityCore.Audio.AudioType.Announcer, "EnteringCharacterSelect_Line3", false, 0.0f);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.isPlaying == false && toggle == false)
        {
            int rand = ((int)Random.Range(1, 5));
            switch (rand)
            {
                case 1:
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Main_Theme", true, 0.0f);
                    break;

                case 2:
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Main_Theme", true, 0.0f);
                    break;

                case 3:
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Main_Theme", true, 0.0f);
                    break;
                case 4:
                    audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Main_Theme", true, 0.0f);
                    break;

            }
            toggle = true;
        }
        else
        {
            toggle = false;
        }
    }
}
