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
            GameObject ropeR = GameObject.Instantiate(rope, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + .7f), Quaternion.identity);
            ropeR.transform.parent = gameObject.transform;
            ropeR.GetComponent<HingeJoint2D>().connectedBody = gameObject.transform.GetComponent<Rigidbody2D>();
            return;
        }
    }
}
