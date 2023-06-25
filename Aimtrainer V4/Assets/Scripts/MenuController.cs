using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuController : MonoBehaviour
{
    // Public variables.  In Unity, the editor allows you to inspect public variables while
    // playing which makes debugging much easier.  Unity also remembers the value you set as
    // a default, which means you can set up a lot of your game's constants in the Unity UI
    public TMP_Text[] HighScoreText;

    /**
     * The menu controller's start gets called when we show the menu screen.  It shows the high scores
     */
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            if (PlayerPrefs.HasKey("score" + i))
            {
                HighScoreText[i].text = "" + PlayerPrefs.GetInt("score" + i);
            }
            else
            {
                HighScoreText[i].text = "" + 0;
            }
        }
    }

    /**
     * Update does nothing as there is nothing to move around on the menu
     */
    void Update()
    {
        
    }

    /**
     * This function handles when the player clicks start.  The Game Scene is loaded which
     * replaces the main menu.
     */
    void StartClicked()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Scenes/GameScene");
    }

    /**
     * Handles when the player clicks quit and stops the game
     */
    void QuitClicked()
    {
        Debug.Log("Quit");
        Application.Quit();

    }
}
