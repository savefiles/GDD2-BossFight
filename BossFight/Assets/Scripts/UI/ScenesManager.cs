﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{

    // Get references to all of the scenes.
    Scene s_mainMenu;
    Scene s_inGame;

    // Credits screen.
    public GameObject credits;
    private bool isCreditsDisplayed = false;

    void Awake()
    {
        // Keep this object around through the scenes (not necessary).
        // DontDestroyOnLoad(gameObject);



        // Get the scenes.
        s_mainMenu = SceneManager.GetSceneByBuildIndex(0);
        s_inGame = SceneManager.GetSceneByBuildIndex(1);

        // Set the onclick of the level button
        GameObject.Find("Tutorial Button").GetComponent<Button>().onClick.AddListener(() => { LoadScene(1); });
        GameObject.Find("Start Game Button").GetComponent<Button>().onClick.AddListener(() => { LoadScene(2); });

        // Set the onclick of the quit game
        GameObject.Find("Quit Button").GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); });

        // Set the onclick of both resume buttons
        GameObject.Find("Credits Button").GetComponent<Button>().onClick.AddListener(() => { ToggleCredits(); });
        credits.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { ToggleCredits(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void ToggleCredits()
    {
        Debug.Log("Clicked");
        isCreditsDisplayed = !isCreditsDisplayed;
        credits.SetActive(isCreditsDisplayed);
    }
}
