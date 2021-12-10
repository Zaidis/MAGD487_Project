using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] List<BuyableItems> enemies, chests;
    [SerializeField] float budget = 1000, spentBudget = 0;
    [SerializeField] float percentageOfEnemyBudget = 1, percentageOfChestBudget = 1;
    List<MapBlock> mapBlocks;
    [SerializeField] float dungeonLevelMultiplier = 100;
    // Start is called before the first frame update
    void Start()
    {
        mapBlocks = MapGenerator.mapBlocksSpawned;

        //Determine extra budget due to dungeon level
        budget += StateController.dungeonLevel * dungeonLevelMultiplier;
        //First Spawn Enemies
        SpawnEnemies();
        //Spawn Chests
        SpawnChests();
    }

    void SpawnEnemies()
    {
        float spendableAmount = (budget * percentageOfEnemyBudget);
        float spent = 0;
        while(spent < spendableAmount)
        {
            int rand = Random.Range(0, enemies.Count);
            int rand2 = Random.Range(0, mapBlocks.Count);
            Instantiate(enemies[rand].item, mapBlocks[rand2].GetRandomEnemySpawn(), Quaternion.identity);
            spent += enemies[rand].cost;
        }
        spentBudget += spent;
    }

    void SpawnChests()
    {
        float spendableAmount = (budget * percentageOfChestBudget);
        float spent = 0;
        int attempts = 0;
        int threshold = 100;
        while (spent < spendableAmount)
        {
            int rand = Random.Range(0, chests.Count);
            int rand2 = Random.Range(0, mapBlocks.Count);
            Vector2 spawnPoint = mapBlocks[rand2].GetRandomTreasureSpawn();
            if(spawnPoint.x == int.MaxValue)
            {
                attempts++;
                if (attempts >= threshold)
                    break;
                else
                    continue;
            }
            Instantiate(chests[rand].item, spawnPoint, Quaternion.identity);
            spent += chests[rand].cost;

            
        }
        spentBudget += spent;
    }
}

[System.Serializable]
public class BuyableItems
{
    public GameObject item;
    public float cost;
}
