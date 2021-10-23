using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMaster : MonoBehaviour
{
    [SerializeField] List<EnemyCatalogue> enemies;
    [SerializeField] float budget = 1000;
    [SerializeField] float percentageOfEnemyBudget = 1;
    // Start is called before the first frame update
    void Start()
    {
        //First Spawn Enemies

    }

    void SpawnEnemies()
    {

    }
}

[System.Serializable]
public class EnemyCatalogue
{
    public GameObject enemy;
    public float cost;
}
