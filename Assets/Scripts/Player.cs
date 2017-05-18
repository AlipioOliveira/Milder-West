using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public static Player instancia;
    private PlayerControlls playerControlls;
    private NPCManager npcManager;

    private bool alive = true;

    void Start () 
	{
        npcManager = GetComponent<NPCManager>();
        playerControlls = GetComponent<PlayerControlls>();
        instancia = this;
    }
	
	void Update () 
	{
        
	}  

    public bool isAlive()
    {
        return alive;
    }
}
