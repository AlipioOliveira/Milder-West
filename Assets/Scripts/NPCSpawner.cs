using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour 
{
    public Transform[] Paths;
    public Transform[] SpawnPoints;
    public GameObject[] NpcPrefab;

    private float spawnTime;

    public float timeToSpawnMin;
    public float timeToSpawnMax;

    void Start () 
	{
        spawnTime = Random.Range(timeToSpawnMin, timeToSpawnMax);
        SpawnNpc();
    }
	
	void Update () 
	{
        if (spawnTime <= Time.time)
        {
            SpawnNpc();
        }
    }

    private void SpawnNpc()
    {
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);
        GameObject newInstance = Instantiate(NpcPrefab[Random.Range(0, NpcPrefab.Length)].gameObject, SpawnPoints[spawnPointIndex].position,
            SpawnPoints[spawnPointIndex].rotation, this.gameObject.transform);
        newInstance.GetComponent<npc>().setPath(Paths[Random.Range(0, Paths.Length)]);
        spawnTime = Time.time + Random.Range(timeToSpawnMin, timeToSpawnMax);
    }
}
