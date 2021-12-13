using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float damage;
    [SerializeField] bool attachedParticles = false;
    private void Start()
    {
        rb.velocity = new Vector2(speed * this.transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            damageable.Damage(damage);

            if (attachedParticles)
                DestroyParticles();

            Destroy(this.gameObject);
        }else if (collision.CompareTag("Ground"))
        {
            if (attachedParticles)
                DestroyParticles();

            Destroy(this.gameObject);
        }
    }

    void DestroyParticles()
    {
        ParticleSystem s = GetComponentInChildren<ParticleSystem>();
        s.transform.SetParent(null);
        ParticleSystem.EmissionModule f = s.emission;
        f.enabled = false;
        Destroy(s.gameObject, 5);
    }
}
