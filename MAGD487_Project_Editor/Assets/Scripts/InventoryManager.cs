using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    public Text pickUpText;
    [SerializeField] private List<Slot> m_slots = new List<Slot>(); //4 different slots for items
    [SerializeField] private float m_currentItem; //what item the player is using on the UI
    [SerializeField] private int m_goldAmount { get; set; } //the amount of gold the player has
    [SerializeField] private GameObject defaultInteractable;
    [SerializeField] private Tooltip tooltip;
    
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        m_currentItem = 0;
        UpdateSlotUI();

       // Physics2D.IgnoreLayerCollision(8, 7);
    }

    public void ActivateTooltip(Item item) {
        tooltip.gameObject.SetActive(true);
        tooltip.SetText(item.name);
    }

    public void DisableToolTip() {
        tooltip.gameObject.SetActive(false);
    }

    /// <summary>
    /// If there is an empty slot, then add an item to that specific slot.  
    /// </summary>
    /// <param name="item"></param>

    public void AddItem(Item item) {
        if (item.stackable) { //first it will check if the item is stackable or not
            for (int i = 0; i < m_slots.Count; i++) {
                if(m_slots[i].m_item != null) { //if there is an item here
                    if(m_slots[i].m_item == item) { //if this item is the same as the one you are adding
                        //you have the item
                        if(m_slots[i].currentStack < m_slots[i].m_item.maxStack) {
                            //you have the item and you can carry more of that item in this slot
                            m_slots[i].currentStack++;
                            UpdateSlotUI();
                            return;
                        }
                    }
                }
            }                       
        }
        //if its not stackable or you have too many, just add to another slot
        for(int i = 0; i < m_slots.Count; i++) {
            if(m_slots[i].m_item == null) {
                //if this slot does not have an item in it yet
                m_slots[i].AddItemToSlot(item);
                m_slots[i].currentStack = 1;
                UpdateSlotUI();
                return; 
            } else {
                continue;
            }
        }
    }

    /// <summary>
    /// Removes the item from the selected slot.
    /// </summary>
    public void RemoveItem() {
        Slot slot = m_slots[(int)m_currentItem];
        slot.DeleteItemFromSlot();
        UpdateSlotUI();
    }

    public void DropItemContext(InputAction.CallbackContext context) {
        if (context.performed) {
            DropItem();
        }
    }
    private void DropItem() {
        Slot slot = m_slots[(int)m_currentItem];
        if(slot.m_item != null) {
            GameObject droppedItem = Instantiate(defaultInteractable, FindObjectOfType<PlayerMovement>().gameObject.transform.position, Quaternion.identity);
            droppedItem.transform.GetChild(0).GetComponent<Interactable>().item = slot.m_item;

            RemoveItem();
        }
    }

    

    /// <summary>
    /// Allows the user to shuffle through the slots with either mouse or keyboard.
    /// </summary>
    /// <param name="context"></param>
    public void SetCurrentItem(InputAction.CallbackContext context) {
        if (context.performed) {
            m_currentItem += context.ReadValue<float>();
            ValidateValues();
        }
    }
    /// <summary>
    /// Allows the user to scroll through the slots with their mouse. 
    /// </summary>
    /// <param name="context"></param>
    public void ScrollThroughSlots(InputAction.CallbackContext context) {
        if (context.performed) {
            m_currentItem += context.ReadValue<Vector2>().normalized.y;
            ValidateValues();
        }
        
    }
    /// <summary>
    /// Selects a slot rather than having to shuffle through them one by one. 
    /// </summary>
    /// <param name="context"></param>

    public void SelectItem(InputAction.CallbackContext context) {
        if (context.performed) {
            Vector2 ctx = context.ReadValue<Vector2>();
            Debug.Log("I used " + ctx);


            if (ctx == new Vector2(-1, 0)) { //left 
                m_currentItem = 0;
            }
            else if (ctx == new Vector2(1, 0)) { //right
                m_currentItem = 2;
            }
            else if (ctx == new Vector2(0, -1)) { //down
                m_currentItem = 3;
            }
            else { //up
                m_currentItem = 1;
            }

            ValidateValues();
        }
    }

    public void UseItemContext(InputAction.CallbackContext context) {
        if (context.performed) {
            UseItem();
        }
    }

    private void UseItem() {
        Debug.Log("I used " + m_slots[(int)m_currentItem].m_name);
        Slot slot = m_slots[(int)m_currentItem];

        //first we check to see if the item is a consumable
        if (slot.m_item.type == itemType.consumable) {
            print("Test");
            slot.currentStack--;
            if(slot.currentStack <= 0) {
                RemoveItem();
            }
        }
    }

    /// <summary>
    /// Checks all slots to see if a particular item is there. 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CheckForItem(Item item) {
        for(int i = 0; i < m_slots.Count; i++) {
            if(m_slots[i].m_item == item) {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Ensures that the current slot that is viewed does not go past the list limit. 
    /// </summary>
    private void ValidateValues() {
        if(m_currentItem < 0) {
            m_currentItem = m_slots.Count - 1;
        } else if (m_currentItem > m_slots.Count - 1) {
            m_currentItem = 0;
        }
        UpdateSlotUI();
    }

    /// <summary>
    /// Updates the UI. Showcases which slot is currently viewed. 
    /// </summary>
    private void UpdateSlotUI() {
        foreach(Slot slot in m_slots) {
            slot.GetComponent<Image>().color = Color.white;
        }
        m_slots[(int)m_currentItem].GetComponent<Image>().color = Color.yellow;
    }

    public float GetCurrentItem() {
        return m_currentItem;
    }
}
