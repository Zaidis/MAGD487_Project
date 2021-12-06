using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public bool detected;
    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public Rigidbody2D playerRB;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
            player = other.transform;
            playerRB = other.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detected = false;
            player = null;
            playerRB = null;
        }
    }
}
