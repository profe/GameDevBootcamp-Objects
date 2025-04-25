using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("GamePlay")]
    [Range(0, 1)]
    [SerializeField] private float initialEnemySpawnRate;
    [SerializeField] private float levelWaitTime; //in seconds

    [Header("Game Entities")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Managers")]
    [SerializeField] public ScoreManager scoreManager;
    [SerializeField] public SoundManager soundManager;
    [SerializeField] public PickupSpawner pickupSpawner;

    public UnityEvent OnGameStart;
    public UnityEvent OnGameOver;


    private Player player;
    private bool isEnemySpawning;
    private bool isPlaying;

    private static GameManager instance;


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
        // isEnemySpawning = true;
        // StartCoroutine(EnemySpawner());
        // StartCoroutine(LevelIncreaser());
    }

    void CreateEnemy()
    {
        //create random enemies continuously
        Enemy tempEnemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)]);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
    }

    //continously spawn enemies using coroutine
    IEnumerator EnemySpawner()
    {
        while (isEnemySpawning)
        {
            float currentRate = GetEnemySpawnRate();
            //Debug.Log($"Current Level {scoreManager.Level} has enemy spawn rate of {currentRate}");
            yield return new WaitForSeconds(1.0f / currentRate);
            CreateEnemy();
        }
    }

    //continuosly increase level (which increases enemy spawning) using coroutine
    IEnumerator LevelIncreaser()
    {
        while (isPlaying)
        {
            yield return new WaitForSeconds(levelWaitTime);
            scoreManager.IncrementLevel();
            //add some kind of notification popup or sound here?
            Debug.Log($"Increased level to {scoreManager.Level}");
        }
    }

    public void SetEnemySpawnState(bool status)
    {
        isEnemySpawning = status;
    }

    public float GetEnemySpawnRate()
    {
        float rate = Mathf.Log10(scoreManager.Level) + initialEnemySpawnRate; //rate results in logarithmic ramp up https://www.desmos.com/calculator/evhaxcv1jt
        return rate; //Mathf.Clamp(rate, 0.0f, 1.0f);
    }



    public void PlaySound(Sound sound)
    {
        soundManager.PlaySound(sound);
    }

    public Player GetPlayer()
    {
        return player;
    }

    public bool IsPlaying()
    {
        return isPlaying;
    }

    public void NotifyDeath(Enemy enemy)
    {
        pickupSpawner.SpawnPickup(enemy.transform.position);
    }






    public void StartGame()
    {
        player = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity).GetComponent<Player>();
        player.OnDeath += StopGame;

        OnGameStart?.Invoke();
        StartCoroutine(GameStarter());
    }

    IEnumerator GameStarter()
    {
        isEnemySpawning = true;
        isPlaying = true;
        yield return new WaitForSeconds(2);
        StartCoroutine(EnemySpawner());
        StartCoroutine(LevelIncreaser());
    }



    public void StopGame()
    {
        StopCoroutine("EnemySpawner");
        StopCoroutine("LevelIncreaser");
        scoreManager.StoreHighScore();
        scoreManager.StoreHighLevel();
        StartCoroutine(GameStopper());
    }

    IEnumerator GameStopper()
    {
        GameManager.GetInstance().PlaySound(Sound.ResetGame);
        isEnemySpawning = false;
        isPlaying = false;
        yield return new WaitForSeconds(2);

        DestroyAllEnemies();
        DestroyAllPickups();

        OnGameOver?.Invoke();
    }


    //should not change score!
    public void DestroyAllEnemies()
    {
        foreach (Enemy item in FindObjectsByType<Enemy>(FindObjectsSortMode.None)) //dont sort when finding, make it faster
        {
            Destroy(item.gameObject);
        }
    }

    //should not apply pickups!
    public void DestroyAllPickups()
    {
        foreach (Pickup item in FindObjectsByType<Pickup>(FindObjectsSortMode.None)) //dont sort when finding, make it faster
        {
            Destroy(item.gameObject);
        }
    }
}
