using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldDamageable : Damageable
{
    [SerializeField] KoboldAnimationController anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D[] hitboxes;
    [SerializeField] GameObject minimapIndicator;
    bool dying = false;
    public override void Death()
    {
        print(gameObject.name + " is dead");
        anim.Death();
        dying = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (dying && collision.collider.CompareTag("Ground") && rb.velocity.y < 0.1)
        {
            for (int i = 0; i < hitboxes.Length; i++)
            {
                hitboxes[i].enabled = false;
            }
            rb.simulated = false;
            minimapIndicator.SetActive(false);
            this.enabled = false;
        }
    }
}
