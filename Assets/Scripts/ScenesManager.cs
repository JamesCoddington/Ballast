using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    
    public enum Scene
    {
        ST_MainMenu,
        ST_Jfames,
        ST_Peter,
        ST_GameOver,
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    // For making the main game when we start up
    public void LoadNewGame()
    {
        LoadScene(Scene.ST_Jfames);
    }

    public void LoadMainMenu()
    {
        LoadScene(Scene.ST_MainMenu);
    }
}
