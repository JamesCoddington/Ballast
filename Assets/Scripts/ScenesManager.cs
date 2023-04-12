using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    public Scene SceneOrigin;
    
    public enum Scene
    {
        ST_MainMenu,
        ST_Peter,
        ST_GameOver,
    }

    private void Awake()
    {
        Instance = this;
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(scene.ToString());
    }

    // For making the main game when we start up
    public void LoadNewGame()
    {
        LoadScene(Scene.ST_Peter);
    }

    public void LoadMainMenu()
    {
        LoadScene(Scene.ST_MainMenu);
    }
}
