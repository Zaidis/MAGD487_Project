using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldAnimationController : MonoBehaviour
{
    RangedAttackAI koboldAttack;
    EnemyWalk enemyWalk;
    Animator anim;
    Rigidbody2D rb;
    private void Awake()
    {
        koboldAttack = GetComponentInParent<RangedAttackAI>();
        enemyWalk = GetComponentInParent<EnemyWalk>();
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("moveX", rb.velocity.x);
        anim.SetFloat("moveY", rb.velocity.y);
    }

    public void Fire()
    {
        anim.SetTrigger("Attack");
        rb.velocity = new Vector2(0, rb.velocity.y);
        enemyWalk.enabled = false;
    }
    public void Death()
    {
        anim.SetTrigger("Die");
        enemyWalk.enabled = false;
        koboldAttack.enabled = false;
        this.enabled = false;
    }
}
