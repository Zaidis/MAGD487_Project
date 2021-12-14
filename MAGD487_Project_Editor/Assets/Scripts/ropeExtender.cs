using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeExtender : MonoBehaviour
{
    [SerializeField] GameObject ropeLink;
    private RaycastHit2D rh;
    void Start()
    {
        rh = Physics2D.Raycast(gameObject.transform.position, Vector2.down);
        if(rh.distance > 2.5f)
        {            
            GameObject nextLink = GameObject.Instantiate(ropeLink, gameObject.transform.position - new Vector3(0f, -1.25f, 0f), Quaternion.identity);
            nextLink.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
            nextLink.transform.parent = gameObject.transform;
        }    
        gameObject.GetComponent<ropeExtender>().enabled = false;
    }
}
