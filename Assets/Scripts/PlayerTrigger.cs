using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    private bool inside = false;

    private void OnTriggerEnter(Collider other)
    {
        //inside = (other.tag == "Player");
        if (other.tag == "Player")
        {
            inside = true;
            ExplodingManager.instancia.UpdateTrigger();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //inside = (other.tag == "Player");
        if (other.tag == "Player")
        {
            inside = false;
            ExplodingManager.instancia.UpdateTrigger();
        }        
    }

    public bool getIsPlayerInside()
    {
        return inside;
    }
}

