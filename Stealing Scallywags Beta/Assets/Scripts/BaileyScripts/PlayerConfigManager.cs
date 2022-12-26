using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    private PlayerControls controls;

    [SerializeField]
    private int maxPlayers = 2;

    public GameObject ready;
    public GameObject back;

    public static PlayerConfigManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
            Debug.Log("Make New Instance of Singleton");
        else
        {
            controls = new PlayerControls();
            instance = this; 
            DontDestroyOnLoad(instance); 
            playerConfigs = new List<PlayerConfiguration>();
            PlayerInputManager inputManager = GetComponent<PlayerInputManager>();
            Debug.Log(inputManager.playerCount);
        }
    }



    public void SetPlayerModel(int index, GameObject prefab)
    {
        playerConfigs[index].playerPrefab = prefab;
    }

    public void ReadyPlayer(int index)
    {

        playerConfigs[index].isReady = true;
        if (playerConfigs.Count >= maxPlayers/2 && playerConfigs.All(p=>p.isReady==true))
        {
            if(ready != null)
            ready.SetActive(true);
            return;
        }
    }
    public void UnReadyPlayer(int index)
    {
        playerConfigs[index].isReady = false;
        ready.SetActive(false);

        if (playerConfigs.Count >= maxPlayers / 2 && playerConfigs.All(p => p.isReady == true))
        {
            if (ready != null)
                ready.SetActive(true);
            return;
        }
    }

        public List<PlayerConfiguration> GetPlayerPrefabs()
    {
        return playerConfigs;
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        /*
        Debug.Log("Player Count " + PlayerInputManager.playerCount + "/" + manager.maxPlayerCount);
        player.actions = inputActions;
        if (manager.playerCount >= manager.maxPlayerCount  && manager.joiningEnabled)
        {
            manager.DisableJoining();
            Debug.Log("Disabled Join");
        }
         */

        Debug.Log("Joined " + pi.playerIndex + " - " + pi.devices[0].displayName);

        if (!playerConfigs.Any(p => p.playerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            playerConfigs.Add(new PlayerConfiguration(pi));
            playerConfigs[0].Input.onActionTriggered += Input_onActionTriggered;
        }
    }
    public void HandlePlayerQuit(PlayerInput pi)
    {
        Debug.Log("Quit: " + pi.playerIndex);
    }

   private void Input_onActionTriggered(CallbackContext obj)
   {
        Debug.Log(obj.action.name);
        if (obj.action.name == controls.Player.Cancel.name)
            QuitSelection();
        if (obj.action.name == controls.Player.Ready.name)
            StartGame();
    }

    public void QuitSelection()
    {
        if (back != null)
            if (back.activeSelf)
                back.GetComponent<Button>().onClick.Invoke();
    }
    public void StartGame()
    {
        if(ready != null)    
            if(ready.activeSelf)
                ready.GetComponent<Button>().onClick.Invoke();
    }

    public void test()
    {
        Debug.Log("Hello Wolrd");
    }
}

public class PlayerConfiguration
{ 


    public PlayerConfiguration(PlayerInput pi)
    {
        playerIndex = pi.playerIndex;
        Input = pi;
    }

    
    public PlayerInput Input { get; set; }

    public int playerIndex { get; set; }

    public bool isReady { get; set; }

    public GameObject playerPrefab { get; set; }
}

