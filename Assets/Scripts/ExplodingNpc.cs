using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingNpc : MonoBehaviour 
{

    private GameObject target;

    private bool moveTwordsTarguet = false;

    private Vector3 originalPos;
    private Vector3 lookTwords;   
    public float speed = 2;
    private bool isDead = false;
    private bool turn = false;   

    private Animator anim;
    private float rotateSpeed = 3f;

    public GameObject deadPrefab;

    void Start () 
	{
        originalPos = transform.position;
        lookTwords = originalPos - transform.right;
        anim = GetComponent<Animator>();
	}
	
	void Update () 
	{        
        if (turn)
        {            
            if (moveTwordsTarguet)
            {
                Vector3 direction = target.transform.position - transform.position;
                if (direction.magnitude > 2f)
                {
                    Walk(direction);
                }
                else Shoot();
                rotateTwords(target.transform);
            }
            else if(!moveTwordsTarguet && !anim.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                Vector3 direction = originalPos - transform.position;
                if (direction.magnitude > 0.5f)
                {
                    Walk(direction);
                }
                else Stop();
                rotateTwords(originalPos);
            }
        }
        else if(!turn)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            anim.SetBool("isWalking", false);
        }
        else
        {
            if (target == null)
            {
                anim.SetBool("isWalking", false);
                return;
            }
        }     
    }

    private void Walk(Vector3 direction)
    {
        direction.y = 0;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
        anim.SetBool("isWalking", true);
    }

    public void isTurn(bool isGonnaDie, GameObject t)
    {
        target = t;
        moveTwordsTarguet = true;
        turn = true;
        isDead = isGonnaDie;        
    }

    private void Stop()
    {
        Debug.Log("Stoped");
        turn = false;
        ExplodingManager.instancia.NextRound();
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

    private void Shoot()
    {
        anim.SetTrigger("Shoot");        
        if (isDead)
        {          
            Instantiate(ExplodingManager.instancia.ExplosionPrefab, target.transform.position, target.transform.rotation);                   
            GameObject dead = Instantiate(deadPrefab);
            spawnDeadPrefab(transform, dead.transform);
            ExplodingManager.instancia.HasWinner();           
            Destroy(gameObject);
        }
        target.GetComponent<BreakOnColision>().Break();
        ExplodingManager.instancia.objects.Remove(target);                
        moveTwordsTarguet = false;
        anim.SetBool("isWalking", false);
    }
    private void rotateTwords(Transform _target)
    {
        Vector3 direction = _target.position - transform.position;
        direction.y = 0;
        float rotSpeed = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotSpeed, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    private void rotateTwords(Vector3 _target)
    {
        Vector3 direction = _target - transform.position;
        direction.y = 0;
        float rotSpeed = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotSpeed, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
