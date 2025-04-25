using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{

    [Header("Gameplay")]
    [SerializeField] private TMP_Text txtHealth;
    [SerializeField] private TMP_Text txtScore;
    [SerializeField] private TMP_Text txtHighScore;
    [SerializeField] private TMP_Text txtLevel;
    [SerializeField] private TMP_Text txtHighLevel;

    [Header("Menu")]
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject labelGameOver;
    [SerializeField] private TMP_Text menuHighScore;
    [SerializeField] private TMP_Text menuHighLevel;

    [Header("NukeUI")]
    [SerializeField] private GameObject[] nukes;

    private Player player;
    private ScoreManager scoreManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        scoreManager = GameManager.GetInstance().scoreManager;
        scoreManager.SetMaxNukes(nukes.Length);
        UpdateHighScore();
        UpdateHighLevel();
    }

    private void OnDisable()
    {
        //unsubscribe from c# action
        //player.health.OnHealthUpdate -= UpdateHealth;
    }

    public void UpdateHealth(float currentHealth)
    {
        txtHealth.SetText(currentHealth.ToString("##0.")); //showing no floating point, force first digit 
    }

    public void UpdateScore()
    {
        txtScore.SetText(scoreManager.Score.ToString());
    }

    public void UpdateLevel()
    {
        txtLevel.SetText(scoreManager.Level.ToString());
    }

    public void UpdateHighScore()
    {
        txtHighScore.SetText(scoreManager.HighScore.ToString());
        menuHighScore.SetText($"High Score : {scoreManager.HighScore}");
    }

    public void UpdateHighLevel()
    {
        txtHighLevel.SetText(scoreManager.HighLevel.ToString());
        menuHighLevel.SetText($"High Level: {scoreManager.HighLevel}");
    }

    public void UpdateNukes()
    {

        for (int i = 0; i < nukes.Length; i++)
        {
            if (i < scoreManager.NumNukes)
            {
                nukes[i].SetActive(true);
            }
            else
            {
                nukes[i].SetActive(false);
            }
        }
    }

    public void GameStarted()
    {
        player = GameManager.GetInstance().GetPlayer();
        player.health.OnHealthUpdate += UpdateHealth;
        menuCanvas.SetActive(false);
    }

    public void GameOver()
    {
        player.health.OnHealthUpdate -= UpdateHealth;
        labelGameOver.SetActive(true);
        menuCanvas.SetActive(true);
    }
}
