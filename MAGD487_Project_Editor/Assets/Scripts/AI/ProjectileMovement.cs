using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    public float damage;
    [SerializeField] bool attachedParticles = false;
    [SerializeField] bool stickToWalls = false;
    bool deactivated = false;
    bool moveRightVector = false;
    private void Start()
    {
        if(moveRightVector)
            rb.velocity = speed * transform.right;
        else
            rb.velocity = new Vector2(speed * this.transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!deactivated && collision.TryGetComponent<Damageable>(out Damageable damageable))
        {
            Weapon bow = (Weapon)InventoryManager.instance.m_slots[(int)InventoryManager.instance.GetCurrentItem()].m_item;
            damageable.Damage(bow.damage);
            
            if (attachedParticles)
                DestroyParticles();

            Destroy(this.gameObject);
        }else if (collision.CompareTag("Ground"))
        {
            if (attachedParticles)
                DestroyParticles();
            if (stickToWalls)
            {
                deactivated = true;
                rb.velocity = Vector3.zero;
                rb.simulated = false;
                Destroy(this.gameObject, 10);
                return;
            }
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
