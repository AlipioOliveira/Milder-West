using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour 
{

    public Transform target;
    public float speed = 3;

    void Start () 
	{
		
	}
	
	void Update () 
	{
        transform.LookAt(target);
        transform.RotateAround(target.transform.position, Vector3.up, Time.deltaTime * speed);
	}
}
