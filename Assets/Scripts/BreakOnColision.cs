using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BreakOnColision : MonoBehaviour {

    public GameObject prefab;
    [Range(0f,10f)]
    public float strength = 1f;

    private bool isBroken = false;

    public InAudioNode BreakSound;

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.relativeVelocity.magnitude > 6f * strength) && !isBroken) 
            Break();                        
    }

    public void Break()
    {
        isBroken = true;
        GameObject broken = Instantiate(prefab, transform.position, transform.rotation);
        InAudio.Play(broken, BreakSound);
        Destroy(gameObject);
    }

    public void Break(Vector3 force, Vector3 pos)
    {
        isBroken = true;
        GameObject a = Instantiate(prefab, transform.position, transform.rotation);
        InAudio.Play(a, BreakSound);
        if (a.transform.childCount > 0)
        {
            for (int i = 0; i < a.transform.childCount; i++)
            {
                a.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(force,ForceMode.Force);
            }
        }
        else
        {
            a.transform.GetComponent<Rigidbody>().AddForce(force, ForceMode.Force);
        }

        Destroy(gameObject);
    }
}
