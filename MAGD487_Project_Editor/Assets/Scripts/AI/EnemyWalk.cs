using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    [SerializeField] float jumpStrength = 10f;
    [SerializeField] float walkSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float wallDetectionRayDistance = 2;
    [SerializeField] Vector3 wallDetectionOffset;
    [SerializeField] float jumpDelay = 0.75f;
    float timer = 0;
    Rigidbody2D rb;
    Transform playerPos;
    GroundDetector groundDetector;
    PlayerDetector playerDetector;
    Vector2 flippedScale, unFlippedScale;
    private void Awake()
    {
        playerDetector = GetComponentInChildren<PlayerDetector>();
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
       if (playerPos != null && playerDetector.detected)
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
            hit = Physics2D.Raycast(this.transform.position + wallDetectionOffset, transform.right * wallDetectionRayDistance, 1, groundLayer);
        else
            hit = Physics2D.Raycast(this.transform.position + wallDetectionOffset, -transform.right * wallDetectionRayDistance, 1, groundLayer);

        timer += Time.deltaTime;
        if (hit.collider != null && timer >= jumpDelay)
        {
            //Theres a wall so try and jump
            Jump();
            timer = 0;
        }
    }
    void Jump()
    {
        //rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        rb.AddForce(new Vector2(0, jumpStrength));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position + wallDetectionOffset, transform.right * wallDetectionRayDistance);
        Gizmos.DrawRay(this.transform.position + wallDetectionOffset, -transform.right * wallDetectionRayDistance);
    }

    public Rigidbody2D GetRigidbody2D() { return rb; }
}
