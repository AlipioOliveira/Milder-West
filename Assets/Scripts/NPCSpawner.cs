using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class NPCSpawner : MonoBehaviour 
{
    public Grid grid;
    public PathFinding find;

    public Transform[] SpawnPoints;
    public Transform[] Objectives;

    private float spawnTime;

    public float timeToSpawnMin;
    public float timeToSpawnMax;

    private string path;
    private string jsonString;

    private NpcData data;

    void Start () 
	{        
        ReadJsonData();
        spawnTime = Random.Range(timeToSpawnMin, timeToSpawnMax) + Time.time;
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
        int npcIndex = Random.Range(0, SceneTransitionManager.instancia.MenuNpcPrefab.Count);
        GameObject newInstance = Instantiate(SceneTransitionManager.instancia.MenuNpcPrefab[npcIndex].gameObject, SpawnPoints[spawnPointIndex].position,
            SpawnPoints[spawnPointIndex].rotation, this.gameObject.transform);
        npc newNpc = newInstance.GetComponent<npc>();
        //newNpc.setPath(Paths[Random.Range(0, Paths.Length)]);

        int dialogueIndex = Random.Range(0, data.Dialogue.Length); 
        
        newNpc.setProperties(data.FristName[Random.Range(0, data.FristName.Length)], data.LastName[Random.Range(0, data.LastName.Length)],
            data.Dialogue[dialogueIndex].DialogueText, data.Dialogue[dialogueIndex].minigameId, npcIndex);

        int objIndex = Random.Range(0, Objectives.Length);

        find.FindPath(newNpc.transform.position, Objectives[objIndex].position);
        List<Node> p = grid.path;
        find.FindPath(Objectives[objIndex].position, newNpc.transform.position);
        p.AddRange(grid.path);
        newNpc.setPath(p);    
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
    //public int id;
    public int minigameId;
    public string[] DialogueText;
}