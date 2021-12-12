using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class shopButton : MonoBehaviour
{
    [SerializeField] private shopItemUI slot;
    private int goldAmount;
    public bool purchasable;
    #region COLORS
    //ENABLED COLORS ---> Default
    private Color32 normalColor = new Color32(7, 118, 0, 255);
    private Color32 pressedColor =  new Color32(135, 236, 114, 255);
    private Color32 selectedColor = new Color32(89, 250, 74, 255);
    private ColorBlock normalBlock;
    //DEACTIVATED COLORS ---> Cannot purchase from
    private Color32 d_normalColor = new Color32(154, 18, 0, 255);
    private Color32 d_pressedColor = new Color32(236, 250, 74, 255);
    private Color32 d_selectedColor = new Color32(250, 94, 74, 255);
    private ColorBlock d_block;
    
    private void Start() {
        normalBlock.normalColor = normalColor;
        normalBlock.pressedColor = pressedColor;
        normalBlock.selectedColor = selectedColor;

        d_block.normalColor = d_normalColor;
        d_block.pressedColor = d_pressedColor;
        d_block.selectedColor = d_selectedColor;
    }
    #endregion
    //Attempts to purchase item
    public void PurchaseItem() {
        if (purchasable) {
            Item item = slot.myItem;
            goldAmount = item.cost;
            if (CheckIfPurchasable()) {  //checks that you have enough to purchase the item
                                         //you have enough money
                                         //you have an open slot. you purchase the item
                this.GetComponent<AudioSource>().Play(); // buy sound
                InventoryManager.instance.AddItem(item);
                StatisticsManager.instance.AddGoldAmount((goldAmount * -1));
                shopMenu.instance.UpdateGoldUI();
            }
        }
    }
    
    public bool CheckIfPurchasable() {
        if(goldAmount <= StatisticsManager.instance.GetGoldAmount()) {
            if (CheckIfRoomIsAvailable()) { //checks if there is an open slot
                this.GetComponent<Button>().colors = normalBlock;
                purchasable = true;
                return true;
            }
        }
        DeactivateButton();
        return false;
    }

    public bool CheckIfRoomIsAvailable() {
        if (InventoryManager.instance.CheckIfOpenSlot()) {
            return true;
        }
        return false;
    }

    public void DeactivateButton() {
        //turning off interactibility is not a good thing, so this is what needs to be done
        purchasable = false;
        this.GetComponent<Button>().colors = d_block;
    }
}
