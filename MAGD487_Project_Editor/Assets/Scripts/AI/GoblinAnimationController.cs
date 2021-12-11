using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimationController : MonoBehaviour
{
    EnemyWalk enemyWalk;
    Animator anim;
    Rigidbody2D rb;
    private void Awake()
    {
        enemyWalk = GetComponentInParent<EnemyWalk>();
        anim = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("moveX", rb.velocity.x);
        anim.SetFloat("moveY", rb.velocity.y);
    }

    public void Swing()
    {
        anim.SetTrigger("Attack");
    }

    public void Block()
    {
        anim.SetTrigger("Block");
    }
    public void Death()
    {
        anim.SetTrigger("Die");
        this.enabled = false;
        enemyWalk.enabled = false;
    }
}
