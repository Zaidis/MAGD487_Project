using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
public class MenuManager : MonoBehaviour
{

    [Header("All Sections")]
    public List<GameObject> sections = new List<GameObject>();
    public List<bool> sectionBools = new List<bool>(); //IN ORDER: Inventory, Stats, Options
    [SerializeField] private int currentSection;

    [Header("Inventory Section")]
    public menu_slot[] slots;
    public menu_slot currentSlot;
    public Item defaultItem;
    [SerializeField] private menu_slot swap_initial;
    [SerializeField] private menu_slot swap_newLocation;

    [Header("Popup Objects")]
    [SerializeField] private TextMeshProUGUI popup;
    [SerializeField] private GameObject sectionsParent; //holds the bottom row
    private int index;
    private int swapIndexInitial;
    private int swapIndexLast;
    private bool wantsToSwap;

    private void Start() {
        sectionBools[0] = true; //intialize inventory bool
    }

    /// <summary>
    /// This allows for the D Pad to be used for multiple sections depending on what section is currently viewed.
    /// </summary>
    /// <param name="context"></param>
    public void DPadSelection(InputAction.CallbackContext context) { //this way only one function is called rather
                                                                     //than three separate ones. 
        if (sectionBools[0]) {
            SelectItem(context);
        } else if (sectionBools[1]) {

        } else if (sectionBools[2]) {

        }
    }
    #region INVENTORY

    /// <summary>
    /// Allows the player to shuffle through the menu slots for the inventory.
    /// </summary>
    /// <param name="context"></param>
    private void SelectItem(InputAction.CallbackContext context) {
        if (context.performed) {
            if (sectionBools[0] == true) { //if inventory is active
                Vector2 ctx = context.ReadValue<Vector2>();
                //Debug.Log("I used " + ctx);

                if (currentSlot == slots[0]) {

                    //right/down
                    if (ctx == new Vector2(1, 0)) {
                        //move right
                        index = 1;
                        currentSlot = slots[index];
                    }
                    else if (ctx == new Vector2(0, -1)) {
                        index = 3;
                        currentSlot = slots[index];
                    }

                }
                else if (currentSlot == slots[1]) {

                    if (ctx == new Vector2(-1, 0)) {
                        //move right
                        index = 0;
                        currentSlot = slots[index];
                    }
                    else if (ctx == new Vector2(0, -1)) {
                        index = 2;
                        currentSlot = slots[index];
                    }

                }
                else if (currentSlot == slots[2]) {

                    if (ctx == new Vector2(-1, 0)) {
                        //move right
                        index = 3;
                        currentSlot = slots[index];
                    }
                    else if (ctx == new Vector2(0, 1)) {
                        index = 1;
                        currentSlot = slots[index];
                    }

                }
                else {

                    if (ctx == new Vector2(1, 0)) {
                        //move right
                        index = 2;
                        currentSlot = slots[index];
                    }
                    else if (ctx == new Vector2(0, 1)) {
                        index = 0;
                        currentSlot = slots[index];
                    }
                }
                HoverOverCurrentSlot();
            }
        }
        
    }

    public void ClickMenuSlot(int num) {
        index = num;
        currentSlot = slots[num];
        HoverOverCurrentSlot();
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
            slot.GetComponent<Image>().color = new Color32(219, 147, 3, 255);
        }
        currentSlot.GetComponent<Image>().color = Color.green;
    }

    /// <summary>
    /// User chooses what item they want to swap to a different place. 
    /// </summary>
    public void SelectSwap(InputAction.CallbackContext context) {
        if (context.performed) {
            swap_initial = currentSlot;
            swapIndexInitial = index;
            wantsToSwap = true;
            ActivatePopup("Select a slot to swap with!");
        }
    }   

    public void Swap() {

        //swap items
        InventoryManager.instance.SwapItems(swapIndexInitial, swapIndexLast);
        DeactivatePopup();
        wantsToSwap = false;
    }
    #endregion
    private void ActivatePopup(string line) {

        popup.text = line;
        DeactivateSections();
        popup.gameObject.SetActive(true);
        
    }

    private void DeactivatePopup() {

        popup.gameObject.SetActive(false);
        ActivateSections();
        popup.text = "";
    }

    /// <summary>
    /// Turns on the bottom row in the menu.
    /// </summary>
    private void ActivateSections() {
        sectionsParent.SetActive(true);
    }

    /// <summary>
    /// Turns off the bottom row in the menu. Allows the popup to be seen.
    /// </summary>
    private void DeactivateSections() {
        sectionsParent.SetActive(false);
    }

    public void ChangeSectionsContext(InputAction.CallbackContext context) {
        if (context.performed) {
            currentSection += (int)context.ReadValue<float>();
            ValidateSection();
            ChangeSections();
        }
    }

    private void ValidateSection() {
        if(currentSection > sections.Count - 1) {
            currentSection = 0;
        } else if (currentSection < 0) {
            currentSection = sections.Count - 1;
        }
    }
    private void ChangeSections() {
        for(int i = 0; i < sections.Count; i++) {
            sections[i].SetActive(false);
            sectionBools[i] = false;
        }
        
        sections[currentSection].SetActive(true);
        sectionBools[currentSection] = true;
    }
    public void ClickSection(int num) {
        currentSection = num;
        ChangeSections();
    }
    public void Accept(InputAction.CallbackContext context) {
        if (context.performed) {
            if (wantsToSwap) {
                if (currentSlot != swap_initial) {
                    swap_newLocation = currentSlot;
                    swapIndexLast = index;
                    Swap();
                }
            }
        }
    }
}
