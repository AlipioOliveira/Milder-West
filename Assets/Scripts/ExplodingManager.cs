using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingManager : MonoBehaviour 
{
    public static ExplodingManager instancia;

    public List<GameObject> objects;
    private List<int> NpcIndexes;
    public GameObject npc;
    public GameObject player;
    private int ExplosiveIndex;

    private int roud = 0;

	void Start () 
	{
        ExplosiveIndex = Random.Range(0, objects.Count);
        NpcIndexes = new List<int>();
        for (int i = 0; i < objects.Count; i++)
            NpcIndexes.Add(i);              
        instancia = this;        
        NextRound();
	}
	
	void Update () 
	{
        
    }

    public void NextRound()
    {
        roud++;
        if (roud % 2 == 0) //even
        {
            int index = Random.Range(0, NpcIndexes.Count);            
            if (NpcIndexes[index] == ExplosiveIndex)            
                npc.GetComponent<ExplodingNpc>().isTurn(true, objects[NpcIndexes[index]]);            
            else            
                npc.GetComponent<ExplodingNpc>().isTurn(false, objects[index]);
            NpcIndexes.Remove(index);
        }            
        if (roud % 2 == 1) //odd
            player.GetComponent<ExplodingPlayer>().IsTurn();
    }

    private void giveTarguetToNpc()
    {
        
    }

    public void Winner()
    {
        if (roud % 2 == 0) //even        
            Debug.Log("PlayerWon!!");        
        if (roud % 2 == 1) //odd
            Debug.Log("NPCWon!!");
    }
}
