using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m2RevolverInput : RevolverInput 
{
    protected override void Start()
    {
        setWeaponStatus(false);
        StartCoroutine(WaitToDraw(4f));
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    IEnumerator WaitToDraw(float time)
    {
        yield return new WaitForSeconds(time);
        setWeaponStatus(true);
        Minigame2Manager.instancia.npc.GetComponent<Minigame2Npc>().StartMinigame();
    }

    protected override void Shoot()
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
            else if (hit.transform.tag == "Enemy")
            {
                Minigame2Manager.instancia.PlayerWon();
                hit.transform.gameObject.GetComponent<Minigame2Npc>().Kill();
            }
        }
    }
}
