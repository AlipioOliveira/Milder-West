using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstPersonController : MonoBehaviour 
{
    private float speed = 10f;

	void Start () 
	{
		
	}
	
	void Update () 
	{        
        float translation = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        float strafe = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;

        transform.Translate(strafe, 0, translation);
    }
}
