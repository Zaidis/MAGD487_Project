using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StateController.LoadGame();
        ScenesManager.instance.LoadScene(ScenesManager.fromMenuLoad);
    }

}
