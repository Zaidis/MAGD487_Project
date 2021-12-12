using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    static Animator anim;
    public static SceneTransitioner instance;
    bool transitioning = false;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        anim = GetComponent<Animator>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void Transition()
    {
        if (!transitioning)
        {
            transitioning = true;
            anim.SetTrigger("Transition");
            Invoke("LoadTheScene", 2);
        }
    }

    public void LoadTheScene()
    {
        transitioning = false;
        ScenesManager.instance.LoadScene();
    }
}
