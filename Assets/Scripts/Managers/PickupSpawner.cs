using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float minPickupProbability;
    [SerializeField] private Pickup bossPickup;
    [SerializeField] private PickupSpawn[] pickups;

    private List<Pickup> pickupPool = new List<Pickup>();

    Pickup chosenPickup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (PickupSpawn spawn in pickups)
        {
            for (int i = 0; i < spawn.spawnAmount; i++)
            {
                pickupPool.Add(spawn.pickup);
            }
        }
    }

    public void SpawnPickup(Vector2 spawnPos)
    {
        if (pickupPool.Count == 0) { return; }

        //calculate pickup probability based off of level: https://www.desmos.com/calculator/djhkukw7ea
        int currentLevel = GameManager.GetInstance().scoreManager.Level;
        float probCalculation = 1f / currentLevel + minPickupProbability;
        float pickupProbability = Mathf.Clamp(probCalculation, 0f, 1f);
        Debug.Log($"Current level {currentLevel} has pickup probability of {pickupProbability}");

        if (Random.Range(0.0f, 1.0f) < pickupProbability)
        {
            chosenPickup = pickupPool[Random.Range(0, pickupPool.Count)];
            Instantiate(chosenPickup, spawnPos, Quaternion.identity);
        }
    }

    public void BossSpawnPickup(Vector2 spawnPos)
    {
        Instantiate(bossPickup, spawnPos, Quaternion.identity);
    }
}


[System.Serializable]
public struct PickupSpawn
{
    public Pickup pickup;
    public int spawnAmount;
}
