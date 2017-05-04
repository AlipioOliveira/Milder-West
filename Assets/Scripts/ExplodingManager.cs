using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingManager : MonoBehaviour 
{
    public static ExplodingManager instancia;

    public List<GameObject> objects;       
    public GameObject npc;
    public GameObject player;
    private GameObject Explosive;

    private int roud = 0;
    private int Winner = 0;

    public GameObject ExplosionPrefab;

	void Start () 
	{
        Explosive = objects[Random.Range(0, objects.Count)];                
        instancia = this;        
        NextRound();
	}
	
	void Update () 
	{
        
    }

    public void NextRound()
    {       
        if (Winner == 0)
        {
            roud++;
            if (roud % 2 == 0) //even
            {
                player.GetComponent<fpsController>().FreezePlayer(true);
                int index = Random.Range(0, objects.Count);
                if (objects[index] == Explosive)                
                    npc.GetComponent<ExplodingNpc>().isTurn(true, objects[index]);                
                else                
                    npc.GetComponent<ExplodingNpc>().isTurn(false, objects[index]);               
            }
            if (roud % 2 == 1) //odd
            {
                player.GetComponent<ExplodingPlayer>().IsTurn();
                player.GetComponent<fpsController>().FreezePlayer(false);
            }
        }        
    }

    public void HasWinner()
    {
        if (roud % 2 == 0) //even        
        {
            Debug.Log("PlayerWon!!");
            Winner = 2;
        }
        if (roud % 2 == 1) //odd
        {
            Debug.Log("NPCWon!!");
            Winner = 1;
        }
    }

    public GameObject getExplosive()
    {
        return Explosive;
    }
}
