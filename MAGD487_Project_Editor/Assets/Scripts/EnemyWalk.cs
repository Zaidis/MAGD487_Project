using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    [SerializeField] float engageDistance = 10f;
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float walkSpeed;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    Transform playerPos;
    GroundDetector groundDetector;
    Vector2 flippedScale, unFlippedScale;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetector = GetComponentInChildren<GroundDetector>();
    }
    // Start is called before the first frame update
    void Start()
    {
        unFlippedScale = transform.localScale;
        flippedScale = new Vector2(-unFlippedScale.x, unFlippedScale.y);
        //TODO OPTIMISE THIS TO FIND GAME MANAGER INSTEAD
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       if (Vector2.Distance(this.transform.position, playerPos.position) < engageDistance)
       {
            //Start Chase
            if(groundDetector.grounded)
                CheckForWall();
            Chase();
       }
    }

    void Chase()
    {
        Vector2 dir = playerPos.position - this.transform.position;
        dir.Normalize();
        if (dir.x > 0)
            transform.localScale = unFlippedScale;
        else if (dir.x < 0)
            transform.localScale = flippedScale;
        rb.velocity = new Vector2(dir.x * walkSpeed, rb.velocity.y);

    }
    void CheckForWall()
    {
        RaycastHit2D hit;
        if (transform.localScale.x > 0)
            hit = Physics2D.Raycast(this.transform.position, transform.right, 1, groundLayer);
        else
            hit = Physics2D.Raycast(this.transform.position, -transform.right, 1, groundLayer);

        if (hit.collider != null)
        {
            //Theres a wall so try and jump
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
    }
}
