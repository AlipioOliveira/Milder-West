using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTumblweed : MonoBehaviour 
{
    Rigidbody rb;

    public Vector3 direction;
    public float minPos = 5f;

	void Start () 
	{
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () 
	{
        rb.AddForce(direction * Time.fixedDeltaTime, ForceMode.Force);
        if (transform.position.x >= minPos)
        {
            GetComponent<DestroyTumblweed>().setValues(0, 1f);
        }
    }
}
