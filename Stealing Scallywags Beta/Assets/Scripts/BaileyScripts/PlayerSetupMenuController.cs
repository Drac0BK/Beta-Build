using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerSetupMenuController : MonoBehaviour
{
    private int playerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private TextMeshProUGUI readyText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;
    [SerializeField]
    private Button confirmButton;

    private float ignoreInputTime = 0.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        playerIndex = pi;
        titleText.SetText("Player " + (pi+1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }

    private void Update()
    {
        if (Time.timeScale != 1)
            Time.timeScale = 1;

        if(Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }

    public void SetPlayerPrefab(GameObject playerModel)
    {
        if(!inputEnabled) { return; }

        Debug.Log("Input recieved");
        PlayerConfigManager.instance.SetPlayerModel(playerIndex, playerModel);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }

    public void UndoPlayerPrefab()
    {
        if (!inputEnabled) { return; }

        Debug.Log("Input recieved");
        PlayerConfigManager.instance.SetPlayerModel(playerIndex, null);
        readyPanel.SetActive(false);
        confirmButton.Select();
        menuPanel.SetActive(true);
    }

    public void ReadyPlayer()
    {
        if(!inputEnabled) { return; }

        PlayerConfigManager.instance.ReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(false);
        readyText.gameObject.SetActive(true);
    }

    public void UnReadyPlayer()
    {
        if (!inputEnabled) { return; }

        PlayerConfigManager.instance.UnReadyPlayer(playerIndex);
        readyButton.gameObject.SetActive(true);
        readyText.gameObject.SetActive(false);
        confirmButton.Select();
    }

}
