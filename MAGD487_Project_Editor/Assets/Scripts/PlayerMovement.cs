using System.Collections;
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
    private bool rolling = false;
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    private float timer = 0;
    private Vector2 initialRollDirection;

    void Awake()
    {
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
        if (!rolling)
        {
            rb.velocity = new Vector3(movement.x * speed, rb.velocity.y);
            if (groundDetector.grounded && wantToRoll)
            {
                rolling = true;
                timer = rollTime;
                initialRollDirection = new Vector2(rollForce * (movement.x >= 0 ? 1 : -1), 0);
            }
        }            
        else
        {
            if (timer > 0)
            {
                rb.AddForce(initialRollDirection); //TODO mess with the force addition /over time?
            }
            if(rb.velocity.x == 0)
                timer = 0;
            if (timer < -rollTime)
            {                
                rolling = false;
                wantToRoll = false;
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
        //if (callbackContext.canceled)
           // wantToRoll = false;
    }
}
