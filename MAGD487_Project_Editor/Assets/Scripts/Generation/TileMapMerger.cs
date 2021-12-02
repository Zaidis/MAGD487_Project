using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileMapMerger : MonoBehaviour
{
    [SerializeField] Tilemap mainTiles;
    
    public void MergeTilemaps(List<Tilemap> tilemapsToMerge)
    {
        //Merge the tilemaps
        //Loop through each individial tile map
        //Loop through each individual tile in each map and add it into the main one
        foreach(Tilemap tilemap in tilemapsToMerge)
        {
            BoundsInt bounds = tilemap.cellBounds;
            BoundsInt.PositionEnumerator iterator = bounds.allPositionsWithin;
            Vector3Int offset = new Vector3Int((int)tilemap.transform.position.x, (int)tilemap.transform.position.y, 0);
            while (iterator.MoveNext())
            {
                RuleTile tile = tilemap.GetTile<RuleTile>(iterator.Current);
                if(tile != null)
                {
                    mainTiles.SetTile(iterator.Current + offset, tile);
                    //tilemap.SetTile(iterator.Current, null);
                }
            }
            tilemap.gameObject.SetActive(false);
        }
    }
}
