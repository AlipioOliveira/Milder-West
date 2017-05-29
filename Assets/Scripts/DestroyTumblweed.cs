using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTumblweed : MonoBehaviour 
{
    private GameObject[] Children;
    private float newAlpha = 0f;

    [SerializeField]
    [Range(0f, 10f)]
    private float timeToStartFading = 1f;
    [SerializeField]
    [Range(0.1f, 10f)]
    private float fadeDuration = 5f;

    private bool active = false;

    void Start () 
	{
		
	}
	
	void Update () 
	{
        if (active)
        {
            timeToStartFading -= Time.deltaTime;

            if (newAlpha < 0.99 && timeToStartFading <= 0f)
                newAlpha += (1f / fadeDuration) * Time.deltaTime;
            else if (newAlpha >= 0.99) Destroy(gameObject);

            MeshRenderer renderer = this.GetComponent<MeshRenderer>();
            Color originalColour = renderer.material.color;
            renderer.material.color = new Color(originalColour.r, originalColour.g, originalColour.b, 1 - newAlpha);
        }                
    }

    public void setValues(float _timeToStartFading, float _fadeDuration)
    {
        timeToStartFading = _timeToStartFading;
        fadeDuration = _fadeDuration;
        active = true;
    }
}
