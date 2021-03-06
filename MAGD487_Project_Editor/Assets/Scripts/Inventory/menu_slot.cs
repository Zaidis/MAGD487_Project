using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class menu_slot : MonoBehaviour
{

    public TextMeshProUGUI t_title, t_desc, t_stats;
    public Image i_statsImage, i_icon;
    public menu_slot leftLink, rightLink;
    public void EmptyThisSlot() {

    }

    public void FillSlot(Item item) {
        t_title.text = item.name;
        t_desc.text = item.description;
        i_icon.sprite = item.icon;
        if(item.type == itemType.weapon) {
            Weapon tempItem = (Weapon)item;
            t_stats.text = "Damage: " + tempItem.damage + " - Speed: " + tempItem.attackSpeed;
        } else if(item.type == itemType.consumable) {
            Consumable tempItem = (Consumable)item;
            t_stats.text = "Heal Amount: " + tempItem.healAmount;
        } else {
            t_stats.text = "";
        }
    }
}
