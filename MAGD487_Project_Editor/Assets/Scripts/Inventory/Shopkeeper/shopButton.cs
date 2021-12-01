using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopButton : MonoBehaviour
{
    [SerializeField] private shopItemUI slot;
    public void PurchaseItem() {
        Item item = slot.myItem;
        int goldAmount = item.cost;
        if(goldAmount <= InventoryManager.instance.m_goldAmount) { //checks that you have enough to purchase the item
            //you have enough money
            if (InventoryManager.instance.CheckIfOpenSlot()) {
                //you have an open slot
                InventoryManager.instance.AddItem(item);
                InventoryManager.instance.m_goldAmount -= goldAmount;
            }
        }
    }

}
