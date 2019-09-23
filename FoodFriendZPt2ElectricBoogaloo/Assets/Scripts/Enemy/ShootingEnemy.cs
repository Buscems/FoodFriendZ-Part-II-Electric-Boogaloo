using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    /*[Tooltip("Length of the Raycast shooting from the FRONT of the enemy")]
    public float rayLength;
    [Tooltip("Length of the Raycast shooting from the LEFT of the enemy")]
    public float leftLength;
    [Tooltip("Length of the Raycast shooting from the RIGHT of the enemy")]
    public float rightLength;*/

    BaseEnemy baseEnemy;

    public bool beam;

    [Tooltip("Check the box if you want the enemy to have a limited amount of bullets to shoot before reloading")]
    public bool Clip;
    private bool Reloading;
    [Tooltip("Amount of time the enemy will take to reload its weapon")]
    public float coroutineTime;

    [Tooltip("Projectile Game Object that you want to link")]
    public GameObject Projectile;

    [Tooltip("Speed of the projectile")]
    public float projectileSpeed;
    private float nextShot;
    [Tooltip("Rate at which projectiles are fired")]
    public float fireRate;
    [Tooltip("Amount of projectiles enemy will shoot before it has to reload")]
    public float clipSize;
    private float currentClip;

    private Vector2 currentPos;
    private Vector2 startPos;

    private IEnumerator coroutine;

    void Start()
    {
        startPos = transform.position;
        currentClip = clipSize;

        //referencing the base script to derive from variables of other scripts
        baseEnemy = this.GetComponent<BaseEnemy>();
    }

    
    void Update()
    {
        /*currentPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(currentPos, transform.up, rayLength);
        RaycastHit2D Left = Physics2D.Raycast(currentPos, transform.right *-1, leftLength);
        RaycastHit2D Right = Physics2D.Raycast(currentPos, transform.right, rightLength);
        Debug.DrawRay(currentPos, transform.up, Color.red, rayLength);

        if(hit.collider != null)
        {
            Debug.Log("hit" + hit.collider.tag);
            if(hit.collider.tag == "Player")
            {
                Instantiate(Projectile, currentPos, Quaternion.identity);
                currentClip--;
            }
        }*/

        if (baseEnemy.aggroScript.aggro)
        {
            if (Time.time > nextShot)
            {
                nextShot = Time.time + fireRate;
                var bullet = Instantiate(Projectile, this.transform.position, Quaternion.identity);
                bullet.GetComponent<EnemyBullet>().velocity = (baseEnemy.aggroScript.currentTarget.transform.position - this.transform.position).normalized;
                bullet.GetComponent<EnemyBullet>().speed = projectileSpeed;
            }
        }

        if (beam)
        {
            
        }

        if (Clip)
        {
            if (currentClip == 0)
            {
                Reloading = true;
            }
            if (Reloading)
            {
                StartCoroutine(Reload());
            }
        }
    }

    IEnumerator Reload()
    {
        Reloading = false;
        Debug.Log("Starting Coroutine " + Time.time);
        yield return new WaitForSeconds(coroutineTime);
        Debug.Log("Ending Coroutine " + Time.time);
        currentClip = clipSize;
    }

    

}
