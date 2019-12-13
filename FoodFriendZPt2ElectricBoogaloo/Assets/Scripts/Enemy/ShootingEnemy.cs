using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [Tooltip("Length of the Raycast shooting from the FRONT of the enemy")]
    public float rayLength;
    /*[Tooltip("Length of the Raycast shooting from the LEFT of the enemy")]
    public float leftLength;
    [Tooltip("Length of the Raycast shooting from the RIGHT of the enemy")]
    public float rightLength;*/

    BaseEnemy baseEnemy;
    BaseBoss baseBoss;

    [Tooltip("Check box if you are using a beam as the way your enemy shoots")]
    public bool beam;
    [Tooltip("Place your Beam object here")]
    public GameObject beamObject;

    [Tooltip("Check the box if you want the enemy to have a limited amount of bullets to shoot before reloading")]
    public bool Clip;
    private bool Reloading;
    [Tooltip("Amount of time the enemy will take to reload its weapon")]
    public float coroutineTime;

    [Tooltip("Projectile Game Object that you want to link")]
    public GameObject Projectile;

    [Tooltip("Projectile Damage")]
    public int bulletDamage;

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

    [Header("Type of Bullet")]
    [Tooltip("If this enemy shoots bullets that slow the player down or not")]
    public bool slowBullets;

    void Start()
    {
        startPos = transform.position;
        currentClip = clipSize;

        //referencing the base script to derive from variables of other scripts
        if (GetComponent<BaseEnemy>() != null)
        {
            baseEnemy = this.GetComponent<BaseEnemy>();
        }
        if (GetComponent<BaseBoss>() != null)
        {
            baseBoss = this.GetComponent<BaseBoss>();
        }
    }

    

    void Update()
    {
        currentPos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(currentPos, transform.position * -1, rayLength);
        //Debug.DrawRay(currentPos, transform.position * -1 ,Color.red, rayLength);
        /*RaycastHit2D Left = Physics2D.Raycast(currentPos, transform.right *-1, leftLength);
        RaycastHit2D Right = Physics2D.Raycast(currentPos, transform.right, rightLength);

        if(hit.collider != null)
        {
            Debug.Log("hit" + hit.collider.tag);
            if(hit.collider.tag == "Player")
            {
                Instantiate(Projectile, currentPos, Quaternion.identity);
                currentClip--;
            }
        }*/

        //if this is a boss or a regular enemy

        if (baseEnemy != null)
        {
            if (baseEnemy.aggroScript.aggro && currentClip > 0)
            {
                if (Time.time > nextShot)
                {
                    nextShot = Time.time + fireRate;
                    var bullet = Instantiate(Projectile, this.transform.position, Quaternion.identity);
                    bullet.GetComponent<EnemyBullet>().velocity = (baseEnemy.aggroScript.currentTarget.transform.position - this.transform.position).normalized;
                    bullet.GetComponent<EnemyBullet>().transform.up = (baseEnemy.aggroScript.currentTarget.transform.position - this.transform.position).normalized;
                    bullet.GetComponent<EnemyBullet>().speed = projectileSpeed;
                    bullet.GetComponent<EnemyBullet>().damage = bulletDamage;
                }
            }
        }

        if (baseBoss != null)
        {
            if (baseBoss.aggroScript.aggro && currentClip > 0)
            {
                if (Time.time > nextShot)
                {
                    nextShot = Time.time + fireRate;
                    var bullet = Instantiate(Projectile, this.transform.position, Quaternion.identity);
                    bullet.GetComponent<EnemyBullet>().velocity = (baseBoss.aggroScript.currentTarget.transform.position - this.transform.position).normalized;
                    bullet.GetComponent<EnemyBullet>().transform.up = (baseBoss.aggroScript.currentTarget.transform.position - this.transform.position).normalized;
                    bullet.GetComponent<EnemyBullet>().speed = projectileSpeed;
                    bullet.GetComponent<EnemyBullet>().damage = bulletDamage;
                    if (slowBullets)
                    {
                        bullet.GetComponent<EnemyBullet>().slowBullet = true;
                    }
                }
            }
        }

        /*if (beam)
        {
            Clip = false;
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "Player")
                {
                    Instantiate(beamObject, this.transform.position, Quaternion.identity);
                }
            }
            else
            {
                Destroy(beamObject);
            }
           
        }*/

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
        //Debug.Log("Starting Coroutine " + Time.time);
        yield return new WaitForSeconds(coroutineTime);
        //Debug.Log("Ending Coroutine " + Time.time);
        currentClip = clipSize;
    }

    

}
