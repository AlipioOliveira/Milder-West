using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigame2Npc : MonoBehaviour
{
    public GameObject deadPrefab;

    public float timeToShoot = 3;
    public float timeToKill = 0.2f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        npcSoundScript.instancia.UpdatePosition(transform.position);
    }

    void Update()
    {
        
    }

    public void StartMinigame()
    {        
        StartCoroutine(WaitToShoot(timeToShoot));
        StartCoroutine(WaitToKill(timeToShoot + timeToKill));
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

    IEnumerator WaitToShoot(float time)
    {        
        yield return new WaitForSeconds(time);
        anim.SetTrigger("Shoot");
        anim.SetBool("isWalking", false);
    }
    IEnumerator WaitToKill(float time)
    {
        yield return new WaitForSeconds(time);                
        Minigame2Manager.instancia.NpcWon();//kills player
        npcSoundScript.instancia.PlayShootSound();
    }
}
