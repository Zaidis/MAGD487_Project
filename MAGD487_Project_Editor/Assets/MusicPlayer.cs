using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip normal;
    public AudioClip bossComing;

    private AudioSource source;
    private void Awake() {
        source = GetComponent<AudioSource>();
    }
    private void Start() {
        if((StateController.dungeonLevel - 1) % 4 == 0) {
            //i think this is how it works? should take the dungeon level subtracted by one. so if the level is 5, that would make it 4? maybe? im tired

            //boss room coming up
            source.clip = bossComing;
        } else {
            source.clip = normal;
        }

        source.Play();
    }


}
