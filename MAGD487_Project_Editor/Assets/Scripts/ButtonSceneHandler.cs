using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneHandler : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        ScenesManager.instance.LoadScene(sceneName);
    }
}
