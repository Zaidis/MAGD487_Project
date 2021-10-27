using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
{
    public Vector3 currentDirection = -Vector3.up;
    private void Awake()
    {
        this.transform.position = new Vector3(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y)) + (transform.localScale / 2);
    }
    // Start is called before the first frame update
    void Start()
    {
       
        WaterFlow.instance.AssignParticle(this.transform);
    }

}
