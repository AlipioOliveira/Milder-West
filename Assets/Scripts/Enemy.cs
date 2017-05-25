using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{

    public GameObject deadPrefab;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

    public void Kill()
    {
        GameObject dead = Instantiate(deadPrefab);
        spawnDeadPrefab(transform, dead.transform);
        Destroy(gameObject);
    }

    private void spawnDeadPrefab(Transform player, Transform dead)
    {
        for (int i = 0; i < player.childCount; i++)
        {
            dead.transform.GetChild(i).gameObject.transform.position = player.GetChild(i).gameObject.transform.position;
            dead.transform.GetChild(i).gameObject.transform.rotation = player.GetChild(i).gameObject.transform.rotation;
            if (player.GetChild(i).childCount > 0)
                spawnDeadPrefab(player.GetChild(i), dead.GetChild(i));
        }
    }
}
