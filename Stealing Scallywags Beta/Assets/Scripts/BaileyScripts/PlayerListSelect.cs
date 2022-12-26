using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListSelect : MonoBehaviour
{
    public List<GameObject> playerPrefabs = new List<GameObject>();
    List<GameObject> takenPrefabs = new List<GameObject>();

    public List<GameObject> playerMesh = new List<GameObject>();
    public List<Texture> playerTextures = new List<Texture>();
    public List<Sprite> playerIcons = new List<Sprite>();
    public RawImage image;
    public RawImage icon;
    public GameObject playerPrefab;
    public GameObject displayPrefab;
    int count = 0;
    public PlayerSetupMenuController playerSetupMenuController;
    public PlayerConfigManager playerConfigManager;
    public List<PlayerConfiguration> playerConfigs;
    public Button cancel;

    List<bool> boolCheck = new List<bool>();

    bool add = true;

    private void Start()
    {
        playerConfigManager = FindObjectOfType<PlayerConfigManager>();
        playerPrefab = playerPrefabs[count];
        image.texture = playerTextures[count];
        icon.texture = playerIcons[count].texture;

        displayPrefab.GetComponent<MeshFilter>().mesh = playerMesh[count].GetComponent<MeshFilter>().sharedMesh;
        displayPrefab.GetComponent<MeshRenderer>().material = playerMesh[count].GetComponent<MeshRenderer>().sharedMaterial;

        ChangeCurrent();
    }

    public GameObject GetPrefab() { return playerPrefab; }
    public void SetPrefab() 
    { 
        playerPrefab = playerPrefabs[count]; 
        displayPrefab.GetComponent<MeshFilter>().mesh = playerMesh[count].GetComponent<MeshFilter>().sharedMesh;
        displayPrefab.GetComponent<MeshRenderer>().material = playerMesh[count].GetComponent<MeshRenderer>().sharedMaterial;
        image.texture = playerTextures[count];
        icon.texture = playerIcons[count].texture;
        ChangeCurrent();

    }
    public void NextCount() 
    { 
        count += 1;
        if(count >= 4) 
            count = 0;
        add = true;
        SetPrefab();
    }
    public void PrevCount()
    {
        count -= 1;
        if (count <= -1)
            count = 3;
        add=false;
        SetPrefab();
    }
    public void ChangeCurrent()
    {
        while(!CheckIfPossible())
        {
            if (add)
            {
                count++;
                if (count >= 4)
                    count = 0;
                SetPrefab();
            }
            else
            {
                count -= 1;
                if (count <= -1)
                    count = 3;
                SetPrefab();
            }
        }
        
    }


    bool CheckIfPossible()
    {
        playerConfigs = playerConfigManager.GetPlayerPrefabs();
        foreach(var prefabs in playerConfigs)
        {
            if (prefabs.playerPrefab == playerPrefab)
                return false;
        }

        return true;
    }

    public void SetPlayer()
    {
        if (CheckIfPossible())
        {
            playerSetupMenuController.SetPlayerPrefab(playerPrefab);
            playerSetupMenuController.ReadyPlayer();
            cancel.Select();
        }
    }
    public void ClearPlayer()
    {
        playerSetupMenuController.SetPlayerPrefab(null);
    }
    //        foreach(var button in confirmButtons)
    //        {
    //            if(i == count)
    //            {
    //                confirmButtons[i].gameObject.SetActive(true);
    //            }
    //            else
    //              confirmButtons[i].gameObject.SetActive(false);
    
    //            i += 1;
    //        }
}

