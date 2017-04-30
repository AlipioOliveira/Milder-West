using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingNpc : MonoBehaviour 
{

    private GameObject target;

    private bool moveTwordsTarguet = false;

    private Vector3 originalPos;    
    public float speed = 2;
    private bool isDead = false;
    private bool turn = false;

    private Animator anim;
    private float rotateSpeed = 3f;

    void Start () 
	{
        originalPos = transform.position;
        anim = GetComponent<Animator>();
	}
	
	void Update () 
	{
        if (turn)
        {
            if (moveTwordsTarguet)
            {
                Vector3 direction = target.transform.position - transform.position;
                if (direction.magnitude > 0.4f)
                {
                    direction.y = 0;
                    direction.Normalize();
                    transform.position += direction * speed * Time.deltaTime;
                    anim.SetBool("isWalking", true);
                }
                else Shoot();
                rotateTwords(target.transform);
            }
            else
            {
                Vector3 direction = originalPos - transform.position;
                if (direction.magnitude > 0.4f)
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
        else
        {
            anim.SetBool("isWalking", false);         
        }          
                
    }

    public void isTurn(bool isGonnaDie, GameObject t)
    {
        moveTwordsTarguet = true;
        turn = true;
        isDead = isGonnaDie;
        target = t;
    }

    private void Stop()
    {
        Debug.Log("Stoped");
        turn = false;
        ExplodingManager.instancia.NextRound();
    }

    private void Shoot()
    {
        Debug.Log("SHOOT!!");
        anim.SetTrigger("Shoot");
        if (isDead)
        {
            ExplodingManager.instancia.objects.Remove(target);
            ExplodingManager.instancia.Winner();
            Destroy(target.gameObject,0f);
        }       
        moveTwordsTarguet = false;
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
