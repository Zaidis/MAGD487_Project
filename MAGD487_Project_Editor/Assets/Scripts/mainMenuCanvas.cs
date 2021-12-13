using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuCanvas : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public void EnableCanvas() {
        canvas.SetActive(true);
        player.SetActive(true);
    }

}
