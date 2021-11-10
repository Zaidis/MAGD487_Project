using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{

    
    public GameObject inventory, statistics, options;
    [Header("Inventory Section")]
    public menu_slot[] slots;
    public menu_slot currentSlot;
    /// <summary>
    /// Allows the player to shuffle through the menu slots for the inventory.
    /// </summary>
    /// <param name="context"></param>
    public void SelectItem(InputAction.CallbackContext context) {
        if (context.performed) {
            Vector2 ctx = context.ReadValue<Vector2>();
            //Debug.Log("I used " + ctx);

            if(currentSlot == slots[0]) {

                //right/down
                if(ctx == new Vector2(1, 0)) {
                    //move right
                    currentSlot = slots[1];
                } else if(ctx == new Vector2(0, -1)) {
                    currentSlot = slots[2];
                }

            } else if (currentSlot == slots[1]) {

                if (ctx == new Vector2(-1, 0)) {
                    //move right
                    currentSlot = slots[0];
                }
                else if (ctx == new Vector2(0, -1)) {
                    currentSlot = slots[3];
                }

            } else if (currentSlot == slots[2]) {

                if (ctx == new Vector2(1, 0)) {
                    //move right
                    currentSlot = slots[3];
                }
                else if (ctx == new Vector2(0, 1)) {
                    currentSlot = slots[0];
                }

            } else {

                if (ctx == new Vector2(-1, 0)) {
                    //move right
                    currentSlot = slots[2];
                }
                else if (ctx == new Vector2(0, 1)) {
                    currentSlot = slots[1];
                }
            }
            HoverOverCurrentSlot();
        }
        
    }

    public void UpdateInventoryMenuUI() {
        Debug.Log("I am updating the menu UI");
        for(int i = 0; i < 4; i++) { //4 slots
            if(InventoryManager.instance.m_slots[i].m_item != null)
                slots[i].FillSlot(InventoryManager.instance.m_slots[i].m_item);
        }
    }

    public void HoverOverCurrentSlot() {
        foreach(menu_slot slot in slots) {
            slot.GetComponent<Image>().color = Color.white;
        }
        currentSlot.GetComponent<Image>().color = Color.green;
    }
}
