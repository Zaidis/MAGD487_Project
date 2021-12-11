using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDamagable : Damageable
{
    MeleeEnemy meleeEnemy;
    Rigidbody2D rb;
    [SerializeField] GameObject deadBody;
    GoblinAnimationController anim;
    bool dying = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        meleeEnemy = GetComponent<MeleeEnemy>();
        anim = GetComponentInChildren<GoblinAnimationController>();
    }

    public override void Damage(float amt)
    {
        if(meleeEnemy.blockCapable && meleeEnemy.blocking)
        {
            //Block the attack!
            meleeEnemy.Block();
        } else if (meleeEnemy.blockCapable && !meleeEnemy.blocking)
            base.Damage(amt);
        else if (!meleeEnemy.blockCapable)
            base.Damage(amt);
    }

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
