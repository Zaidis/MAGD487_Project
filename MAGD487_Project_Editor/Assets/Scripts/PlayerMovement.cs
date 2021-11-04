﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    public Vector2 movement;
    [SerializeField]
    float speed;
    
    private GroundDetector groundDetector; //roll elements
    private bool wantToRoll = false;
    public bool rolling = false;
    [Space]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    private float timer = 0;
    private Vector2 initialRollDirection;

    public static PlayerMovement instance;
    public bool canReceiveAttackInput; //Attack elements
    public bool AttackInputReceived;
    public bool canMove;

    void Awake()
    {
        canReceiveAttackInput = true;
        AttackInputReceived = false;
        canMove = true;
        instance = this;
        groundDetector = GetComponentInChildren<GroundDetector>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movement = movement.normalized;
        if (rolling)
            timer -= Time.deltaTime;

    }

    private void FixedUpdate()
    {
        if(canMove) { //TODO Disable moving while in animation loop, figure this out
            if(!rolling) {
                rb.velocity = new Vector3(movement.x * speed, rb.velocity.y);
                if(groundDetector.grounded && wantToRoll) {
                    rolling = true;
                    timer = rollTime;
                    initialRollDirection = new Vector2(rollForce * (movement.x >= 0 ? 1 : -1), 0);
                }
            } else {
                if(timer > 0) {
                    rb.AddForce(initialRollDirection); //TODO mess with the force addition /over time?
                }
                if(rb.velocity.x == 0)
                    timer = 0;
                if(timer < -rollTime) {
                    rolling = false;
                    wantToRoll = false;
                }
            }
        }        
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        movement = new Vector2(callbackContext.ReadValue<Vector2>().x, callbackContext.ReadValue<Vector2>().y);
    }
    public void RollInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            wantToRoll = true;
    }
    public void Attack(InputAction.CallbackContext callbackContext)
    {        
        if(callbackContext.performed) {
            if(canReceiveAttackInput && groundDetector.grounded) {
                AttackInputReceived = true;
                canReceiveAttackInput = false;
                canMove = false; //TODO Move here?
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
