using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopkeeper : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            InventoryManager.instance.pickUpText.text = "Talk to Bo?";
            InventoryManager.instance.pickUpText.gameObject.SetActive(true);
            shopMenu.instance.canShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        InventoryManager.instance.pickUpText.gameObject.SetActive(false);
        shopMenu.instance.canShop = false;
    }

}
