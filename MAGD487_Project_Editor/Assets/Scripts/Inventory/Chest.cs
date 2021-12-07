using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Chest : MonoBehaviour
{
    //THIS OBJECT NEEDS THE CHEST TAG
    [Range(1, 15)]
    [SerializeField] private int minimumLevel;

    [Range(1, 15)]
    [SerializeField] private int maximumLevel;

    [SerializeField] private Sprite openSprite;

    private bool canbeOpened;
    public bool opened;
    public Item item;


    private void Start() {
        //ChooseItem();
    }


    private void ChooseItem() {
        List<Item> list = ItemDatabase.instance.GetChestList(minimumLevel, maximumLevel);

        item = ItemDatabase.instance.GetRandomItemFromList(list);

    }

    /*public void OpenChestContext(InputAction.CallbackContext context) {
        if (context.performed) {
            if (!opened) {
                if (canbeOpened) {
                    OpenChest();
                }
            }
        }
    } */
    public void OpenChest() {
        if (opened == false) {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;

            GameObject droppedItem = Instantiate(InventoryManager.instance.defaultInteractable, transform.position, Quaternion.identity);
            droppedItem.transform.GetChild(0).GetComponent<Interactable>().item = item;

            opened = true;
            InventoryManager.instance.player.GetComponent<PickUp>().nearChest = false;
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            InventoryManager.instance.pickUpText.text = "Open chest?";
            InventoryManager.instance.pickUpText.gameObject.SetActive(true);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        InventoryManager.instance.pickUpText.gameObject.SetActive(false);
    }
}
