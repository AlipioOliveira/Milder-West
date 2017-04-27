using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTumbleWeed : MonoBehaviour 
{
    public Transform[] spawnPoints;

    public float timeToSpawnMin;
    public float timeToSpawnMax;

    public GameObject prefab;

    private float[] spawnTime;

	void Start () 
	{
        spawnTime = new float[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnTime[i] = Random.Range(timeToSpawnMin, timeToSpawnMax);
        }
	}
	
	void Update () 
	{

        for (int i = 0; i < spawnTime.Length; i++)
        {
            if (spawnTime[i] <= Time.time)
            {
                GameObject newInstance = Instantiate(prefab, spawnPoints[i].position, spawnPoints[i].rotation, this.gameObject.transform);
                newInstance.GetComponent<MoveTumblweed>().direction = new Vector3(500, 0, Random.Range(-200, 200));
                newInstance.GetComponent<DestroyTumblweed>().setValues(20f, 5f);               
                spawnTime[i] = Time.time + Random.Range(timeToSpawnMin, timeToSpawnMax);
            }
        }
    }
}
