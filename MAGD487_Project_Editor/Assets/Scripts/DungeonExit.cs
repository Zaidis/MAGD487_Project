using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    bool exiting = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !exiting)
        {
            exiting = true;
            StateController.dungeonLevel += 1;
            StateController.SaveGame();
            ScenesManager.instance.LoadScene("Camp");
            this.enabled = false;
        }
    }
}
