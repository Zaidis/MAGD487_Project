using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    public static Attack instance;
    public bool canReceiveAttackInput;
    public bool AttackInputReceived;
    private GroundDetector groundDetector;
    private InventoryManager im;
    private Camera mainCam;

    public GameObject shot;
    public float speed;
    private void Awake()
    {
        canReceiveAttackInput = true;
        AttackInputReceived = false;        
        instance = this;
        groundDetector = GetComponentInChildren<GroundDetector>();
        im = InventoryManager.instance;
        mainCam = GameObject.Find("Camera").GetComponent<Camera>();
    }
    public void AttackInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed) {
            if(canReceiveAttackInput && groundDetector.grounded) {
                weaponType wt = im.CheckCurrentItemForWeaponType(); //Move to animation handler script?
                if(wt == weaponType.none) {
                    //no attack animation
                } if(wt == weaponType.dagger) {
                    //dagger attack animation
                } if(wt == weaponType.grapple) {
                    //no attack animation
                    Shoot();
                } if(wt == weaponType.sword) {
                    //sword attack animation
                }
                AttackInputReceived = true;
                canReceiveAttackInput = false;
            } else {
                return;
            }
        }
    }
    public void InputManager()
    {
        if(!canReceiveAttackInput) {
            canReceiveAttackInput = true;
        } else {
            canReceiveAttackInput = false;
        }
    }
    public void Shoot()
    {
        Vector3 direction;
        direction = mainCam.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 0.0f));
        direction = mainCam.ScreenToWorldPoint(direction);
        direction = direction - transform.position;
        GameObject grappleShot = Instantiate(shot, transform.position, Quaternion.Euler(Vector3.zero));
        grappleShot.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
    }
}
