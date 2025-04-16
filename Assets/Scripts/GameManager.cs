using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Entities")]
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Transform[] spawnPositions;

    [Header("Game Variables")]
    [SerializeField] private float enemySpawnRate;

    private bool isEnemySpawning;

    public ScoreManager scoreManager;

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
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     CreateEnemy();
        // }
    }

    void CreateEnemy()
    {
        //melee enemy right now
        Enemy tempEnemy = Instantiate(enemyPrefab);
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

}
