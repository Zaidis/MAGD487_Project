using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float maxDistanceBeforeTeleporting = 10;

    private void Awake()
    {
        transform.SetParent(null);
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, target.position, speed * Time.fixedDeltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        if(Vector2.Distance(this.transform.position, target.position) > maxDistanceBeforeTeleporting)
        {
            //Tp to them
            transform.position = transform.position = new Vector3(target.position.x, target.position.y, -10);
        }
    }
}
