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
    private void Awake()
    {
        canReceiveAttackInput = true;
        AttackInputReceived = false;        
        instance = this;
        groundDetector = GetComponentInChildren<GroundDetector>();
    }
    public void AttackInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.performed) {
            if(canReceiveAttackInput && groundDetector.grounded) {
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
