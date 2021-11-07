using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWaterParticle : MonoBehaviour
{
    [SerializeField] GameObject waterParticle;
    [SerializeField] int amountOfParticles;
    [SerializeField] float speedToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfParticles; i++)
        {
            Invoke("SpawnParticles", speedToSpawn * i);
        }
    }

    void SpawnParticles()
    {
        Instantiate(waterParticle, this.transform.position, Quaternion.identity);
    }
}
