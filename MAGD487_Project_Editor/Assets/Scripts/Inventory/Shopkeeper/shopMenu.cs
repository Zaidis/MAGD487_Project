using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;
public class shopMenu : MonoBehaviour
{
    //This is for the shopkeeper UI, not the normal menu
    public static shopMenu instance;
    public PixelPerfectCamera cam;
    public bool canShop; //active if the player is in range of the shopkeeper

    private PlayerMovement player;
    //MENUS
    [SerializeField] private GameObject theMenu;
    [SerializeField] private GameObject sellMenu;
    [SerializeField] private GameObject itemSlots; //the four item slots in the base game. turn them off when in menu
    [SerializeField] private GameObject minimap;

    [SerializeField] private List<shopItemUI> myShopItems = new List<shopItemUI>();
    [SerializeField] private List<shopItemUI> myInventoryItems = new List<shopItemUI>();

    public bool shopActive;
    public bool sellActive;

    [SerializeField] private GameObject firstButton; //shop menu first button
    [SerializeField] private GameObject firstSell; //sell menu first button
    [SerializeField] private TextMeshProUGUI gold1;
    [SerializeField] private TextMeshProUGUI gold2;
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        player = InventoryManager.instance.player;
    }

    public void CheckIfShop(InputAction.CallbackContext context) {
        if (context.performed) {
            if (canShop) {
                BeginTurningOnMenu();
            }  
        }
    }

    public void UpdateGoldUI() {
        gold1.text = "Gold: " + (0 + StatisticsManager.instance.GetGoldAmount()).ToString();
        gold2.text = "Gold: " + (0 + StatisticsManager.instance.GetGoldAmount()).ToString();
    }

    public void Back(InputAction.CallbackContext context) {
        if (context.performed) {
            if (sellActive) {
                TurnOffSell();
            }
            else {
                TurnOffMenu();
            }
        }
    }

    private void BeginTurningOnMenu() {
        FindObjectOfType<shopkeeper>().GetComponent<Animator>().SetBool("useShop", true);
        StopAllCoroutines();
        StartCoroutine(ZoomIn());
        player.canMove = false;

        itemSlots.SetActive(false);
        minimap.SetActive(false);
        Invoke("TurnOnMenu", 4);
    }

    IEnumerator ZoomIn() {
        while(cam.assetsPPU < 128) {
            yield return new WaitForSeconds(0.02f);
            cam.assetsPPU += 2;
        }
    }

    IEnumerator ZoomOut() {
        while (cam.assetsPPU > 64) {
            yield return new WaitForSeconds(0.02f);
            cam.assetsPPU -= 2;
        }
    }

    public void TurnOnMenu() {
        UpdateGoldUI();
        theMenu.SetActive(true);
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("Shop Menu");
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        player.canMove = false;
        shopActive = true;
    }

    public void TurnOffMenu() {
        UpdateGoldUI();
        sellMenu.SetActive(false);
        theMenu.SetActive(false);
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("Player");
        EventSystem.current.SetSelectedGameObject(null);
        shopActive = false;
        player.canMove = true;
        itemSlots.SetActive(true);
        minimap.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ZoomOut());
        FindObjectOfType<shopkeeper>().GetComponent<Animator>().SetBool("useShop", false);
    }

    public void TurnOnSell() {
        UpdateGoldUI();
        UpdateSellList();
        theMenu.SetActive(false);
        sellMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSell.gameObject);
        sellActive = true;
    }

    public void TurnOffSell() {
        UpdateGoldUI();
        theMenu.SetActive(true);
        sellMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        sellActive = false;
    }

    public void UpdateShoppingList() {
        for(int i = 0; i < myShopItems.Count; i++) {
            Item item;
            if(i != myShopItems.Count - 1) {
                //not the middle slot, does not need the list from the dungeon level
                List<Item> randList = ItemDatabase.instance.GetRandomList(StateController.dungeonLevel);
                item = ItemDatabase.instance.GetRandomItemFromList(randList);
                myShopItems[i].UpdateIcon(item);
            } else {
                //middle slot, need the list of the dungeon level
                List<Item> uniqueList = ItemDatabase.instance.GetList(StateController.dungeonLevel);
                item = ItemDatabase.instance.GetRandomItemFromList(uniqueList);
                myShopItems[i].UpdateIcon(item);
            }
        }
    }

    /// <summary>
    /// Called for when the player wants to sell their items to the shopkeeper. 
    /// </summary>
    public void UpdateSellList() {
        for(int i = 0; i < InventoryManager.instance.m_slots.Count; i++) {
            Item item = InventoryManager.instance.m_slots[i].m_item;
            myInventoryItems[i].UpdateSellSlot(item);
        }
    }
}
