using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    [SerializeField] Transform playerSpawnParent, trapSpawnParent, enemySpawnParent, treasureSpawnParent;
    public List<Transform> playerSpawns, trapSpawns, enemySpawns, treasureSpawns;

    private void Awake()
    {
        AddToList(playerSpawnParent, playerSpawns);
        AddToList(trapSpawnParent, trapSpawns);
        AddToList(enemySpawnParent, enemySpawns);
        AddToList(treasureSpawnParent, treasureSpawns);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void AddToList(Transform parent, List<Transform> list)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            list.Add(parent.GetChild(i));
        }
    }
}
