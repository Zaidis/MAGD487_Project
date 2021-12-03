using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellButton : MonoBehaviour
{

    [SerializeField] private shopItemUI slot;
    public void SellItem() {
        Item item = slot.myItem;
        float goldAmount = item.cost;

        InventoryManager.instance.RemoveSpecificItem(item);
        StatisticsManager.instance.AddGoldAmount((int)goldAmount);
        shopMenu.instance.UpdateGoldUI();
        shopMenu.instance.UpdateSellList();

    }
}
