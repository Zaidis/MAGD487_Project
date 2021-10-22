using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> mapBlocks;
    [SerializeField] int blocksInX, blocksInY;
    [SerializeField] int blockOffset;
    // Start is called before the first frame update
    void Start()
    {
        //Generate Map
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int i = 0; i < blocksInX; i++)
        {
            for (int j = 0; j < blocksInY; j++)
            {
                GameObject block = PickRandomMapBlock();
                Instantiate(block, new Vector3(i * blockOffset, j * blockOffset) + this.transform.position, Quaternion.identity);
            }
        }
    }
    private GameObject PickRandomMapBlock()
    {
        return mapBlocks[Random.Range(0, mapBlocks.Count)];
    }
}
