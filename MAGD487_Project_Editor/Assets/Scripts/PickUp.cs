using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject currentInteractable;
    public void PickUpItem() {
        if(currentInteractable != null) {
            currentInteractable.GetComponent<Interactable>().ItemPickUp();
            currentInteractable = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if(collision.gameObject.layer == 9) {
            if (currentInteractable == null) {
                currentInteractable = collision.gameObject;
            }
            else if (Vector2.Distance(this.transform.position, collision.transform.position) < 
                Vector2.Distance(this.transform.position, currentInteractable.transform.position)) {

                currentInteractable = collision.gameObject;
            }
            InventoryManager.instance.pickUpText.text = "Pick up " + currentInteractable.GetComponent<Interactable>().item.name + "?";
            InventoryManager.instance.pickUpText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject == currentInteractable) {
            currentInteractable = null;
            InventoryManager.instance.pickUpText.gameObject.SetActive(false);
        }
    }
}
