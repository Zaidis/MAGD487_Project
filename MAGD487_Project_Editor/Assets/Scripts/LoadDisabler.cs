using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDisabler : MonoBehaviour
{
    private void Update()
    {
        if (!PlayerPrefs.HasKey("Dungeon Level"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
