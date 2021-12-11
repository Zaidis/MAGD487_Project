using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float damage;
    private void Start()
    {
        rb.velocity = new Vector2(speed * this.transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            damageable.Damage(damage);
            Destroy(this.gameObject);
        }else if (collision.CompareTag("Ground"))
        {
            Destroy(this.gameObject);
        }
    }
}
