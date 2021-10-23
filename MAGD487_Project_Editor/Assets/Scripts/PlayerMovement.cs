using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour{
    Rigidbody2D rb;
    public Vector2 movement;
    [SerializeField]
    float speed;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
   
    // Update is called once per frame
    void Update()
    {
        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        movement = new Vector2(callbackContext.ReadValue<Vector2>().x, callbackContext.ReadValue<Vector2>().y);
    }

}
