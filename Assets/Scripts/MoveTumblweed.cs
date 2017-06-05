using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTumblweed : MonoBehaviour 
{
    Rigidbody rb;

    public Vector3 direction;
    public float minPos = 5f;

    private bool destroy = false;

    public InAudioEvent TumbleweedEvents;

    public float TimeToDestroy = 20f;

	void Start () 
	{
        TimeToDestroy += Time.time;
        GetComponent<DestroyTumblweed>().enabled = false;
        rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () 
	{
        rb.AddForce(direction * Time.fixedDeltaTime, ForceMode.Force);
        if ((transform.position.x >= minPos && !destroy) || (TimeToDestroy <= Time.time && !destroy))
        {
            destroy = true;
            GetComponent<DestroyTumblweed>().enabled = true;
            GetComponent<DestroyTumblweed>().setValues(1f, 3f);           
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        InAudio.PostEvent(gameObject, TumbleweedEvents);
    }
    private void OnDestroy()
    {
        InAudio.StopAll(gameObject);
    }
}
