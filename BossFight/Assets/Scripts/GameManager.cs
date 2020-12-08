using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

// Big monobehavior for the game. Get using the find method.

public class GameManager : MonoBehaviour
{
    // Simple simpleton instance.
    public static GameManager instance;

    // Public layers.
    public const int SOLID_LAYER = 9;
    public const int PLAYER_LAYER = 10;
    public const int ENEMY_LAYER = 11;

    // Pause menu things
    public GameObject pauseMenu;
    public GameObject resumeButton;
    public GameObject mainMenuButton1;

    // Game over menu
    public GameObject gameOverMenu;
    public GameObject mainMenuButton2;
    private bool isGameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        // Make sure there's only one instance of this class.
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        // Set some defaults for replayability.
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);


        // Set the onclick of the buttons.
        resumeButton.GetComponent<Button>().onClick.AddListener(() => { UnpauseGame(); });
        mainMenuButton1.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadSceneAsync(0); });
        mainMenuButton2.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadSceneAsync(0); });
    }

    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (pauseMenu.activeInHierarchy == false)
                PauseGame();
            else
                UnpauseGame();
        }

        //InputSystem.Update();
    }


    public void PauseGame()
    {
        // Stop time, show the pause menu.
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void UnpauseGame()
    {
        // Hide the pause menu, start time again.
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        // Stop time, update point counter, show menu.
        gameOverMenu.SetActive(true);
        gameOverMenu.transform.GetChild(1).GetComponent<Text>().text = "Game over";
        Time.timeScale = 0.0f;
    }

    public void GameWon()
    {
        // Stop time, update point counter, show menu.
        gameOverMenu.SetActive(true);
        gameOverMenu.transform.GetChild(1).GetComponent<Text>().text = "You win!";
        Time.timeScale = 0.0f;
    }
}
