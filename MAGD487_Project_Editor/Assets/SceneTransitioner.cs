using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    static Animator anim;
    static SceneTransitioner instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }

    public static void Transition()
    {
        anim.SetTrigger("Transition");
    }

    public void LoadTheScene()
    {
        ScenesManager.instance.LoadScene();
    }
}
