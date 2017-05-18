using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodingMinigameRevolver : MonoBehaviour 
{
    public static ExplodingMinigameRevolver instacia;

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
    private bool canUseWeapon = false;

    private Vector3 inacuracy;

    public Rigidbody PlayerRb;

    public Image crosshair;
    public bool breakObjectOnCollision = false;

    public GameObject deadPrefab;

    void Start()
    {
        bulletsIn = magazineSize;
        anim = GetComponent<Animator>();
        instacia = this;
    }

    void Update()
    {

            if (canFire() && canUseWeapon && Input.GetButtonDown("Fire1") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting1") && bulletsIn > 0)
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

    public void setWeaponStatus(bool state)
    {
        canUseWeapon = state;
        anim.SetBool("Down", !state);
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
        else
        {
            crosshair.color = Color.green;
            return true;
        }        
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
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForceAtPosition((hit.point - camera.transform.position).normalized * force, hit.point);
            }
            if (breakObjectOnCollision && ExplodingManager.instancia.objects.Contains(hit.transform.gameObject) && hit.transform.tag == "Breakable")
            {                               
                setWeaponStatus(false);
                ExplodingManager.instancia.objects.Remove(hit.transform.gameObject);
                ExplodingManager.instancia.PlayerShoot();
                hit.transform.GetComponent<BreakOnColision>().Break();
                if (hit.transform.gameObject == ExplodingManager.instancia.getExplosive())
                {
                    Debug.Log(ExplodingManager.instancia.player.transform.position);
                    Instantiate(deadPrefab, ExplodingManager.instancia.player.transform.position, ExplodingManager.instancia.player.transform.rotation);
                    Instantiate(ExplodingManager.instancia.ExplosionPrefab, hit.transform.position, hit.transform.rotation);
                    ExplodingManager.instancia.HasWinner();
                }
                CanvasManager.instancia.setTunrCheckpointText("Go back to your spot.");      
                          
            }
            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 0.3f);
        }                
    }
}
