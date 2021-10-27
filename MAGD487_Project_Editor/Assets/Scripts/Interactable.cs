using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Header("Scriptable Object")]
    public Item item;
    private void Start() {
        transform.parent.GetComponent<Rigidbody2D>().AddForce(transform.up * 5f, ForceMode2D.Impulse);
    }
    public void ItemPickUp() {
        InventoryManager.instance.AddItem(item);
        Destroy(this.gameObject.transform.parent.gameObject);
    }
}
