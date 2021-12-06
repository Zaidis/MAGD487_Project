using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoseAnimationController : MonoBehaviour
{
    Animator anim;
    NoseAttack noseAttack;
    EnemyWalk enemyWalk;
    Rigidbody2D rb;
    bool currentlyAttacking = false;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        noseAttack = GetComponentInParent<NoseAttack>();
        enemyWalk = GetComponentInParent<EnemyWalk>();
    }

    private void Start()
    {
        rb = enemyWalk.GetRigidbody2D();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("moveX", rb.velocity.normalized.x);

        if(noseAttack.attack && !currentlyAttacking)
        {
            anim.SetTrigger("StartAttacking");
            currentlyAttacking = true;
        }else if(!noseAttack.attack && currentlyAttacking)
        {
            anim.SetTrigger("StopAttacking");
            currentlyAttacking = false;
        }
    }
}
