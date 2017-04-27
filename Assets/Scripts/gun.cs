using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour 
{
    public float damage = 10;
    public float range = 100;

    public Camera camera;
	void Start () 
	{
		
	}
	
	void Update () 
	{
        if (Input.GetButtonDown("Fire 1"))
        {
            shoot();
        }
	}

    private void shoot()
    {
        RaycastHit hit;
        Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range);
    }
}
