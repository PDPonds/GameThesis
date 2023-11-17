using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button startGame;
    public Button exitGame;

    private void Awake()
    {
        startGame.onClick.AddListener(() => StartGame());
        exitGame.onClick.AddListener(() => ExitGame());
    }

    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void ExitGame()
    {
        Application.Quit();
    }

}
