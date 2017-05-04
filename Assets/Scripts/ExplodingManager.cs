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
    public float slowness = 10f;
    public float slowDownTime = 3f;
    void Start () 
	{
        Explosive = objects[Random.Range(0, objects.Count)];
        Debug.Log(objects.IndexOf(Explosive));          
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
            ExplodingManager.instancia.SlowTime();
        }
        if (roud % 2 == 1) //odd
        {
            Debug.Log("NPCWon!!");
            Winner = 1;
            ExplodingManager.instancia.SlowTime();
        }
    }

    public GameObject getExplosive()
    {
        return Explosive;
    }

    public void SlowTime()
    {
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        Time.timeScale = 1f / slowness;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowness;

        yield return new WaitForSeconds(slowDownTime * Time.timeScale);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowness;       
    }
}
