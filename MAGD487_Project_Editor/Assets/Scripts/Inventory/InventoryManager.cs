using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;
    public PlayerAnimationController PAC;
    public MenuManager menuManager;
    public PlayerMovement player;
    public Text pickUpText;
    public List<Slot> m_slots = new List<Slot>(); //4 different slots for items
    //public int m_goldAmount { get; set; } //the amount of gold the player has
    public GameObject defaultInteractable;



    [SerializeField] private float m_currentItem; //what item the player is using on the UI
    
    [SerializeField] private Tooltip tooltip;

    [Header("Inventory Menu")]
    [SerializeField] private GameObject menu;

    private bool inventoryOn;

    /*
     * DEVELOPER NOTES
     * An empty item == menuManager.defaultItem;
     * Do not check the inventory if a slot item == null, use default item instead.
     */

    
    private void Awake() { //singleton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

        menuManager = FindObjectOfType<MenuManager>();
        
    }

    private void Start() {
        player = FindObjectOfType<PlayerMovement>();
        m_currentItem = 0;
        
        PAC = GameObject.Find("Graphic").GetComponent<PlayerAnimationController>();
        InitialWeapon();
        UpdateSlotUI();
        // Physics2D.IgnoreLayerCollision(8, 7);
    }

    /// <summary>
    /// Rusty Dagger
    /// </summary>
    public void InitialWeapon() {
        m_slots[0].AddItemToSlot(m_slots[0].m_item);
    }

    public void ManageInventoryContext(InputAction.CallbackContext context) {
        if (context.performed) {
            ManageInventory();
        }
    }

    /// <summary>
    /// Turns on/off the inventory screen.
    /// </summary>
    public void ManageInventory() {
        menuManager.SectionButtonBarSetActive();
        if (!inventoryOn) {
            //turn on the inventory menu
            TurnMenuOn();
        } else {
            TurnMenuOff();
        }
    }

    private void TurnMenuOn() {
        menuManager.UpdateStatisticsSection();
        inventoryOn = true;
        menu.SetActive(true);
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("Menu");
    }

    private void TurnMenuOff() {
        menuManager.UpdateStatisticsSection();
        inventoryOn = false;
        menu.SetActive(false);
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("Player");
    }
    /// <summary>
    /// Called when enabling the tooltip. 
    /// </summary>
    /// <param name="item"></param>
    public void ActivateTooltip(Item item) {
        if (item.type == itemType.weapon) {
            Weapon w = (Weapon)item;
            tooltip.EnableToolTip(ItemRarityColor(w) + "\n" 
                + "Level: " + w.level.ToString() + "\n" 
                + "Damage: " + w.damage.ToString() + "\n" 
                + "Speed: " + w.attackSpeed.ToString() + "\n" 
                + w.description);
        } else if (item.type == itemType.gear) {
            
            
        } else {
            //consumable
        }
        
    }

    /// <summary>
    /// Grabs the rarity of the item and changes the color of its name for the UI.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private string ItemRarityColor(Item item) {
        int level = item.level;
        tooltip.gameObject.SetActive(true);
        switch (level) {
            case 1:
                return "<color=grey>" + item.name + "</color>";
            case 2:
                return "<color=white>" + item.name + "</color>";
            case 3:
                return "<color=green>" + item.name + "</color>";
            case 4:
                return "<color=blue>" + item.name + "</color>" ;
            case 5:
                return "<color=yellow>" + item.name + "</color>";
            case 6:
                return "<color=orange>" + item.name + "</color>";
            default:
                return "<color=red>" + item.name + "</color>";
        }
    }

    /// <summary>
    /// Called when disabling the tooltip. 
    /// </summary>
    public void DisableToolTip() {
        tooltip.DisableToolTip();
    }

    /// <summary>
    /// If there is an empty slot, then add an item to that specific slot.  
    /// </summary>
    /// <param name="item"></param>

    public void AddItem(Item item) {
        if (item.stackable) { //first it will check if the item is stackable or not
            for (int i = 0; i < m_slots.Count; i++) {
                if(m_slots[i].m_item != menuManager.defaultItem) { //if there is an item here
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
            if(m_slots[i].m_item == menuManager.defaultItem) {
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
    /// <summary>
    /// Removes a specific item from one of the slots. 
    /// </summary>
    /// <param name="item"></param>
    public void RemoveSpecificItem(Item item) {
        foreach(Slot slot in m_slots) {
            if(slot.m_item == item) {
                slot.DeleteItemFromSlot();
                UpdateSlotUI();
                return;
            }
        }
    }

    public void DropItemContext(InputAction.CallbackContext context) {
        if (context.performed) {
            DropItem();
        }
    }
    private void DropItem() {
        Slot slot = m_slots[(int)m_currentItem];
        if(slot.m_item != menuManager.defaultItem) {
            GameObject droppedItem = Instantiate(defaultInteractable, player.gameObject.transform.position, Quaternion.identity);
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
                m_currentItem = 3;
            }
            else if (ctx == new Vector2(1, 0)) { //right
                m_currentItem = 1;
            }
            else if (ctx == new Vector2(0, -1)) { //down
                m_currentItem = 2;
            }
            else { //up
                m_currentItem = 4;
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
        if (slot.m_item != null) {
            //first we check to see if the item is a consumable
            if (slot.m_item.type == itemType.consumable) {
                print("Test");
                slot.currentStack--;
                if (slot.currentStack <= 0) {
                    RemoveItem();
                }
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
            slot.transform.parent.GetComponent<Image>().color = new Color32(77,77,77,255);
        }
        //m_slots[(int)m_currentItem].GetComponent<Image>().color = Color.yellow;
        m_slots[(int)m_currentItem].gameObject.transform.parent.GetComponent<Image>().color = new Color32(70, 29, 0, 255); //brown
        foreach (Slot slot in m_slots) {
            if(slot.m_item != menuManager.defaultItem) {
                menuManager.UpdateInventoryMenuUI(); //update the menu slots
                PAC.ChangedWeapon();
                return;
            }
        }        
    }

    public float GetCurrentItem() {
        return m_currentItem;
    }

    public weaponType CheckCurrentItemForWeaponType()
    {
        if(m_slots[(int)m_currentItem].m_item is Weapon weapon)
            return weapon.weapon;
        return weaponType.none;
    }

    public bool CheckIfOpenSlot() {
        foreach(Slot slot in m_slots) {
            if(slot.m_item == menuManager.defaultItem) {
                return true;
            }
        }
        return false;
    }
    public void SwapItems(int num1, int num2) {
        Item temp = m_slots[num1].m_item;
        m_slots[num1].AddItemToSlot(m_slots[num2].m_item);
        m_slots[num2].AddItemToSlot(temp);

        menuManager.UpdateInventoryMenuUI();
        UpdateSlotUI();
    }
}
