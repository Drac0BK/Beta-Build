using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    PlayerConfigManager playerConfigManager;
    public void Menu()
    {
        Time.timeScale = 1;
        PlayerConfigManager playerConfigManager;
        playerConfigManager = FindObjectOfType<PlayerConfigManager>();
        if (playerConfigManager != null)
            Destroy(GameObject.Find("PlayerConfigurationManager"));
        SceneManager.LoadScene("MainMenu");
    }
    public void CharacterSelect()
    {
        playerConfigManager = FindObjectOfType<PlayerConfigManager>();
        if(playerConfigManager != null)
            Destroy(playerConfigManager);
        SceneManager.LoadScene("PlayerSelect");
    }
    public void Level1Load()
    {
        SceneManager.LoadScene("Lvl1Scene");
    }
    public void Level2Load()
    {
        SceneManager.LoadScene("Lvl2Scene");
    }
    public void ReloadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LevelSelectScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelSelect");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
