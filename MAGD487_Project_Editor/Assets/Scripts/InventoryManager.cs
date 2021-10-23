using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    [SerializeField] private List<Slot> m_slots = new List<Slot>();
    [SerializeField] private float m_currentItem; //what item the player is using on the UI
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


    public void SetCurrentItem(InputAction.CallbackContext context) {
        if (context.performed) {
            m_currentItem += context.ReadValue<float>();
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
