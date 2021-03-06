using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public static PlayerAnimationController instance;
    PlayerMovement playerMovement;
    [SerializeField] GroundDetector groundDetector;
    public Animator anim;
    SpriteRenderer sr;
    public AnimatorOverrideController sword;
    public AnimatorOverrideController dagger;
    public weaponType wt = weaponType.none;
    public GameObject bowObj;

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
        wt = InventoryManager.instance.CheckCurrentItemForWeaponType();
        Attack.instance.canReceiveAttackInput = true;
        if(wt == weaponType.none) {
            Attack.instance.canReceiveAttackInput = false;
        }
        else if(wt == weaponType.dagger) {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = dagger;
        }
        else if(wt == weaponType.grapple) {
            Attack.instance.canReceiveAttackInput = false;
            bowObj.SetActive(true);
            return;
        }
        else if(wt == weaponType.sword) {
            gameObject.GetComponent<Animator>().runtimeAnimatorController = sword;
        }
        else if(wt == weaponType.bow)
        {
            Attack.instance.canReceiveAttackInput = false;
            bowObj.SetActive(true);
            return;
        }
        bowObj.SetActive(false);
    }
}
