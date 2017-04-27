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

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        instancia = this;
        target = this.transform;
	}

    //private void Update()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 0.1f), out hit))
    //    {
    //        if (hit.transform.tag != this.tag)
    //        {
    //            Debug.DrawLine(hit.transform.position, transform.position, Color.red);
    //            Debug.Log(hit.transform.tag);
    //        }
    //        //Debug.DrawLine(ray.origin, hit.point);

    //        //if (hit.tra == player.gameObject)
    //        //{
    //        //    Debug.Log("Player is above me");
    //        //    // player is directly above this tile
    //        //    pausePlayer = true;
    //        //}
    //    }
    //}

    void FixedUpdate ()
    {
        if (!interacting)
        {
            walkingSpeed = hasWeapon ? 0.2f : 1f;
            //jumpSpeed = isJumping ? 1f : 1f;    

            float translation = Input.GetAxisRaw("Vertical") * movementSpeed * jumpSpeed * walkingSpeed * Time.fixedDeltaTime;
            float rotation = Input.GetAxisRaw("Horizontal") * rotateSpeed * jumpSpeed * Time.fixedDeltaTime;

            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
                transform.Rotate(0, rotation, 0);

            bool isJumping = animator.GetCurrentAnimatorStateInfo(0).IsName("jump");

            if (isJumping && translation > 0 && applyUpForce)
            {
                //rb.velocity = new Vector3(0, 5 * jumpPower * Time.fixedDeltaTime, 0);
                applyUpForce = false;
            }
            if (!isJumping)
                applyUpForce = true;

            if (translation > 0 && !isJumping && !animator.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                transform.Translate(0, 0, translation);
                animator.SetBool("isRunning", true);
                animator.SetBool("isRunningBack", false);
            }
            else if (translation < 0 && !isJumping && !animator.GetCurrentAnimatorStateInfo(0).IsName("shooting"))
            {
                transform.Translate(0, 0, translation);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningBack", true);
            }
            else
            {
                //rb.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetBool("isRunning", false);
                animator.SetBool("isRunningBack", false);
            }

            //if (Input.GetKeyDown(KeyCode.Alpha1))
            //{
            //    hasWeapon = !hasWeapon;
            //    animator.SetBool("hasWeapon", hasWeapon);
            //    animator.SetBool("isRunning", false);
            //    animator.SetBool("isRunningBack", false);
            //    if (hasWeapon)
            //        weapon = Instantiate(prefab, hand.transform, false);
            //    else Destroy(weapon);
            //}
            if (hasWeapon && Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("FIRE!!");
                animator.SetTrigger("Shoot");
                weapon.GetComponent<RevolverInput>().Shoot();
            }

            if (Input.GetButtonDown("Jump") && !isJumping && translation >= 0)
            {
                //Debug.Log("Jump");
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
