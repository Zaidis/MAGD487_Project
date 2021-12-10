using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sectionButton : MonoBehaviour
{
    public GameObject bar;
    public void Selected() {
        bar.SetActive(true);
    }

    public void Deactivated() {
        bar.SetActive(false);
    }

}
