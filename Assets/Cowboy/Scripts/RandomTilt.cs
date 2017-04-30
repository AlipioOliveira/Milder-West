using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTilt : MonoBehaviour {
    Animator animator;
    float timer;
    int i;
    private void Start()
    {
        animator = this.GetComponent<Animator>();
        timer = animator.GetCurrentAnimatorStateInfo(0).length;
    }
    
	void Update ()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            i = Random.Range(0, 4);
            animator.SetInteger("Anim", i);            
            SetTimer();
        }
    }

    private void SetTimer()
    {
        timer = animator.GetCurrentAnimatorStateInfo(0).length;
    }
}
