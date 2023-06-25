using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    // Public variables.  In Unity, the editor allows you to inspect public variables while
    // playing which makes debugging much easier.  Unity also remembers the value you set as
    // a default, which means you can set up a lot of your game's constants in the Unity UI

    // MyTarget is a prefab - a sample target object that I clone to make new targets.  It
    // needs to be public so I can drag and drop it in place in the Unity editor
    public GameObject MyTarget;

    // These are public so they can be assigned in the Unity editor
    public TMP_Text ScoreText;
    public TMP_Text AccuracyText;
    public TMP_Text TimerText;

    // Score, timer and shot counts are private to this class
    private int _score = 0;
    private int _accuracy = 100;
    private int _totalShots = 0;
    private int _totalHits = 0;
    private float _seconds = 30;

    /**
     * Uses a Singleton pattern so that the game instance can be accessed
     * from other classes.  This instance is set up in Awake which is 
     * guaranteed to be called before Start or Update.
     */
    private static GameController _instance;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        } else
        {
            // Prevent any other instance from ever existing
            Destroy(this);
        }
    }


    /**
     * Create 5 targets when the game is started
     */
    void Start()
    {
        for(int i=0; i<5; i++)
        {
            InstantiateNewTarget();
        }
    }

    /**
     * Keeps track of the game state, including the timer and the accuracy.
     * Update is called many times a second so the score and accuracy count
     * is kept up to date.  The time between calls (deltaTime) is accurate
     * enough to keep the timer counting down.  
     */
    void Update()
    {
        _seconds -= Time.deltaTime;

        // Check if the game time is over
        if(_seconds <= 0)
        {
            // game over
            SaveScore();

            // Show the start menu & clear the game screen
            SceneManager.LoadScene("Scenes/MenuScene");
            SceneManager.UnloadSceneAsync("Scenes/GameScene");
        }

        // Set up the score display
        ScoreText.text = "" + _score;

        // Calculate the player's accuracy, as an integer
        if (_totalShots > 0) {
            _accuracy = (int) (((float) _totalHits / (float) _totalShots) * 100f);
        }

        // Display the Accuracy and Timer
        AccuracyText.text = "Acc: " + _accuracy + "%";
        TimerText.text = "" + ((int)_seconds + 1);
    }

    /**
     * Record a Hit on a target.  This will change the score and record the hit so
     * that the player's accuracy can be calculated.
     */
    public static void Hit()
    {
        _instance._score+=10;
        _instance._totalHits++;
        _instance._totalShots++;

        // Create a new target for the player to shoot at
        _instance.InstantiateNewTarget();
    }

    /**
     * Record a miss.  This decreases the score and records the shot so that the accuracy
     * can be updated.
     */
    public static void Miss()
    {
        _instance._score -= 15;
        _instance._totalShots++;
    }

    /**
     * Creates a new target in a random position.
     */
    private void InstantiateNewTarget()
    {
        Instantiate(MyTarget, new Vector3(
            Random.Range(TargetBehaviour.MinX, TargetBehaviour.MaxX),
            Random.Range(TargetBehaviour.MinY, TargetBehaviour.MaxY),
            Random.Range(TargetBehaviour.MinZ, TargetBehaviour.MaxZ)
            ), Quaternion.identity);
    }

    /**
     * The high scores are saved in the Player Preferences, which is a Unity API that
     * stores some values in the registry on Windows.  We save the top 10 scores, even
     * though we only show 5 on the menu screen.  When a game ends, we see if the players
     * score should be saved by looping through the scores, and comparing the saved scores
     * to the current score.  If the current score is higher, then we shuffle all the other
     * scores down the list.
     */
    private void SaveScore()
    {
        int[] scores = new int[10];
        for(int i=0; i<scores.Length; i++)
        {
            // when the game first runs, the scores will not be set
            if (PlayerPrefs.HasKey("score" + i))
            {
                scores[i] = PlayerPrefs.GetInt("score" + i);
            } else
            {
                // if the score is not set, make it zero.
                scores[i] = 0;
            }
        }

        int arrayIndex = 0;
        for (int i = 0; i < scores.Length; i++)
        {
            int s = scores[arrayIndex];
            if(_score > s)
            {
                // This is a high score - save it in the array
                s = _score;
                _score = -1;
            } else
            {
                // The existing score is higher, skip over it and compare the next score
                arrayIndex++;
            }
            PlayerPrefs.SetInt("score" + i, s);
        }
    }
}
