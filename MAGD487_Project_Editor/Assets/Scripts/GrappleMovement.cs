using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    bool deactivated = false;
    [SerializeField] bool moveRightVector = false;
    [SerializeField] GameObject rope;
    private void Start()
    {
        if (moveRightVector)
            rb.velocity = speed * transform.right;
        else
            rb.velocity = new Vector2(speed * this.transform.localScale.x, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            deactivated = true;
            rb.velocity = Vector3.zero;
            //rb.simulated = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            rb.interpolation = RigidbodyInterpolation2D.None;
            GameObject.Instantiate(rope, gameObject.transform);
            return;
        }
    }
}
