using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerMovement playerMovement;
    Jump jumpScript;
    Animator anim;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        jumpScript = GetComponentInParent<Jump>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("MoveX", playerMovement.movement.x);
        float val = playerMovement.movement.x;
        if (val > 0)
            sr.flipX = false;
        else if (val < 0)
            sr.flipX = true;
    }
}
