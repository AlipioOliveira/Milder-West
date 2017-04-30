using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingPlayer : MonoBehaviour 
{
    private Rigidbody rb;

    private fpsController controller;

    private bool isTurn = false;

    private Vector3 originalPos;

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
            if (Input.GetButtonDown("Fire1"))
            {
                ExplodingManager.instancia.NextRound();
                isTurn = false;
            }
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
