using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBoxAttack : MonoBehaviour
{
    [SerializeField]
    float DaggerDamage = 5f;
    [SerializeField]
    float kbForce = 100f;
    private void OnTriggerEnter2D(Collider2D collision) //not checking collision, apply kockback, 
    {
        if(collision.gameObject.GetComponent<Damageable>() != null) {
            Debug.Log("Damage Dealt");
            collision.gameObject.GetComponent<Damageable>().Damage(DaggerDamage);
            //Vector2 knockback = collision.gameObject.transform.position - PlayerMovement.instance.transform.position; //Make knock back based on attack direction
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(knockback * kbForce);
        }
    }
}
