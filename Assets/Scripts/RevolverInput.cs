﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private Vector3 inacuracy;

    public Rigidbody PlayerRb;

    public Image crosshair;
    public bool breakObjectOnCollision = false;

    void Start()
    {
        bulletsIn = magazineSize;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (canFire() && Input.GetButtonDown("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting1") && bulletsIn > 0)                  
            anim.SetTrigger("Shot1");        
        else if (bulletsIn <= 0 && !isReloading && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting1"))        
            anim.SetTrigger("Reload");        
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

    private bool canFire()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward + inacuracy, out hit, range))
        {
            if (hit.transform.tag == "NPC")
            {
                crosshair.color = Color.red;
                return false;
            }
            else
            {
                crosshair.color = Color.green;
                return true;
            }            
        }
        return true;
    }

    public void Shoot()
    {
        inacuracy = new Vector3(Random.Range(-0.1f, 0.1f) * PlayerRb.velocity.x, Random.Range(-0.1f, 0.1f) * PlayerRb.velocity.y, Random.Range(-0.1f, 0.1f) * PlayerRb.velocity.z);
        bulletsIn--;
        muzzle.Play();
        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, transform.position - transform.parent.position , out hit, 100f))
        //    print("Found an object - name: " + hit.transform.name);

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward + inacuracy, out hit, range))
        {           
            crosshair.color = Color.red;

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.3f);

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition((hit.point - camera.transform.position).normalized * force, hit.point);
                hit.transform.parent = null;
            }
            if (breakObjectOnCollision && hit.transform.tag == "Breakable")
            {
                hit.transform.GetComponent<BreakOnColision>().Break();
            }
        }        
    }
}
