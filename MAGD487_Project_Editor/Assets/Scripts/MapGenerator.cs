
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> mapBlocksMidSection, mapBlocksUpDown;
    [SerializeField] int blocksInX, blocksInY;
    [SerializeField] int blockOffset;
    public List<MapBlock> mapBlocksSpawned = new List<MapBlock>();
    [SerializeField] Transform player;
    [SerializeField] GameObject mapBlockTemplate;
    // Start is called before the first frame update
    void Start()
    {
        //Generate Map
        GenerateMap();
        //Place in Up Down Blocks
        PlaceUpDownBlocks();
        //Spawn Player
        SpawnPlayer();
    }

    void GenerateMap()
    {
        for (int i = 0; i < blocksInY; i++)
        {
            for (int j = 0; j < blocksInX; j++)
            {
                GameObject block, g;
                MapBlock mapBlock;
                bool end = false;
                if (i == 0 || j == 0 || j == blocksInX - 1 || i == blocksInY - 1)
                {
                    block = mapBlockTemplate;
                    end = true;
                }
                else
                {
                    block = PickRandomMidSectionMapBlock();
                }
                g = Instantiate(block, new Vector3(j * blockOffset, i * -blockOffset) + this.transform.position, Quaternion.identity);
                if (!end)
                {
                    mapBlock = g.GetComponent<MapBlock>();
                    mapBlocksSpawned.Add(mapBlock);
                }
            }
        }
    }

    void PlaceUpDownBlocks()
    {
        for (int i = 0; i < mapBlocksSpawned.Count - blocksInX; i += blocksInX-3)
        {
            int rand = Random.Range(i, i + blocksInX-3);
            GameObject block = PickRandomUpDownBlock();
            GameObject g = Instantiate(block, mapBlocksSpawned[rand].transform.position, Quaternion.identity);
            Destroy(mapBlocksSpawned[rand].gameObject);
            mapBlocksSpawned.RemoveAt(rand);
            mapBlocksSpawned.Add(g.GetComponent<MapBlock>());
        }
    }

    void SpawnPlayer()
    {
        int rand = Random.Range(0, blocksInX - 1);
        //Picks a random room at the top level of the dungeon to spawn the player
        player.position = mapBlocksSpawned[rand].playerSpawns[Random.Range(0, mapBlocksSpawned[rand].playerSpawns.Count)].position;
    }
    private GameObject PickRandomMidSectionMapBlock()
    {
        return mapBlocksMidSection[Random.Range(0, mapBlocksMidSection.Count)];
    }
    private GameObject PickRandomUpDownBlock()
    {
        return mapBlocksUpDown[Random.Range(0, mapBlocksUpDown.Count)];
    }
}
