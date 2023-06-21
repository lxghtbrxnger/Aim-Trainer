using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuController : MonoBehaviour
{
    public TMP_Text[] highScoreTexts;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            if (PlayerPrefs.HasKey("score" + i))
            {
                highScoreTexts[i].text = "" + PlayerPrefs.GetInt("score" + i);
            }
            else
            {
                highScoreTexts[i].text = "" + 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartClicked()
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Scenes/GameScene");

    }

    void QuitClicked()
    {
        Debug.Log("Quit");
        Application.Quit();

    }
}
