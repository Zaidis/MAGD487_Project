using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Statistics")]
    public float damage;
    public float speed;

    [Header("Scriptable Object")]
    public Item item;
}
