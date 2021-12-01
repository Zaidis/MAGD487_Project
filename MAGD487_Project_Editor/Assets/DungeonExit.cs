using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StateController.dungeonLevel++;
            StateController.SaveGame();
            print(StateController.dungeonLevel);
            ScenesManager.instance.LoadScene("Camp");
        }
    }
}
