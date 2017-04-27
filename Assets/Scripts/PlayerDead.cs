using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour 
{

    public Transform DeadPrefab;

    private bool a = true; 

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && a)
        {
            KillPlayer();
            a = false;
        }
        else if (!Input.GetKeyDown(KeyCode.K))
        {
            a = true;
        }
    }

    public void KillPlayer()
    {
        Instantiate(DeadPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

	
}
