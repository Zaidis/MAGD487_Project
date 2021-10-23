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
        if (context.performed) {
            Debug.Log("I used " + context.ReadValue<float>());
        }
    }

    private void ValidateValues() {
        if(m_currentItem < 0) {
            m_currentItem = m_slots.Count - 1;
        } else if (m_currentItem > m_slots.Count - 1) {
            m_currentItem = 0;
        }
        UpdateSlotUI();
    }

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
