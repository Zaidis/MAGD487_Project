
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> mapBlocks;
    [SerializeField] int blocksInX, blocksInY;
    [SerializeField] int blockOffset;
    public List<MapBlock> mapBlocksSpawned = new List<MapBlock>();
    [SerializeField] Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //Generate Map
        GenerateMap();

        //Spawn Player
        SpawnPlayer();
    }

    void GenerateMap()
    {
        for (int i = 0; i < blocksInY; i++)
        {
            for (int j = 0; j < blocksInX; j++)
            {
                GameObject block = PickRandomMapBlock();
                GameObject g = Instantiate(block, new Vector3(j * blockOffset, i * -blockOffset) + this.transform.position, Quaternion.identity);
                mapBlocksSpawned.Add(g.GetComponent<MapBlock>());
                
            }
        }
    }

    void SpawnPlayer()
    {
        int rand = Random.Range(0, blocksInX);
        player.position = mapBlocksSpawned[rand].playerSpawns[Random.Range(0, mapBlocksSpawned[rand].playerSpawns.Count)].position;
    }
    private GameObject PickRandomMapBlock()
    {
        return mapBlocks[Random.Range(0, mapBlocks.Count)];
    }
}
