using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sellButton : MonoBehaviour
{

    [SerializeField] private shopItemUI slot;
    public void SellItem() {
        Item item = slot.myItem;
        if (item.id != 0) {
            float goldAmount = item.cost;
            GetComponent<AudioSource>().Play();
            InventoryManager.instance.RemoveSpecificItem(item);
            StatisticsManager.instance.AddGoldAmount((int)goldAmount);
            shopMenu.instance.UpdateGoldUI();
            shopMenu.instance.UpdateSellList();
        }
    }
}
