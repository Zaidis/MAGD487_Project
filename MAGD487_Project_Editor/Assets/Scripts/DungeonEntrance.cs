using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] int bossTick = 5;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            print("Dungeon level: " + StateController.dungeonLevel + " | " + StateController.dungeonLevel % bossTick);
            if(StateController.dungeonLevel % bossTick == 0)
                ScenesManager.instance.LoadScene("Boss Room");
            else
                ScenesManager.instance.LoadScene("Dungeon");
            this.enabled = false;
        }
    }
}
