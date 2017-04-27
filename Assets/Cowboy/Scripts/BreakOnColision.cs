using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BreakOnColision : MonoBehaviour {

    public GameObject prefab;
    [Range(0f,10f)]
    public float strength = 1f;

    private bool isBroken = false;
    void OnCollisionEnter(Collision collision)
    {
        if ((collision.relativeVelocity.magnitude > 6f * strength) && !isBroken)
        {
            isBroken = true;
            Instantiate(prefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
                
    }
}
