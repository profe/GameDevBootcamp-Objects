using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Entities")]
    [SerializeField] private Player player;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;

    [Header("Managers")]
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] SoundManager soundManager;

    private static GameManager instance;

    private bool isEnemySpawning;

    public static GameManager GetInstance()
    {
        return instance;
    }

    private void SetSingleton()
    {
        if (instance != null && instance != this)
        {
            Destroy(this); //destroy this if other copy exists
        }

        instance = this;
    }

    private void Awake() //called before start
    {
        SetSingleton();
    }

    void Start()
    {
        isEnemySpawning = true;
        StartCoroutine(EnemySpawner());
    }

    void CreateEnemy()
    {
        //create random enemies continuously
        Enemy tempEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]);
        tempEnemy.transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }

    //continously using coroutine
    IEnumerator EnemySpawner()
    {
        while (isEnemySpawning)
        {
            yield return new WaitForSeconds(1.0f / enemySpawnRate);
            CreateEnemy();
        }
    }

    public void SetEnemySpawnState(bool status)
    {
        isEnemySpawning = status;
    }

    public void ResetGame()
    {
        GameManager.GetInstance().PlaySound(Sound.ResetGame);
        //reset player (location, health)
        player.transform.position = playerSpawnPoint.transform.position;
        player.FullHeal();
        //check if new highscore and set if so
        scoreManager.TryUpdateHighScore();
        //reset score
        scoreManager.Score = 0;
        //remove all enemies
        DestroyAllEnemies();
    }

    //should not change score!
    public void DestroyAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public void PlaySound(Sound sound)
    {
        soundManager.PlaySound(sound);
    }
}
