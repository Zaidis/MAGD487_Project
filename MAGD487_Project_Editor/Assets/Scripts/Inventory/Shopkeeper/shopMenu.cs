using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class shopMenu : MonoBehaviour
{
    //This is for the shopkeeper UI, not the normal menu
    public static shopMenu instance;

    public bool canShop; //active if the player is in range of the shopkeeper

    private PlayerMovement player;
    [SerializeField] private GameObject theMenu;
    [SerializeField] private GameObject firstButton;
    [SerializeField] private TextMeshProUGUI gold;
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
            if (canShop)
                TurnOnMenu();
        }
    }

    public void UpdateGoldUI() {
        gold.text = "Gold: " + (0 + StatisticsManager.instance.GetGoldAmount()).ToString();
    }
    public void TurnOnMenu() {
        UpdateGoldUI();
        theMenu.SetActive(true);
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("Shop Menu");
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        player.canMove = false;
    }

    public void TurnOffMenu() {
        UpdateGoldUI();
        theMenu.SetActive(false);
        player.GetComponent<PlayerInput>().currentActionMap = player.GetComponent<PlayerInput>().actions.FindActionMap("Player");
        EventSystem.current.SetSelectedGameObject(null);
        player.canMove = true;
    }

}
