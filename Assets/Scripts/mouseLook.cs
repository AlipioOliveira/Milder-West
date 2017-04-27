using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour 
{
    private Vector2 mouselook;

    public float sensitivity = 2.8f;

    GameObject player;

	void Start () 
	{       
        player = this.transform.parent.gameObject;
        UnityEngine.Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	void Update () 
	{
        Vector2 mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * Time.deltaTime * sensitivity;
        
        mouselook += mouseChange;
        mouselook.y = Mathf.Clamp(mouselook.y, -90f, 90f);

        //Debug.Log(mouseChange.ToString() + " | " + mouselook.ToString());
               
        transform.localRotation = Quaternion.AngleAxis(-mouselook.y, Vector3.right);
        player.transform.localRotation = Quaternion.AngleAxis(mouselook.x, player.transform.up);

    }
}
