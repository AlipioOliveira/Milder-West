﻿using System.Collections;
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
                    direction.y = 0;
                    direction.Normalize();
                    transform.position += direction * speed * Time.deltaTime;
                    anim.SetBool("isWalking", true);
                }
                else Shoot();
                rotateTwords(target.transform);
            }
            else if(!moveTwordsTarguet && !anim.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                Vector3 direction = originalPos - transform.position;
                if (direction.magnitude > 0.5f)
                {
                    direction.y = 0;
                    direction.Normalize();
                    transform.position += direction * speed * Time.deltaTime;
                    anim.SetBool("isWalking", true);
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

    private void Shoot()
    {
        anim.SetTrigger("Shoot");        
        if (isDead)
        {          
            Instantiate(ExplodingManager.instancia.ExplosionPrefab, target.transform.position, target.transform.rotation);
            ExplodingManager.instancia.HasWinner();
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
