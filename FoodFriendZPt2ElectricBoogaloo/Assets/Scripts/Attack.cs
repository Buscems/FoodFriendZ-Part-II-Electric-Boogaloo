using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public bool canPierce = false;
    [HideInInspector]
    public float pierceMultiplier = 1;
    [HideInInspector]
    public int maxAmountOfEnemiesCanPassThrough = -1;

    private int currentEnemiesPassed =-1;

    [HideInInspector]
    public bool isBomb;
    [HideInInspector]
    public float force;
    [HideInInspector]
    public float radius;

    public GameObject explosionParticles;

    private GameObject enemy = null;


    bool isPinshot;
    bool isNeedler;
    float timeBeforeExplosion;
    float explosionDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemiesPassed = maxAmountOfEnemiesCanPassThrough;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            //this is going to be for any bomb characters or other types of explosions
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);

            foreach (Collider2D nearbyObject in colliders)
            {
                Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddExplosionForce2D(force, transform.position, radius);
                }
            }

            if (enemy != null)
            {
                if (isPinshot)
                {
                    enemy.transform.position = transform.position;
                }
                if (isNeedler)
                {
                    print(timeBeforeExplosion);
                    timeBeforeExplosion -= Time.deltaTime;

                    if (timeBeforeExplosion < 0)
                    {
                        print(enemy.GetComponent<BaseEnemy>().health);
                        enemy.GetComponent<BaseEnemy>().health -= explosionDamage;
                        print(enemy.GetComponent<BaseEnemy>().health);
                        GetComponent<BasicBullet>().timeTillDespawn = -6;
                        Destroy(gameObject);
                    }
                }
            }
        }

    }

    public void SetBulletVariables(bool _canPierce, int _maxAmountOfEnemiesCanPassThrough, float _pierceMultiplier, bool _isPinshot, bool _isNeedler, float _timeBeforeExplosion, float _explosionDamage)
    {
        canPierce = _canPierce;
        maxAmountOfEnemiesCanPassThrough = _maxAmountOfEnemiesCanPassThrough;
        pierceMultiplier = _pierceMultiplier;
        isPinshot = _isPinshot;
        isNeedler = _isNeedler;
        timeBeforeExplosion = _timeBeforeExplosion;
        explosionDamage = _explosionDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag == "Projectile")
        {
            if (other.gameObject.tag == "TilesHere")
            {
                try
                {
                    if (isPinshot)
                    {
                        enemy.GetComponent<BaseEnemy>().health -= explosionDamage;
                        GetComponent<BasicBullet>().timeTillDespawn = -6;
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
                catch { }
            }
        }

        if (other.gameObject.tag == "Enemy")
        {
            if (!isBomb)
            {
                //decrease the enemy's health, this will be for regular enemies as well as boss enemies
                other.GetComponent<BaseEnemy>().health -= damage;

                try
                {
                    //if attack is non pierce-able, destroy on collision with enemy
                    if (transform.root.GetComponent<MainPlayer>().HitEnemy(gameObject.tag))
                    {
                        if (!canPierce)
                        {
                            DestroyBullet(other.gameObject);
                        }
                    }
                }
                catch
                {
                    DestroyBullet(other.gameObject);
                }

                damage *= pierceMultiplier;

                if (currentEnemiesPassed != -1)
                {
                    if (currentEnemiesPassed == 0)
                    {
                        DestroyBullet(other.gameObject);
                    }

                    currentEnemiesPassed -= 1;
                }
            }
        }

       // if(other.gameObject.tag == "")
    }

    public void DestroyBullet(GameObject _enemy)
    {
        if (isPinshot || isNeedler)
        {
            if (enemy == null)
            {
                print("fdghdxhxdghxfgh0");
                GetComponent<BasicBullet>().timeTillDespawn = 11111111;
                damage = 0;
                GetComponent<BasicBullet>().stopMoving = true;
                GetComponent<BasicBullet>().rb.velocity = Vector3.zero;
                GetComponent<Animator>().enabled = false;
                enemy = _enemy;
                Destroy(GetComponent<Rigidbody2D>());
                transform.parent = _enemy.transform;
            }
        }
        else
        {
            try
            {
                if (transform.parent.name.Contains("Holder"))
                {
                    Destroy(transform.parent.gameObject);
                }
            }
            catch { Destroy(gameObject); }
        }
    }

    public void Destroy()
    {
        try
        {
            if (transform.parent.name.Contains("Holder"))
            {
                Destroy(transform.parent.gameObject);
            }
        }
        catch { Destroy(gameObject); }
    }


    public void Exploison()
    {
        Instantiate(explosionParticles, gameObject.transform.position, Quaternion.identity);
    }
}
