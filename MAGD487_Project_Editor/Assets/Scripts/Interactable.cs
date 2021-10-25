using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Scriptable Object")]
    public Item item;

    public bool canBePickedUp;

    private void Start() {
        transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
        Invoke("Active", 3f);
    }

    private void Active() {
        canBePickedUp = true;
        print("I am active");
    }
    public void ItemPickUp() {
        InventoryManager.instance.AddItem(item);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
