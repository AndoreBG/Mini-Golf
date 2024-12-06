using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MenuPrincipal()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ComoJogar()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void SairJogo()
    {
        Application.Quit();
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
    }
}
