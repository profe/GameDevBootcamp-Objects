using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text txtHealth, txtScore, txtHighScore;
    [SerializeField] private Player player;

    private ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreManager = GameManager.GetInstance().scoreManager;
        player.health.OnHealthUpdate += UpdateHealth; //subscribe to C# action

    }

    private void OnDisable()
    {
        //unsubscribe from c# action
        player.health.OnHealthUpdate -= UpdateHealth;
    }

    public void UpdateHealth(float currentHealth)
    {
        txtHealth.SetText(currentHealth.ToString(".0")); //showing float with at least one decimal place
    }

    public void UpdateScore()
    {
        txtScore.SetText(scoreManager.Score.ToString());
    }

    public void UpdateHighScore()
    {
        txtHighScore.SetText(scoreManager.HighScore.ToString());
    }
}
