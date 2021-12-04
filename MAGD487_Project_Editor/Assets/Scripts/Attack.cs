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
    private void Awake()
    {
        canReceiveAttackInput = true;
        AttackInputReceived = false;        
        instance = this;
        groundDetector = GetComponentInChildren<GroundDetector>();
        im = InventoryManager.instance;
    }
    public void AttackInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed) {
            if(canReceiveAttackInput && groundDetector.grounded) {
                weaponType wt = im.CheckCurrentItemForWeaponType();
                //TODO Change Animation based on weapon type;
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
}
