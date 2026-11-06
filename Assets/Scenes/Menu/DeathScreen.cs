using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static bool PlayerIsDead = false;

    public GameObject pauseMenuUI;

    private bool deathHandled = false; // Flag to ensure death handling only occurs once

    void Update()
    {
        PlayerHPandST playerHpandST = FindObjectOfType<PlayerHPandST>();
        if (playerHpandST.health <= 0 && !deathHandled)
        {
            deathHandled = true; // Set the flag to true to prevent further death handling
            Pause();
        }
        else if (playerHpandST.health > 0 && deathHandled)
        {
            deathHandled = false; // Reset the flag if player's health increases
            Resume();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PlayerIsDead = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PlayerIsDead = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
