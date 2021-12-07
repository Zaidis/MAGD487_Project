using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] List<EnemyCatalogue> enemies;
    [SerializeField] float budget = 1000, spentBudget = 0;
    [SerializeField] float percentageOfEnemyBudget = 1;
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
    }

    void SpawnEnemies()
    {
        while(spentBudget < budget * percentageOfEnemyBudget)
        {
            int rand = Random.Range(0, enemies.Count);
            int rand2 = Random.Range(0, mapBlocks.Count);
            Instantiate(enemies[rand].enemy, mapBlocks[rand2].GetRandomEnemySpawn(), Quaternion.identity);
            spentBudget += enemies[rand].cost;
        }
    }
}

[System.Serializable]
public class EnemyCatalogue
{
    public GameObject enemy;
    public float cost;
}
