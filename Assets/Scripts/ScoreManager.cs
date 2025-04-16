using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    //fields
    private int score;
    private int highScore;

    //unity events
    public UnityEvent OnScoreUpdate;

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
        set { highScore = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void IncrementScore()
    {
        Score++;
    }


}
