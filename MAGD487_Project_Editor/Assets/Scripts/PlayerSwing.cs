using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwing : MonoBehaviour
{

    //MAKES SOUND
    public AudioClip clip;
    public void SoundEffect() {
        clip = InventoryManager.instance.m_slots[(int)InventoryManager.instance.GetCurrentItem()].m_item.clip;
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }

}
