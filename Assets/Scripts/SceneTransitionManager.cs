﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour 
{
    public static SceneTransitionManager instancia;

    //Nao esquecer de fazer com que os npcs iguais tenham o mesmo index nas listas de cada minigame
    [Header("MenuNpcs")]
    [Tooltip("Prefabs/Npcs")]
    public List<GameObject> MenuNpcPrefab;
    [Header("Minigame1 Npcs")]
    [Tooltip("Prefabs/Npcs/NpcExplosiveMinigame")]
    public List<GameObject> Minigame1Prefabs;
    [Header("Minigame2 Npcs")]
    [Tooltip("Prefabs/Npcs/Minigame2Npcs")]
    public List<GameObject> Minigame2Prefabs;

    private string CurrentNpcName = "";
    private int currentNpcIndex = 0;

    private int currentSceneIndex = 0;

    private void Awake()
    {
        instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    public void setNpcName(string name)
    {
        CurrentNpcName = name;
    }

    public void setCurrentSceneIndex(int sceneIndex)
    {
        currentSceneIndex = sceneIndex;
    }
    public int getNpcIndex()
    {
        return currentNpcIndex;
    }
    public string getNpcName()
    {
        return CurrentNpcName;
    }
    public void setNpcIndex(int index)
    {
        currentNpcIndex = index;   
    }
}
