using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class shopItemUI : MonoBehaviour
{

    public Item myItem;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemLevel;
    [SerializeField] private TextMeshProUGUI itemStats;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private bool isPotionButton;
    [SerializeField] private bool isSellButton;
    private void Start() {
        if (!isPotionButton) {
            if (isSellButton) {
                UpdateSellSlot(myItem);
            } else {
                UpdateIcon(myItem);
            }
        }
            
    }
    public void UpdateIcon(Item item) {
        myItem = item;
        cost.text = item.cost.ToString();
        itemName.text = myItem.name;
        itemDescription.text = myItem.description;
        itemLevel.text = "Level " + myItem.level.ToString();

        if (item.type == itemType.weapon) {
            Weapon tempItem = (Weapon)item;
            itemStats.text = "Damage: " + tempItem.damage + " - Speed: " + tempItem.attackSpeed;
        }
        else if (item.type == itemType.consumable) {
            Consumable tempItem = (Consumable)item;
            itemStats.text = "Heal Amount: " + tempItem.healAmount;
        }
        else {
            itemStats.text = "";
        }

        icon.sprite = myItem.icon;
    }


    public void UpdateSellSlot(Item item) {
        myItem = item;
        cost.text = item.cost.ToString();
        itemName.text = myItem.name;
        itemLevel.text = "Level " + myItem.level.ToString();
        icon.sprite = myItem.icon;
    }

}
