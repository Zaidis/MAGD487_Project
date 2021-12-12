using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapBlock : MonoBehaviour
{
    [SerializeField] Transform playerSpawnParent, trapSpawnParent, enemySpawnParent, treasureSpawnParent, shopKeepSpawnPointParent, exitSpawnParent;
    public Tilemap tilemap;
    public List<Transform> playerSpawns, trapSpawns, enemySpawns, treasureSpawns, shopKeepSpawns, exitSpawns;
    private void Awake()
    {
        AddToList(playerSpawnParent, playerSpawns);
        AddToList(trapSpawnParent, trapSpawns);
        AddToList(enemySpawnParent, enemySpawns);
        AddToList(treasureSpawnParent, treasureSpawns);
        AddToList(shopKeepSpawnPointParent, shopKeepSpawns);
        AddToList(exitSpawnParent, exitSpawns);
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
        if (treasureSpawns.Count <= 0)
            return new Vector2(int.MaxValue, int.MaxValue);

        int rand = Random.Range(0, treasureSpawns.Count);
        Vector2 val = treasureSpawns[rand].position;
        treasureSpawns.RemoveAt(rand);
        return val;
    }
}
