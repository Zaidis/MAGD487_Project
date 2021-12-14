using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEnabler : MonoBehaviour
{
    public GameObject[] objects;
    public void EnableStuff()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
        }
    }
}
