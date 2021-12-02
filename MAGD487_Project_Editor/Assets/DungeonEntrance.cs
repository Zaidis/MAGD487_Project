using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ScenesManager.instance.LoadScene("Dungeon");
        }
    }
}
