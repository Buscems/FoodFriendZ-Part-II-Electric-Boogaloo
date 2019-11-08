using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector] public float damage;

    [HideInInspector] public bool canPierce = false;
    [HideInInspector] public float pierceMultiplier = 1;
    [HideInInspector] public int maxAmountOfEnemiesCanPassThrough = -1;

    private int currentEnemiesPassed = -1;

    [HideInInspector] public bool isBomb;
    [HideInInspector] public float force;
    [HideInInspector] public float radius;

    public GameObject explosionParticles;
    public AudioSource soundToInstantiate;
    public AudioClip soundClip;

    private GameObject enemy = null;

    bool isPinshot;
    bool isNeedler;
    bool canBounce;
    float timeBeforeExplosion;
    float explosionDamage;


    private bool blood;
    private bool fire;
    private bool poison;
    private bool freeze;
    private bool stun;

    private float[] bleedVariables;
    private float[] fireVariables;
    private float[] poisonVariables;
    private float[] freezeVariables;
    private float stunLength;


    void Start()
    {
        currentEnemiesPassed = maxAmountOfEnemiesCanPassThrough;

        //this is for the attack sounds Dan
        try
        {
            soundToInstantiate.clip = soundClip;
            Instantiate(soundToInstantiate.gameObject, transform.position, Quaternion.identity);
        }
        catch
        {

        }

    }

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
                    timeBeforeExplosion -= Time.deltaTime;

                    if (timeBeforeExplosion < 0)
                    {
                        if (enemy.GetComponent<BaseEnemy>() != null)
                        {
                            enemy.GetComponent<BaseEnemy>().TakeDamage(explosionDamage);
                        }
                        if (enemy.GetComponent<BaseBoss>() != null)
                        {
                            enemy.GetComponent<BaseBoss>().TakeDamage(explosionDamage);
                        }
                        GetComponent<BasicBullet>().timeTillDespawn = -6;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    public void SetStatusEffectsBools(bool _blood, bool _fire, bool _poison, bool _freeze, bool _stun)
    {
        blood = _blood;
        fire = _fire;
        poison = _poison;
        freeze = _freeze;
        stun = _stun;
    }
    public void SetStatusEffects(float[] _blood, float[] _fire, float[] _poison, float[] _freeze, float _stun)
    {
        bleedVariables = _blood;
        fireVariables = _fire;
        poisonVariables = _poison;
        freezeVariables = _freeze;
        stunLength = _stun;
    }

    public void SetBulletVariables(bool _canPierce, int _maxAmountOfEnemiesCanPassThrough, float _pierceMultiplier, bool _isPinshot, bool _isNeedler, float _timeBeforeExplosion, float _explosionDamage, bool _canBounce)
    {
        canPierce = _canPierce;
        maxAmountOfEnemiesCanPassThrough = _maxAmountOfEnemiesCanPassThrough;
        pierceMultiplier = _pierceMultiplier;
        isPinshot = _isPinshot;
        isNeedler = _isNeedler;
        timeBeforeExplosion = _timeBeforeExplosion;
        explosionDamage = _explosionDamage;
        canBounce = _canBounce;
    }

    private void SetStatusEffectsToEnemy(GameObject enemy)
    {
        try
        {
            BaseEnemy be = enemy.GetComponent<BaseEnemy>();

            if (blood)
            {
                be.SetBleed(bleedVariables);
            }
            if (fire)
            {
                be.SetBurn(fireVariables);
            }
            if (poison)
            {
                be.SetPoison(poisonVariables);
            }
            if (freeze)
            {
                be.SetFreeze(freezeVariables);
            }
            if (stun)
            {
                be.SetStun(stunLength);
            }
        }
        catch { }

        try
        {
            BaseBoss bb = enemy.GetComponent<BaseBoss>();

        }
        catch { }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canBounce)
        {
            if (gameObject.tag == "Projectile")
            {
                if (other.gameObject.tag == "TilesHere")
                {
                    try
                    {
                        if (isPinshot)
                        {
                            if (enemy != null)
                            {
                                if (enemy.GetComponent<BaseEnemy>() != null)
                                {
                                    other.GetComponent<BaseEnemy>().TakeDamage(explosionDamage);
                                }
                                if (enemy.GetComponent<BaseBoss>() != null)
                                {
                                    enemy.GetComponent<BaseBoss>().TakeDamage(explosionDamage);
                                }
                            }
                            Destroy(gameObject);
                        }
                        else
                        {
                            Destroy(this.gameObject);
                        }
                    }
                    catch { }
                }
            }
        }

        if (other.gameObject.tag == "Enemy")
        {

            ItemExtension ie = GameObject.Find("Player").GetComponent<ItemExtension>();
            if (ie.needEnemyScript)
            {
                ie.bEScript = other.gameObject.GetComponent<BaseEnemy>();
                ie.hasPlayerHitEnemy = true;
            }

            //decrease the enemy's health, this will be for regular enemies as well as boss enemies
            if (other.GetComponent<BaseEnemy>() != null)
            {
                other.GetComponent<BaseEnemy>().TakeDamage(damage);
                SetStatusEffectsToEnemy(other.gameObject);
            }


            if (other.GetComponent<BaseBoss>() != null)
            {
                other.GetComponent<BaseEnemy>().TakeDamage(damage);
                SetStatusEffectsToEnemy(other.gameObject);
            }

            try
            {
                //if attack is non pierce-able, destroy on collision with enemy
                if (gameObject.tag == "Projectile")
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

            if (isPinshot)
            {
                if (other.gameObject.name.Contains("turret"))
                {
                    Destroy(gameObject);
                }
            }
            if (enemy == null)
            {
                enemy = other.gameObject;
            }
        }
    }

    public void DestroyBullet(GameObject _enemy)
    {
        if (isNeedler)
        {
            if (enemy == null)
            {
                GetComponent<BasicBullet>().timeTillDespawn = 11111111;
                damage = 0;
                GetComponent<BasicBullet>().stopMoving = true;
                GetComponent<BasicBullet>().rb.velocity = Vector3.zero;
                if (GetComponent<Animator>() != null)
                {
                    GetComponent<Animator>().enabled = false;
                }
                enemy = _enemy;
                Destroy(GetComponent<Rigidbody2D>());
                if (_enemy.gameObject.name.Contains("turret"))
                {
                    transform.parent = _enemy.transform.parent;
                }
                else
                {
                    transform.parent = _enemy.transform;
                }
            }
        }
        else if (!isPinshot)
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
        CircleCollider2D[] c = GetComponents<CircleCollider2D>();
        for (int i = 0; i < c.Length; i++)
        {
            c[i].enabled = true;
        }
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
