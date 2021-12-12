using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> mapBlocksMidSection, mapBlocksDown, mapBlocksUp;
    [Header("Minimum X = 7")]
    [SerializeField] int blocksInX = 7;
    [Header("Minimum Y = 3")]
    [SerializeField] int blocksInY = 3;
    [SerializeField] int maxBlocksInY = 7;
    [SerializeField] int blockOffset;
    [SerializeField] bool testingSizes;
    public static List<MapBlock> mapBlocksSpawned = new List<MapBlock>();
    List<MapBlock> templateMapBlocks = new List<MapBlock>();
    [SerializeField] Transform player;
    [SerializeField] GameObject exitPrefab, mapBlockTemplate, shopKeeperPrefab;
    [SerializeField] int dungeonLevelRatio = 2;
    TileMapMerger mapMerger;
    private void Awake()
    {
        mapMerger = GetComponent<TileMapMerger>();
        mapBlocksSpawned.Clear();
        if (testingSizes)
        {
            //Generate Map
            GenerateMap();
            //Place in Up Down Blocks
            PlaceUpDownBlocks();
            //Merge Tilemaps
            MergeTiles();
            /*
            if(blocksInY > 3)
            {
                //Spawn ShopKeeper
                SpawnShopKeeper();
            }*/
            //Spawn Player
            SpawnPlayer();
            //Spawn Dungeon Exit
            SpawnExit();
        }
        else
        {
            blocksInY = (StateController.dungeonLevel / dungeonLevelRatio) + 3;
            if (blocksInY > maxBlocksInY)
                blocksInY = maxBlocksInY;
            //Generate Map
            GenerateMap();
            //Place in Up Down Blocks
            PlaceUpDownBlocks();
            //Merge Tilemaps
            MergeTiles();
            /*
            if (blocksInY > 3)
            {
                //Spawn ShopKeeper
                SpawnShopKeeper();
            }*/
            //Spawn Player
            SpawnPlayer();
            //Spawn Dungeon Exit
            SpawnExit();
        }
        
    }

    void MergeTiles()
    {
        //One optiization is to create "End blocks" that spawn on the borders of the map. 
        List<Tilemap> tilemaps = new List<Tilemap>();
        for (int i = 0; i < mapBlocksSpawned.Count; i++)
        {
            tilemaps.Add(mapBlocksSpawned[i].tilemap);
        }
        for (int i = 0; i < templateMapBlocks.Count; i++)
        {
            tilemaps.Add(templateMapBlocks[i].tilemap);
        }
        mapMerger.MergeTilemaps(tilemaps);
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
                }else
                {
                    mapBlock = g.GetComponent<MapBlock>();
                    templateMapBlocks.Add(mapBlock);
                }
            }
        }
    }
    //To go the block below the upDown block, BlocksInX-2
    void PlaceUpDownBlocks()
    {
        List<int> indexs = new List<int>();
        int previousIndex = -1;
        for (int i = 0; i < mapBlocksSpawned.Count - blocksInX-2; i += blocksInX-2)
        {
            int rand = PickRandomIndexExcludingOne(i, i + blocksInX-2, previousIndex);
            indexs.Add(rand);
            previousIndex = rand + blocksInX - 2;
            GameObject block = PickRandomDownBlock();
            GameObject g = Instantiate(block, mapBlocksSpawned[rand].transform.position, Quaternion.identity);
            Destroy(mapBlocksSpawned[rand].gameObject);
            mapBlocksSpawned[rand] = g.GetComponent<MapBlock>();
        }
        for (int i = 0; i < indexs.Count; i++)
        {
            Destroy(mapBlocksSpawned[indexs[i] + blocksInX - 2].gameObject);
            GameObject block = PickRandomUpBlock();
            GameObject g = Instantiate(block, mapBlocksSpawned[indexs[i] + blocksInX - 2].transform.position, Quaternion.identity);
            mapBlocksSpawned[indexs[i] + blocksInX - 2] = g.GetComponent<MapBlock>();
        }
        
    }
    private int PickRandomIndexExcludingOne(int leftBound, int RightBound, int leaveOut)
    {
        int rand = Random.Range(leftBound, RightBound);
        if (rand == leaveOut)
            return PickRandomIndexExcludingOne(leftBound, RightBound, leaveOut);
        else
            return rand;
    }
    void SpawnShopKeeper()
    {
        int rand = Random.Range(blocksInX-2, mapBlocksSpawned.Count);
        //Picks a random room of the dungeon to spawn the shopkeep excluding the first level
        Instantiate(shopKeeperPrefab, mapBlocksSpawned[rand].shopKeepSpawns[Random.Range(0, mapBlocksSpawned[rand].shopKeepSpawns.Count)].position, Quaternion.identity);
    }
    void SpawnPlayer()
    {
        int rand = Random.Range(0, blocksInX - 2);
        int rand2 = Random.Range(0, mapBlocksSpawned[rand].playerSpawns.Count);
        //Picks a random room at the top level of the dungeon to spawn the player
        player = FindObjectOfType<PlayerMovement>().transform;
        player.position = mapBlocksSpawned[rand].playerSpawns[rand2].position;
    }
    void SpawnExit()
    {
        int rand = Random.Range(mapBlocksSpawned.Count - blocksInX + 2, mapBlocksSpawned.Count);
        //Picks a random room at the top level of the dungeon to spawn the player
        Instantiate(exitPrefab, mapBlocksSpawned[rand].exitSpawns[Random.Range(0, mapBlocksSpawned[rand].exitSpawns.Count)].position, Quaternion.identity);
    }

    private GameObject PickRandomMidSectionMapBlock()
    {
        return mapBlocksMidSection[Random.Range(0, mapBlocksMidSection.Count)];
    }
    private GameObject PickRandomDownBlock()
    {
        return mapBlocksDown[Random.Range(0, mapBlocksDown.Count)];
    }
    private GameObject PickRandomUpBlock()
    {
        return mapBlocksUp[Random.Range(0, mapBlocksUp.Count)];
    }
}
