using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStart : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindObjectOfType<CanvasEnabler>().EnableStuff();
    }
}
