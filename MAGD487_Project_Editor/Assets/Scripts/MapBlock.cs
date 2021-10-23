using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    [SerializeField] Transform playerSpawnParent, trapSpawnParent, enemySpawnParent, treasureSpawnParent;
    public List<Transform> playerSpawns, trapSpawns, enemySpawns, treasureSpawns;
    public bool isEndRoom = false;
    private void Awake()
    {
        AddToList(playerSpawnParent, playerSpawns);
        AddToList(trapSpawnParent, trapSpawns);
        AddToList(enemySpawnParent, enemySpawns);
        AddToList(treasureSpawnParent, treasureSpawns);
    }
    
    void AddToList(Transform parent, List<Transform> list)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            list.Add(parent.GetChild(i));
        }
    }
}
