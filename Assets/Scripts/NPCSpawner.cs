using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class NPCSpawner : MonoBehaviour 
{
    public Transform[] Paths;
    public Transform[] SpawnPoints;
    public GameObject[] NpcPrefab;

    private float spawnTime;

    public float timeToSpawnMin;
    public float timeToSpawnMax;

    private string path;
    private string jsonString;

    //private npcData data;
    private NpcData data;

    void Start () 
	{
        ReadJsonData();
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
        npc newNpc = newInstance.GetComponent<npc>();
        newNpc.setPath(Paths[Random.Range(0, Paths.Length)]);

        int dialogueIndex = Random.Range(0, data.Dialogue.Length);

        newNpc.setProperties(data.FristName[Random.Range(0, data.FristName.Length)], data.LastName[Random.Range(0, data.LastName.Length)],
            data.Dialogue[dialogueIndex].DialogueText, data.Dialogue[dialogueIndex].minigameId);
        spawnTime = Time.time + Random.Range(timeToSpawnMin, timeToSpawnMax);
    }

    private void ReadJsonData()
    {
        path = Application.streamingAssetsPath + "/npcData.json";
        jsonString = File.ReadAllText(path);
        data = JsonMapper.ToObject<NpcData>(jsonString);
    }   
}

[System.Serializable]
public class NpcData
{
    public string[] FristName;
    public string[] LastName;
    public Dialogue[] Dialogue;
}
[System.Serializable]
public class Dialogue
{
    public int id;
    public int minigameId;
    public string[] DialogueText;
}