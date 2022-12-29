using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Variables
    public Transform FirePoint;
    public LineRenderer lineRenderer;
    public Animator animator;
    private bool inShotState;
    private bool isShooting;
    private bool isGunPressed;
    private float fireRate = 0.375f;
    private float nextFire = 0.0f;
    public int damage = 40;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject != null)
        {
            isGunPressed = Input.GetKey("left ctrl");
            isShooting = Input.GetButtonDown("Fire1");
            if (isShooting && isGunPressed && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine(Shoot());
            }
        }
    }

    // Shooting co-routine
    IEnumerator Shoot()
    {
        // Raycast
        RaycastHit2D hitInfo = Physics2D.Raycast(FirePoint.position, FirePoint.right);

        if(hitInfo)
        {
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takeDamage(damage);
            }

            //Instantiate(impactEffetct, hitInfo.point, Quaternion.identity);
            // Line renderer (for debug)
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            // Line renderer (for debug)
            lineRenderer.SetPosition(0, FirePoint.position);
            lineRenderer.SetPosition(1, FirePoint.position + FirePoint.right * 100);
        }
        
        lineRenderer.enabled = true;
        
        yield return new WaitForSeconds(0.02f);
        
        lineRenderer.enabled = false;
    }
}
