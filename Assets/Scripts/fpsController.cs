using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class fpsController : MonoBehaviour 
{
    public float speed = 10.0f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public bool canJump = true;
    public float jumpHeight = 2.0f;
    private bool grounded = false;
    private bool freeze = false;   
    private Rigidbody rigidbody;

    public InAudioNode FootstepsSound;
    public float footStepTime = 0.35f;
    public float backwardsMulti = 0.6f;
    private float footStepDtime = 0;

    public InAudioNode JumpSound;
    public InAudioNode LandSound;
    public float timeForLandingSound = 0.3f;
    private float timeForLandingDSound = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.useGravity = false;
    }

    void FixedUpdate()
    {
        if (!freeze)
        {
            if (grounded)
            {
                // Calculate how fast we should be moving
                Vector3 targetVelocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                targetVelocity = transform.TransformDirection(targetVelocity);
                targetVelocity *= speed;

                if (targetVelocity != Vector3.zero && (footStepDtime >= footStepTime))
                {
                    InAudio.Play(gameObject, FootstepsSound);
                    footStepDtime = 0;
                }
                else footStepDtime += Time.fixedDeltaTime;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rigidbody.velocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

                if ((timeForLandingDSound >= 0) && timeForLandingDSound < timeForLandingSound)
                {
                    timeForLandingDSound += Time.fixedDeltaTime;
                }
                if (timeForLandingDSound >= timeForLandingSound)
                {
                    timeForLandingDSound = -1;
                    InAudio.Play(gameObject, LandSound);
                }
                // Jump
                if (canJump && Input.GetButton("Jump"))
                {
                    InAudio.Play(gameObject, JumpSound);
                    timeForLandingDSound = 0;
                    rigidbody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
                }
            }

            // We apply gravity manually for more tuning control
            rigidbody.AddForce(new Vector3(0, -gravity * rigidbody.mass, 0));

            grounded = false;
        }
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravity);
    }

    public void FreezePlayer(bool f)
    {
        freeze = f;
        if (f)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }else rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

    }
}
