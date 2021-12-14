using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static int dungeonLevel;

    public static void NewGame()
    {
        dungeonLevel = 1;
        SaveGame();
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("Dungeon Level");
    }

    public static void LoadGame()
    {
        if(PlayerPrefs.HasKey("Dungeon Level"))
        {
            dungeonLevel = PlayerPrefs.GetInt("Dungeon Level");
            LoadInventory();
        }
        else
        {
            NewGame();
        }
        
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("Dungeon Level", dungeonLevel);
        PlayerPrefs.SetInt("Slot 1", InventoryManager.instance.m_slots[0].m_item.id);
        PlayerPrefs.SetInt("Slot 2", InventoryManager.instance.m_slots[1].m_item.id);
        PlayerPrefs.SetInt("Slot 3", InventoryManager.instance.m_slots[2].m_item.id);
        PlayerPrefs.SetInt("Slot 4", InventoryManager.instance.m_slots[3].m_item.id);
    }

    public static void LoadInventory() {
        InventoryManager.instance.m_slots[0].m_item = ItemDatabase.instance.GetItem(0 + PlayerPrefs.GetInt("Slot 1"));
        InventoryManager.instance.m_slots[1].m_item = ItemDatabase.instance.GetItem(0 + PlayerPrefs.GetInt("Slot 2"));
        InventoryManager.instance.m_slots[2].m_item = ItemDatabase.instance.GetItem(0 + PlayerPrefs.GetInt("Slot 3"));
        InventoryManager.instance.m_slots[3].m_item = ItemDatabase.instance.GetItem(0 + PlayerPrefs.GetInt("Slot 4"));
    }


}
