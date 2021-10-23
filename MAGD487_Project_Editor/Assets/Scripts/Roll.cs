using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Roll : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GroundDetector groundDetector;
    public bool isGrounded;
    private bool wantToRoll = false;
    private bool rolling = false;
    private float direction;
    [SerializeField] float rollForce;
    [SerializeField] float maxSpeed;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update() {
        if (groundDetector.grounded)
            isGrounded = true;
    }
    private void FixedUpdate(){        
        if (isGrounded && wantToRoll && !rolling) { //apply force in direction of analog stick
            rolling = true;
            rb.AddForce(new Vector2(rollForce * (direction >= 0 ? 1 : -1), 0), ForceMode2D.Impulse);
            Debug.Log(rb.velocity.x);
        }
        if(Mathf.Abs(rb.velocity.x) < maxSpeed)
            rolling = false;
    }
    public void RollDirection(InputAction.CallbackContext callbackContext){
        direction = callbackContext.ReadValue<Vector2>().x;
    }
    public void RollInput(InputAction.CallbackContext callbackContext){
        Debug.Log("Roll");
        if (callbackContext.performed)
            wantToRoll = true;
        if (callbackContext.canceled)
            wantToRoll = false;
    }
}
