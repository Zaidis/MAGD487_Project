using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    [Header("Item ID")]
    public int id;
    [TextArea()]
    new public string name;
    [TextArea()]
    public string description;
    [Header("Item Level")]
    [Range(1, 15)]
    public int level = 1;
    [Header("Item Cost")]
    public int cost;
    public bool stackable;
    public int maxStack = 1;
    public Sprite icon;
    public itemType type = itemType.weapon;
    public AudioClip clip;
    
    //statistics will come later
}
