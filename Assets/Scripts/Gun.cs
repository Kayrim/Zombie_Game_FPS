using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    #region Variables
    public float damage = 25f;
    public Camera fpsCamera;
    private int ignoreLayerMask = ~(1 << 10);
    public ParticleSystem muzzleFlash;
    public float lightFlash;
    public float lightFadeTime = 1f;
    public float impactForce = 500f;
    public float fireRate = 0.3f; 
    public float nextShot;
    public float clipAmount = 6;
    bool isReloading;
    bool isShooting;
    public Animator anim;
    public GameObject impactEffect;
    public GameObject impactEffect2;
    public GameObject impactEffect3;
    public GameObject impactEffect4;
    public AudioManager audioManager;
    public Light gunLight;
    HUDManager hm;
    #endregion


    private void Awake()
    {
        hm = FindObjectOfType<HUDManager>();
        gunLight = GetComponentInChildren<Light>();
        gunLight.intensity = 2f;
        gunLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (isReloading) { return; }
            else
            {

                audioManager.Play("ReloadGun");
                isReloading = true;
                StartCoroutine(Reload());
            }
        }
        
        if (Input.GetMouseButtonDown(0) && Time.time > nextShot && !isShooting)
        {
            isShooting = true;            
            StartCoroutine(Shoot());            
            nextShot = Time.time + fireRate; 
        }

    }

    IEnumerator Reload()
    {
        hm.ReloadingWeapon();
        anim.SetBool("Reloading", true);
        
        yield return new WaitForSeconds(2f -.25f);
        clipAmount = 6;
        hm.ammoCount(clipAmount);
        hm.ReloadComplete();
        anim.SetBool("Reloading", false);
        isReloading = false;
        

        yield return new WaitForSeconds(.25f);
    }

    IEnumerator LightFlash()
    {
        gunLight.enabled = true;
        yield return new WaitForSeconds(.15f);
        gunLight.enabled = false;
    }
    IEnumerator Shoot()
    {
        if (clipAmount == 0 && !isReloading)
        {

            audioManager.Play("DryGun");
            isShooting = false;
            yield break;
        }
        if (isReloading && isShooting)
        {
            isShooting = false;
            yield break;
        }
        else
        {
            clipAmount--;
            hm.ammoCount(clipAmount);
            anim.SetBool("Shoot", true);
            muzzleFlash.Play(true);
            audioManager.Play("PistolFire");
            StartCoroutine(LightFlash());


            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Mathf.Infinity, ignoreLayerMask))
            {
                Debug.Log(hit.transform.tag);


                Enemy enemy = hit.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.takeDamage(damage);
                }
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);

                }

                if (hit.transform.tag == "Enemy")
                {
                    GameObject impactTemp4 = Instantiate(impactEffect4, hit.point, Quaternion.LookRotation(hit.normal));

                    Destroy(impactTemp4, 2f);
                }
                else
                {
                    GameObject impactTemp = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    GameObject impactTemp2 = Instantiate(impactEffect2, hit.point, Quaternion.LookRotation(hit.normal));
                    GameObject impactTemp3 = Instantiate(impactEffect3, hit.point, Quaternion.LookRotation(hit.normal));

                    Destroy(impactTemp3, 2f);
                    Destroy(impactTemp2, 5f);
                    Destroy(impactTemp, 1f);
                }
            };
            yield return new WaitForSeconds(.25f);
            anim.SetBool("Shoot", false);
            isShooting = false;
        }
    }
}
