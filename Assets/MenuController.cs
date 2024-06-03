using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public Manager manager;
    
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Player1Scene");
        manager.currentPlayer = 0;
        manager.player1Score = 0;
        manager.player2Score = 0;
        
        
    }

    public void OpenSettings()
    {
        // Load the settings scene
        SceneManager.LoadScene("SettingsScene");
    }

    public void onPlayer1()
    {
        if (Manager.Instance != null)
        {
            PlayerPrefs.SetInt("Player1Score", Manager.Instance.player1Score);
            PlayerPrefs.SetInt("Player2Score", Manager.Instance.player2Score);
        }
        manager.currentPlayer = 0;
        SceneManager.LoadScene("Player1Scene");
    }

    public void onPlayer2()
    {
        if (Manager.Instance != null)
        {
            PlayerPrefs.SetInt("Player2Score", Manager.Instance.player2Score);
            PlayerPrefs.SetInt("Player1Score", Manager.Instance.player1Score);
        }
        manager.currentPlayer = 1;
        SceneManager.LoadScene("Player2Scene");
    }
    public void ExitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
