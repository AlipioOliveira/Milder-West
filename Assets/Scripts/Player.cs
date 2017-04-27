using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public static Player instancia;
    private PlayerControlls playerControlls;
    private NPCManager npcManager;

    void Start () 
	{
        npcManager = GetComponent<NPCManager>();
        playerControlls = GetComponent<PlayerControlls>();
        instancia = this;
    }
	
	void Update () 
	{
        
	}  

    public void addToList(GameObject obj)
    {
        Debug.Log("add - " + obj.tag);
        npcManager.addNewNPC(obj);
    }
}
