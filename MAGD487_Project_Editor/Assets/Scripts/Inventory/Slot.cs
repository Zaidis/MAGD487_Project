using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item m_item;
    public Image m_icon;
    public string m_name;
    public int currentStack;
    private void Awake() {
        m_icon = GetComponent<Image>();
    }

    public void AddItemToSlot(Item item) {
        m_item = item;
        m_icon.sprite = m_item.icon;
        m_name = m_item.name;
    }

    public void DeleteItemFromSlot() {
        //m_item = null;
        //m_icon.sprite = null;
        //m_name = "";
        m_item = InventoryManager.instance.menuManager.defaultItem;
        currentStack = 0;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Hovering!");
        if(m_item != null)
            InventoryManager.instance.ActivateTooltip(m_item);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData) {
        Debug.Log("No longer hovering!");
        if (m_item != null)
            InventoryManager.instance.DisableToolTip();
    }
}
