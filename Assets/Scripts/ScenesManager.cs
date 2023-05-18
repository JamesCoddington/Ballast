using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public Scene SceneOrigin;
    public GameObject Player;
    
    public enum Scene
    {
        ST_MainMenu,
        S_IntroCutscene,
        S_MainGame,
        ST_GameOver,
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(Scene scene)
    {
        //string previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene.ToString());
        Player.SetActive(false);
        // SceneManager.UnloadSceneAsync(previousScene);
    }

    // For making the main game when we start up
    public void LoadNewGame()
    {
        LoadScene(Scene.S_IntroCutscene);
    }

    public void LoadMainMenu()
    {
        LoadScene(Scene.ST_MainMenu);
    }

    public void LoadMainGame()
    {
        LoadScene(Scene.S_MainGame);
    }
}
