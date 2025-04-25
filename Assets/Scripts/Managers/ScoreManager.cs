using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    private const string HIGH_LEVEL_PREF = "HighLevel";
    private const string HIGH_SCORE_PREF = "HighScore";

    //fields
    private int _score;
    private int _level;
    private int _highScore;
    private int _highLevel;
    private int _numNukes;
    private int _maxNukes;

    //unity events
    public UnityEvent OnScoreUpdate;
    public UnityEvent OnLevelUpdate;
    public UnityEvent OnHighScoreUpdate;
    public UnityEvent OnHighLevelUpdate;
    public UnityEvent OnNukesUpdate;


    //properties
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            OnScoreUpdate?.Invoke();
            TryUpdateHighScore();
        }
    }

    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;
            OnLevelUpdate?.Invoke();
            TryUpdateHighLevel();
        }
    }

    public int HighScore
    {
        get { return _highScore; }
        set
        {
            _highScore = value;
            OnHighScoreUpdate?.Invoke();
        }
    }

    public int HighLevel
    {
        get { return _highLevel; }
        set
        {
            _highLevel = value;
            OnHighLevelUpdate?.Invoke();
        }
    }

    public int NumNukes
    {
        get { return _numNukes; }
        set
        {
            _numNukes = value;
            if (_numNukes >= _maxNukes) { _numNukes = _maxNukes; } //restrict to max of array
            OnNukesUpdate?.Invoke();
        }
    }

    void Start()
    {
        HighScore = PlayerPrefs.GetInt(HIGH_SCORE_PREF);
        HighLevel = PlayerPrefs.GetInt(HIGH_LEVEL_PREF);
    }

    public void GameStart()
    {
        Score = 0;
        Level = 1;
        NumNukes = 0;
        OnScoreUpdate?.Invoke();
        OnLevelUpdate?.Invoke();
        OnHighScoreUpdate?.Invoke();
        OnHighLevelUpdate?.Invoke();
        OnNukesUpdate?.Invoke();
    }

    public void GameOver()
    {
        StoreHighScore();
        StoreHighLevel();
    }


    public void IncrementScore()
    {
        Score++;
    }

    public void IncrementLevel()
    {
        //added if statement to avoid incrementing level when coroutine is running but player dies (which ends up incrementing level even after player dies)
        if (GameManager.GetInstance().IsPlaying())
        {
            Level++;
        }

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
        _maxNukes = max;
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
        PlayerPrefs.SetInt(HIGH_SCORE_PREF, _highScore);
    }

    public void TryUpdateHighLevel()
    {
        if (Level > HighLevel)
        {
            HighLevel = Level;
        }
    }

    public void StoreHighLevel()
    {
        PlayerPrefs.SetInt(HIGH_LEVEL_PREF, _highLevel);
    }

}
