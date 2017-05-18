using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour 
{
    public float rotateSpeed = 5f;    

    private Transform playerT;

    private bool rotateToPlayer = false;

    public Transform[] path;
    private int pathID = 0;
    private Vector3 direction;
    public float speed = 2f;

    private Animator animator;

    private bool col;

    private string name;
    private string[] dialogue;
    private int minigameId;

    void Start () 
	{
        NPCManager.instancia.addNewNPC(this.gameObject);
        animator = GetComponent<Animator>();
        playerT = transform;                
        animator.SetBool("isWalking", true);
    }
	    
	void Update () 
	{
        if (rotateToPlayer)        
            rotateTwords(playerT);        
        else
        {            
            if ((path[pathID].position - transform.position).magnitude <= 0.2f)
            {
                if (pathID + 1 >= path.Length)
                {
                    Destroy(this.gameObject, 1f);
                    pathID = 0;
                }
                else pathID++;
                
                UpdateDirection();                
            }
            if (!col)            
                transform.position += direction * speed * Time.deltaTime;                
            rotateTwords(path[pathID]);
        }
    }

    private void UpdateDirection()
    {
        direction = path[pathID].position - transform.position;
        direction.y = 0;
        direction.Normalize();
    }

    private void rotateTwords(Transform _target)
    {
        Vector3 direction = _target.position - transform.position;
        direction.y = 0;
        float rotSpeed = rotateSpeed * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotSpeed, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    public void RotateTwordsPlayer(Transform t)
    {
        playerT = t;
        rotateToPlayer = true;
        DialogueManager.instancia.AddNewDialogue(dialogue, name, this.gameObject);
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", false);
    }

    public void StopRotating()
    {
        rotateToPlayer = false;
        if (!col)
        {
            animator.SetBool("isWalking", true);
        }        
    }

    public void setPath(Transform _path)
    {
        path = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)        
           path[i] = _path.GetChild(i).transform; 
         
        UpdateDirection();
    }
     
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            col = true;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            col = false;
            animator.SetBool("isWalking", true);
        }
    }

    public void setProperties(string firstName, string lastName, string[] _dialogue, int mId)
    {
        name = firstName +" " +lastName;
        dialogue = _dialogue;
        minigameId = mId;
    }
    private void OnDestroy()
    {
        NPCManager.instancia.removeNpc(this.gameObject);
    }
}

