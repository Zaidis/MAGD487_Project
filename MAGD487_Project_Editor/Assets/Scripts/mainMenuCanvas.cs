using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuCanvas : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    AudioListener listener;

    private void Awake() {
        listener = GetComponent<AudioListener>();
    }

    public void EnableCanvas() {
        Destroy(listener);
        //canvas.SetActive(true);
        player.SetActive(true);
    }

}
