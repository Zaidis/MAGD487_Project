using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampSpawn : MonoBehaviour
{

    private void Start() {
        FindPlayer();
    }

    private void FindPlayer() {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        player.transform.position = this.transform.position;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

        Destroy(this.gameObject);
    }

}
