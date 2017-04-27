using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverInput : MonoBehaviour
{
    //public Camera cam;
    private Animator anim;

    public float range = 100f;
    public float damage = 10f;

    public Camera camera;

    public ParticleSystem muzzle;
    public GameObject impactEffect;
    public float force = 10f;

    public int magazineSize = 6;
    private int bulletsIn;
    private bool isReloading = false;   

    void Start () 
	{
        bulletsIn = magazineSize;
        anim = GetComponent<Animator>();
	}
	
	void Update () 
	{

        if (Input.GetButtonDown("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting1") && bulletsIn > 0)
        {
            anim.SetTrigger("Shot1");            
        }
        else if (bulletsIn <= 0 && !isReloading && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting1"))
        {                              
            anim.SetTrigger("Reload");
        }
    }

    public void StartReload()
    {
        isReloading = true;
    }
    public void StopReload()
    {
        isReloading = false;
        bulletsIn = magazineSize;
    }

    public void Shoot()
    {
        bulletsIn--;
        muzzle.Play();        
        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.position - transform.parent.position , out hit, 100f))
        //    print("Found an object - name: " + hit.transform.name);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log("HIT : " + hit.transform.name);
        }        

        GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.3f);

        if (hit.rigidbody != null)
        {
            hit.rigidbody.AddForceAtPosition((hit.point - camera.transform.position).normalized * force, hit.point);
        }
    }
}
