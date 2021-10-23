using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    [SerializeField] private List<Slot> m_slots = new List<Slot>(); //4 different slots for items
    [SerializeField] private float m_currentItem; //what item the player is using on the UI
    [SerializeField] private int m_goldAmount { get; set; } //the amount of gold the player has
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
    }

    /// <summary>
    /// If there is an empty slot, then add an item to that specific slot. 
    /// </summary>
    /// <param name="item"></param>

    public void AddItem(Item item) {
        for(int i = 0; i < m_slots.Count; i++) {
            if(m_slots[i].m_item == null) {
                //if this slot does not have an item in it yet
                m_slots[i].AddItemToSlot(item);
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
        m_slots[(int)m_currentItem].DeleteItemFromSlot();
        UpdateSlotUI();
    }

    public void SetCurrentItem(InputAction.CallbackContext context) {
        if (context.performed) {
            m_currentItem += context.ReadValue<float>();
            ValidateValues();
        }
    }

    public void ScrollThroughSlots(InputAction.CallbackContext context) {
        if (context.performed) {
            m_currentItem += context.ReadValue<Vector2>().normalized.y;
            ValidateValues();
        }
        
    }

    public void UseItem(InputAction.CallbackContext context) {
        if(context.performed) {
            Debug.Log("I used " + m_slots[(int)m_currentItem].m_name);
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
