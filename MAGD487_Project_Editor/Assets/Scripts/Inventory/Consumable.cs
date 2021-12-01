using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Health Potion", menuName = "New Consumable")]
public class Consumable : Item
{
    [Header("Statistics")]
    public int healAmount;

}
