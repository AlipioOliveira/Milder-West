using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlls : MonoBehaviour {

    public static PlayerControlls instancia;

    private GameObject weapon;

    private Animator animator;
    private Rigidbody rb;

    public GameObject hand;
    public GameObject prefab;

    public float movementSpeed = 1f;
    public float rotateSpeed = 1f;
    public float jumpPower = 0.5f;
    public float rotateToNpcSpeed = 2f;
    private float jumpSpeed = 1f;
    private float walkingSpeed = 0.4f;
    private bool applyUpForce = true;
    private bool isJumping = false;
    private bool isGrounded = true;
    private bool hasWeapon = false;
    private bool interacting = false;

    private Transform target;

    public GameObject deadPrefab; //TEM DE TER AS MESMAS CHILDREN

    private bool isDead = false;

    public GameObject[] Children { get; private set; }

    public InAudioNode FootstepsSound;
    public float footStepTime = 0.35f;
    public float backwardsMulti = 0.6f;
    private float footStepDtime = 0;

    public InAudioNode JumpSound;
    public InAudioNode LandSound;
    public float timeForLandingSound = 0.3f;
    private float timeForLandingDSound = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        instancia = this;
        target = this.transform;
	}

    void FixedUpdate ()
    {
        if (!interacting)
        {
            //walkingSpeed = hasWeapon ? 0.2f : 1f;
            //jumpSpeed = isJumping ? 1f : 1f;    

            float translation = Input.GetAxisRaw("Vertical") * movementSpeed * jumpSpeed * walkingSpeed * Time.fixedDeltaTime;
            float rotation = Input.GetAxisRaw("Horizontal") * rotateSpeed * jumpSpeed * Time.fixedDeltaTime;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
                transform.Rotate(0, rotation, 0);

            bool isJumping = animator.GetCurrentAnimatorStateInfo(0).IsName("jump");

            if (isJumping && translation > 0 && applyUpForce)
            {                
                applyUpForce = false;
            }
            if (!isJumping)
                applyUpForce = true;
            if (isJumping && (timeForLandingDSound < timeForLandingSound))
            {
                timeForLandingDSound += Time.fixedDeltaTime;
            }
            if (timeForLandingDSound >= timeForLandingSound)
            {
                timeForLandingDSound = 0;
                InAudio.Play(gameObject, LandSound);                
            }

            if (translation > 0 && !isJumping && !animator.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                transform.Translate(0, 0, translation);
                if (footStepDtime >= footStepTime)
                {
                    InAudio.Play(gameObject, FootstepsSound);
                    footStepDtime = 0;
                }
                else footStepDtime += Time.fixedDeltaTime;                                

                animator.SetBool("isRunning", true);
                animator.SetBool("isRunningBack", false);
            }
            else if (translation < 0 && !isJumping && !animator.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                transform.Translate(0, 0, translation);
                if (footStepDtime >= footStepTime * backwardsMulti)
                {
                    InAudio.Play(gameObject, FootstepsSound);
                    footStepDtime = 0;
                }
                else footStepDtime += Time.fixedDeltaTime;                
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningBack", true);
            }
            else
            {                
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningBack", false);
                footStepDtime = 0;
            }           
            if (Input.GetButtonDown("Jump") && !isJumping && translation >= 0)
            {
                InAudio.Play(gameObject, JumpSound);
                timeForLandingDSound = 0;
                animator.SetTrigger("isJumping");
            }            
        }
        else if (interacting)
        {
            Vector3 direction = target.position - transform.position;
            direction.y = 0;
            float rotSpeed = rotateToNpcSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotSpeed, 0.0F);           
            transform.rotation = Quaternion.LookRotation(newDirection);
        }      
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

    public void startInteraction(Transform _target, bool locked)
    {
        ThirdPersonCamera.instacia.StartInteraction(_target);
        target = _target;
        interacting = locked;
        animator.SetBool("hasWeapon", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isRunningBack", false);
    }  
    
    public void StopIteraction()
    {
        interacting = false;
        ThirdPersonCamera.instacia.StopInteraction();
    }     
}
