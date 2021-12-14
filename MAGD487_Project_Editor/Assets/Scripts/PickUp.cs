using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PickUp : MonoBehaviour
{
    public GameObject currentInteractable;
    private Chest nearbyChest;
    public bool nearChest;
    public void PickUpItem(InputAction.CallbackContext context) {
        if (context.performed) {
            if (currentInteractable != null) {
                currentInteractable.GetComponent<Interactable>().ItemPickUp();
                currentInteractable = null;
            }
        }
    }

    public void OpenChestContext(InputAction.CallbackContext context) {
        if (context.performed) {
            if(nearbyChest != null) {
                //there is a nearby chest
                nearbyChest.OpenChest();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.layer == 9) {
            if (currentInteractable == null) {
                currentInteractable = collision.gameObject;
            }
            else if (Vector2.Distance(this.transform.position, collision.transform.position) < 
                Vector2.Distance(this.transform.position, currentInteractable.transform.position)) {

                currentInteractable = collision.gameObject;
            }
            InventoryManager.instance.pickUpText.text = "Pick up " + currentInteractable.GetComponent<Interactable>().item.name + " ?";
            InventoryManager.instance.pickUpText.gameObject.SetActive(true);
        } else if (collision.gameObject.CompareTag("Chest")) {
            //near a chest object
            nearbyChest = collision.gameObject.GetComponent<Chest>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject == currentInteractable) {
            currentInteractable = null;
            InventoryManager.instance.pickUpText.gameObject.SetActive(false);
        } else if (collision.gameObject.CompareTag("Chest")) {
            //near a chest object
            nearbyChest = null;
        }
    }
}
