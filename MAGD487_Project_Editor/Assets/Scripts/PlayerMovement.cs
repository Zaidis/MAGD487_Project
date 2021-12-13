using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    public Vector2 movement;
    [SerializeField]
    float speed;
    public bool canMove;

    private GroundDetector groundDetector; //roll elements
    private bool wantToRoll = false;
    public bool rolling = false;
    [Space]
    [SerializeField] private float rollForce;
    [SerializeField] private float rollTime;
    private float timer = 0;
    private Vector2 initialRollDirection;
    SpriteRenderer sr;
    public static PlayerMovement instance;
    void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        instance = this;
        canMove = true;
        groundDetector = GetComponentInChildren<GroundDetector>();
        rb = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
        movement = movement.normalized;
        if (rolling)
            timer -= Time.deltaTime;

    }
    private void FixedUpdate()
    {
        if(canMove) {
            if(!rolling) {
                rb.velocity = new Vector3(movement.x * speed, rb.velocity.y);
                if(groundDetector.grounded && wantToRoll) {
                    rolling = true;
                    timer = rollTime;
                    initialRollDirection = new Vector2(rollForce * (sr.flipX == false ? 1 : -1), rb.velocity.y);
                }
            } else {
                if (timer > 0)
                    rb.velocity = initialRollDirection;
                if(rb.velocity.x == 0)
                    timer = 0;
                if(timer < -rollTime) {
                    rolling = false;
                    wantToRoll = false;
                }
            }
        } else {
            rb.velocity = new Vector2(movement.x * 0, rb.velocity.y);
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
}
