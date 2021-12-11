using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController instance;
    PlayerMovement playerMovement;
    [SerializeField] GroundDetector groundDetector;
    Animator anim;
    SpriteRenderer sr;
    public AnimatorOverrideController sword;
    public AnimatorOverrideController none;
    public AnimatorOverrideController dagger;

    [SerializeField]
    private GameObject dhb;
    private void Awake()
    {
        new AnimatorOverrideController(GetComponent<Animator>().runtimeAnimatorController);
        sr = GetComponent<SpriteRenderer>();
        playerMovement = GetComponentInParent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("MoveX", playerMovement.movement.x);
        anim.SetBool("Dodging", playerMovement.rolling);
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
    public void ChangedWeapon()
    {
        weaponType wt = InventoryManager.instance.CheckCurrentItemForWeaponType(); //Move to animation handler script?
        Attack.instance.canReceiveAttackInput = true;
        if(wt == weaponType.none) {
            Attack.instance.canReceiveAttackInput = false;
        }
        if(wt == weaponType.dagger) {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = dagger;
        }
        if(wt == weaponType.grapple) {
            Attack.instance.canReceiveAttackInput = false;
            Attack.instance.Shoot();
        }
        if(wt == weaponType.sword) {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = sword;
        }
    }
}
