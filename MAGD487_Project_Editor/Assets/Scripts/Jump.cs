using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Jump : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float jumpPower;
    [SerializeField] GroundDetector groundDetector;
    bool wantToJump;
    public bool wasGrounded;
    bool jumping;
    [SerializeField] float jumpLength = 2;
    float timer = 0;
    [SerializeField] float gravityMultiplier = 2;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (groundDetector.grounded)
        {
            wasGrounded = true;
        }

    }

    private void FixedUpdate()
    {
        JumpCharacter();
    }

    public void OnJump(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        { 
            wantToJump = true;
        }
        if (callbackContext.canceled)
        {
            wantToJump = false;
        }
    }

    void JumpCharacter()
    {
        if (wasGrounded && wantToJump)
        {
            jumping = true;
        }

        if (jumping)
        {
            wasGrounded = false;
            timer += Time.fixedDeltaTime;
            if (timer < jumpLength)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            }

            else
            {
                jumping = false;
                timer = 0;
            }
        }

        if(!jumping || !wantToJump)
        {
            rb.AddForce(Physics2D.gravity * gravityMultiplier);
            jumping = false;
            timer = 0;
        }
    }
}
