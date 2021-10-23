using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class Item : ScriptableObject
{

    public Sprite icon;
    public string name;
    public bool stackable;
    public int maxStack = 1;

    public itemType type = itemType.weapon;
    //statistics will come later

}
