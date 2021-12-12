using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomExit : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject exit;

    private void Start()
    {
        exit.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(boss == null)
        {
            exit.SetActive(true);
            Destroy(this);
        }
    }
}
