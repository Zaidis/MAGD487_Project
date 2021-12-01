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
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private bool isPotionButton;
    private void Start() {
        if(!isPotionButton)
            UpdateIcon(myItem);
    }
    public void UpdateIcon(Item item) {
        myItem = item;
        cost.text = item.cost.ToString();
        itemName.text = myItem.name;
        itemDescription.text = myItem.description;
        itemLevel.text = "Level " + myItem.level.ToString();
        icon.sprite = myItem.icon;
    }
}
