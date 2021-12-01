using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StateController.dungeonLevel += 1;
            StateController.SaveGame();
            ScenesManager.instance.LoadScene("Camp");
        }
    }
}
