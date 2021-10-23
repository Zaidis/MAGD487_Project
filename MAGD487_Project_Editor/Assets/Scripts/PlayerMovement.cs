using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    Vector2 movement;
    [SerializeField]
    float speed;

    [Space][SerializeField] GroundDetector groundDetector; //roll elements
    private bool wantToRoll = false;
    private bool rolling = false;
    [SerializeField] float rollForce;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y);

        if (groundDetector.grounded && wantToRoll && !rolling)
        { //add timer, adjust force manually, cancel player input left and right, timer
            rolling = true;
            rb.AddForce(new Vector2(rollForce * (movement.x >= 0 ? 1 : -1), 0), ForceMode2D.Impulse);
            Debug.Log(rb.velocity.x);
        }
        if (Mathf.Abs(rb.velocity.x) <= speed)//change to timer system
            rolling = false;
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        movement = new Vector2(callbackContext.ReadValue<Vector2>().x, callbackContext.ReadValue<Vector2>().y);
    }
    public void RollInput(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
            wantToRoll = true;
        if (callbackContext.canceled)
            wantToRoll = false;
    }
}
