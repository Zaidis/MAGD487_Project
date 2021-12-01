using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public static int dungeonLevel;
    
    public void NewGame()
    {
        dungeonLevel = 1;
        SaveGame();
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("Dungeon Level");
    }

    public void LoadGame()
    {
        dungeonLevel = PlayerPrefs.GetInt("Dungeon Level");
    }

    public static void SaveGame()
    {
        PlayerPrefs.SetInt("Dungeon Level", dungeonLevel);
    }
}
