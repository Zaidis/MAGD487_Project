using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapBlock : MonoBehaviour
{
    [SerializeField] Transform playerSpawnParent, trapSpawnParent, enemySpawnParent, treasureSpawnParent, shopKeepSpawnPointParent;
    public Tilemap tilemap;
    public List<Transform> playerSpawns, trapSpawns, enemySpawns, treasureSpawns, shopKeepSpawns;
    private void Awake()
    {
        AddToList(playerSpawnParent, playerSpawns);
        AddToList(trapSpawnParent, trapSpawns);
        AddToList(enemySpawnParent, enemySpawns);
        AddToList(treasureSpawnParent, treasureSpawns);
        AddToList(shopKeepSpawnPointParent, shopKeepSpawns);
    }
    
    void AddToList(Transform parent, List<Transform> list)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            list.Add(parent.GetChild(i));
        }
    }

    public Vector2 GetRandomEnemySpawn()
    {
        int rand = Random.Range(0, enemySpawns.Count);
        return enemySpawns[rand].position;
    }

    public Vector2 GetRandomTreasureSpawn()
    {
        int rand = Random.Range(0, treasureSpawns.Count);
        return treasureSpawns[rand].position;
    }
}
