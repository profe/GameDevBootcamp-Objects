using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    //fields
    private int score;
    private int highScore;

    //unity events
    public UnityEvent OnScoreUpdate;
    public UnityEvent OnHighScoreUpdate;

    //properties
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            OnScoreUpdate?.Invoke();
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

    public void IncrementScore()
    {
        Score++;
    }

    public void TryUpdateHighScore()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
        }
    }

}
