using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [Header("Game Entities")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Enemy[] enemyPrefabs;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;

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
        isEnemySpawning = true;

        StartCoroutine(EnemySpawner());
    }

    void CreateEnemy()
    {
        //create random enemies continuously
        Enemy tempEnemy = Instantiate(enemyPrefabs[UnityEngine.Random.Range(0, enemyPrefabs.Length)]);
        tempEnemy.transform.position = spawnPositions[UnityEngine.Random.Range(0, spawnPositions.Length)].position;
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
        isPlaying = true;

        OnGameStart?.Invoke();
        StartCoroutine(GameStarter());
    }

    IEnumerator GameStarter()
    {
        yield return new WaitForSeconds(2);
        isEnemySpawning = true;
        StartCoroutine(EnemySpawner());
    }



    public void StopGame()
    {
        isEnemySpawning = false;
        //invoke on game over?
        scoreManager.StoreHighScore();
        StartCoroutine(GameStopper());
    }

    IEnumerator GameStopper()
    {
        GameManager.GetInstance().PlaySound(Sound.ResetGame);
        isEnemySpawning = false;
        yield return new WaitForSeconds(2);
        isPlaying = false;

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
