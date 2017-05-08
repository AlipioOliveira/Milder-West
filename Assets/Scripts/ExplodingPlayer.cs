using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlayer : MonoBehaviour 
{
    private Rigidbody rb;

    private fpsController controller;

    private bool isTurn = false;    

    private Vector3 originalPos;

    public Camera playerCamera;

	void Start () 
	{
        originalPos = transform.position;
        controller = GetComponent<fpsController>();
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () 
	{
        if (isTurn)
        {
            
        }
        else
        {
            controller.FreezePlayer(true);
        }
	}

    public void IsTurn()
    {
        controller.FreezePlayer(false);
        isTurn = true;
    }
}
