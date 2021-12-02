using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{

    [Header("All Sections")]
    public List<GameObject> sections = new List<GameObject>();
    public List<bool> sectionBools = new List<bool>(); //IN ORDER: Inventory, Stats, Options
    [SerializeField] private int currentSection;

    [Header("Inventory Section")]
    public menu_slot[] slots;
    public menu_slot currentSlot; //the item that is currently selected
    public Item defaultItem; // <---- IMPORTANT: This is THE empty item!!! <---------
    [SerializeField] private menu_slot swap_initial; //the item you want to swap
    [SerializeField] private menu_slot swap_newLocation; //the new place for that item to swap
    private int index;
    private int swapIndexInitial;
    private int swapIndexLast;
    private bool wantsToSwap;

    [Header("Popup Objects")]
    [SerializeField] private TextMeshProUGUI popup;
    [SerializeField] private GameObject sectionsParent; //holds the bottom row

    [Header("Options Section")]
    [SerializeField] private Button firstButton; //so the event system knows where to look first when selected!
    [SerializeField] private GameObject firstSettings; //the first thing in the settings menu. master slider
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject optionButtons;//the first selection of buttons outside of the settings. quit game, menu, etc
    private bool settingsMenuOn;

    [Header("Statistics Section")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI movementText;
    [SerializeField] private TextMeshProUGUI goldAmountText;
    //[SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI damageText;
    
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
            if (sectionBools[0]) {
                swap_initial = currentSlot;
                swapIndexInitial = index;
                wantsToSwap = true;
                ActivatePopup("Select a slot to swap with!");
            }
        }
    }   

    public void Swap() {
        if (sectionBools[0]) {
            //swap items
            InventoryManager.instance.SwapItems(swapIndexInitial, swapIndexLast);
            DeactivatePopup();
            wantsToSwap = false;
        }
    }

    private void NoLongerSwapping() {
        if (sectionBools[0]) {
            swap_initial = null;
            swapIndexInitial = -1;
            DeactivatePopup();
            wantsToSwap = false;
        }
    }
    #endregion

    #region SECTIONS


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
            if (!settingsMenuOn) {
                currentSection += (int)context.ReadValue<float>();
                ValidateSection();
                ChangeSections();
            }
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
        UpdateStatisticsSection();
        sections[currentSection].SetActive(true);
        sectionBools[currentSection] = true;
        if(sectionBools[2] == true) {
            //options menu
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        } else {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public void ClickSection(int num) {
        currentSection = num;
        ChangeSections();
    }

    #endregion

    #region STATISTICS

    public void UpdateStatisticsSection() {
        //damage, health, gold amount, movement speed, armor?, 

        goldAmountText.text = "Gold: " + StatisticsManager.instance.GetGoldAmount().ToString();
        damageText.text = "Damage: " + StatisticsManager.instance.m_damageAmount.ToString();
        healthText.text = "Health: " + StatisticsManager.instance.m_healthAmount.ToString();
        movementText.text = "MovementSpeed: " + StatisticsManager.instance.m_movementSpeedAmount.ToString();
    }



    #endregion

    #region OPTIONS

    public void Settings() {
        Debug.Log("Accessing Settings...");
        optionButtons.SetActive(false);
        DeactivateSections();
        EventSystem.current.SetSelectedGameObject(null);
        settingsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSettings);
        settingsMenuOn = true;
    }

    public void DeactivateSettings() {
        Debug.Log("Exiting Settings Menu...");
        settingsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        optionButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        ActivateSections();
        settingsMenuOn = false;
    }

    public void MainMenu() {
        Debug.Log("Main Menu...");
    }

    public void QuitGame() {
        Debug.Log("Quitting the game...");
        Application.Quit(); //will make a popup asking are you sure
    }


    #endregion
    public void Accept(InputAction.CallbackContext context) { //when you press A on the controller
        if (context.performed) {
            if (sectionBools[0]) {
                if (wantsToSwap) {
                    if (currentSlot != swap_initial) {
                        swap_newLocation = currentSlot;
                        swapIndexLast = index;
                        Swap();
                    }
                }
            } else if (sectionBools[1]) {

            } else if (sectionBools[2]) {
                //OPTIONS MENU
                
            }
        }
    }

    public void Decline(InputAction.CallbackContext context) { //pressing B on the controller
        if (context.performed) {
            if (sectionBools[0]) {
                if (wantsToSwap) {
                    //no longer wanting to swap
                    NoLongerSwapping();
                } else {
                    InventoryManager.instance.ManageInventory();
                }
            } else if (sectionBools[1]) {

                InventoryManager.instance.ManageInventory();

            } else if (sectionBools[2]) {
                //options menu
                if (settingsMenuOn) {
                    DeactivateSettings();
                    return;
                } else {
                    //backing out of the menu
                    InventoryManager.instance.ManageInventory();
                }
            }
        }
    }
}
