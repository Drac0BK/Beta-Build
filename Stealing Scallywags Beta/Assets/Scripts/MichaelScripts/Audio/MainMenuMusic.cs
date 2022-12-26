using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityCore;
using UnityCore.Audio;

public class MainMenuMusic : MonoBehaviour
{
    public AudioManager audioManager;
    [SerializeField]
    private AudioSource audioSource;
    private bool toggle;

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //Debug.Log(audioSource.isPlaying);

        if (audioSource.isPlaying == false)
        {
            if (toggle == true)
            {               
                audioManager.PlayAudio(UnityCore.Audio.AudioType.BGM, "Main_Theme", true, 0.0f);
                       
                toggle = false;
            }
            else
            {
                toggle = true;
            }

        }

        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            Destroy(gameObject);
        }
    }
}
