using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] GroundDetector groundDetector;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField]
    private GameObject dhb;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("MoveX", playerMovement.movement.x);

        if(groundDetector.grounded)
            anim.SetFloat("MoveY", 0);
        else
            anim.SetFloat("MoveY", 1);

        float val = playerMovement.movement.x;

        if(PlayerMovement.instance.canMove) {
            if(val > 0) {
                sr.flipX = false;
                dhb.transform.localScale = new Vector3(1, 1, 1);
            } else if(val < 0) {
                sr.flipX = true;
                dhb.transform.localScale = new Vector3(-1, 1, 1);
            }
        }             
    }
}
