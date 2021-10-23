using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Roll : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GroundDetector groundDetector;
    public bool wasGrounded;
    

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }
    void Update() {
        if (groundDetector.grounded)
            wasGrounded = true;
    }
}
