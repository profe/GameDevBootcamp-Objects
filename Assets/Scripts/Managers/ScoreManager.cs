using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    //fields
    private int score;
    private int highScore;
    private int numNukes;
    private int maxNukes;

    //unity events
    public UnityEvent OnScoreUpdate;
    public UnityEvent OnHighScoreUpdate;
    public UnityEvent OnNukesUpdate;

    //properties
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            OnScoreUpdate?.Invoke();
            TryUpdateHighScore();
        }
    }

    public int HighScore
    {
        get { return highScore; }
        set
        {
            highScore = value;
            OnHighScoreUpdate?.Invoke();
        }
    }

    public int NumNukes
    {
        get { return numNukes; }
        set
        {
            numNukes = value;
            if (numNukes >= maxNukes) { numNukes = maxNukes; } //restrict to max of array
            OnNukesUpdate?.Invoke();
        }
    }

    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
    }

    public void GameStart()
    {
        Score = 0;
        NumNukes = 0;
        OnScoreUpdate?.Invoke();
        OnHighScoreUpdate?.Invoke();
        OnNukesUpdate?.Invoke();
    }

    public void GameOver()
    {
        StoreHighScore();
    }


    public void IncrementScore()
    {
        Score++;
    }

    public void IncrementNukes()
    {
        NumNukes++;
    }

    public void DecrementNukes()
    {
        NumNukes--;
    }

    public void SetMaxNukes(int max)
    {
        maxNukes = max;
    }

    public void TryUpdateHighScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
        }
    }

    public void StoreHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
    }

}
