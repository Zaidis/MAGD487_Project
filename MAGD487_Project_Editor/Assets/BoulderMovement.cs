using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderMovement : MonoBehaviour
{
    [SerializeField] float throwForce;
    Rigidbody2D rb;
    [SerializeField] float damage;
    [SerializeField] float lifeTime = 10;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 dir = player.position - this.transform.position;
        dir.Normalize();
        rb.velocity = dir * throwForce;
        Destroy(this.gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Enemy") && collision.collider.TryGetComponent<Damageable>(out Damageable damageable))
        {
            damageable.Damage(damage);
            Destroy(this.gameObject);
        }
    }
}
