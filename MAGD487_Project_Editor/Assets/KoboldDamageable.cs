using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldDamageable : Damageable
{
    [SerializeField] KoboldAnimationController anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject deadBody;
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
            GameObject g = Instantiate(deadBody, this.transform.position, Quaternion.identity);
            g.transform.localScale = this.transform.localScale;
            Destroy();
        }
    }
}
