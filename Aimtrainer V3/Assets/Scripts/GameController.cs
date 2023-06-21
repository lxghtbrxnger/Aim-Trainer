using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject myTarget;
    public TMP_Text scoreText;
    public TMP_Text accuracyText;
    public TMP_Text timerText;
    private int score = 0;
    private int accuracy = 100;
    private int totalShots = 0;
    private int totalHits = 0;
    private float seconds = 30;

    public static GameController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            InstantiateNewTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;

        if(seconds <= 0)
        {
            // game over
            SaveScore();

            SceneManager.LoadScene("Scenes/MenuScene");
            SceneManager.UnloadScene("Scenes/GameScene");
        }


        scoreText.text = "" + score;

        if (totalShots > 0) {
            accuracy = (int) (((float) totalHits / (float) totalShots) * 100f);
        }

        accuracyText.text = "Acc: " + accuracy + "%";
        timerText.text = "" + ((int)seconds + 1);
    }

    public static void Hit()
    {
        instance.score+=10;
        instance.totalHits++;

        instance.InstantiateNewTarget();
    }

    public static void Miss()
    {
        instance.score -= 15;
    }

    public static void Shot()
    {
        instance.totalShots++;
    }


    private void InstantiateNewTarget()
    {
        Instantiate(myTarget, new Vector3(
            Random.Range(TargetBehaviour.MIN_X, TargetBehaviour.MAX_X),
            Random.Range(TargetBehaviour.MIN_Y, TargetBehaviour.MAX_Y),
            Random.Range(TargetBehaviour.MIN_Z, TargetBehaviour.MAX_Z)
            ), Quaternion.identity);
    }

    private void SaveScore()
    {
        int[] scores = new int[10];
        for(int i=0; i<scores.Length; i++)
        {
            if(PlayerPrefs.HasKey("score" + i))
            {
                scores[i] = PlayerPrefs.GetInt("score" + i);
            } else
            {
                scores[i] = 0;
            }
        }

        int arrayIndex = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            int s = scores[arrayIndex];
            if(score > s)
            {
                s = score;
                score = -1;
            } else
            {
                arrayIndex++;
            }
            PlayerPrefs.SetInt("score" + i, s);
        }

    }
}
