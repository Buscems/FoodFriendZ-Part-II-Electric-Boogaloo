using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasePlayer : ScriptableObject
{
    public Sprite hudIcon;

    [Tooltip("This is the name of the character. Make it all lower case")]
    public string characterName;
    public enum AttackType { Melee, Ranged_Basic, Ranged_Burst_Fire, Ranged_Semi_Auto, Ranged_Split_Fire, Boomerang, Builder };
    public AttackType attackType;
    

    [Header("")]
    public float speed;
    public float baseDamage;
    [Range(0.0f, 1.0f)]
    public float critChance;
    public float critDamageMulitiplier = 1;
    public float dodgeSpeedMultiplier = 1;

    [HideInInspector]
    public float currentDodgeSpeedMultiplier;

    public float dodgeLength = 1;

    [HideInInspector]
    public float currentDodgeTime = -1000;

    public float dodgeWaitTime = 1;

    [HideInInspector]
    public float currentDodgeWaitTime = 0;

    [Tooltip("This is going to be the size of the weapon, z is always 1.")]
    public Vector3 attackSize;
    [Tooltip("Can attack go through enemies, or get destroyed on collision")]

    [Header("")]
    public bool canPierce = false;
    [Tooltip("How much more damage does the attack do, per enemy passed through. A value of 1 means damage does not change.")]
    public float pierceMultiplier = 1;
    [Tooltip("How many enemies can the attack pass through before being destroyed. Keep -1 if its infinite.")]
    public int maxAmountOfEnemiesCanPassThrough = -1;


    [Tooltip("This will be how far the weapon is from the player when it is activated.")]
    public float offset;

    [HideInInspector]
    public Vector3 currentPosition;
    [HideInInspector]
    public Vector3 currentDirection;

    
    
    [Header("Melee Characters")]
    public GameObject weapon;
    [Tooltip("This is the position the weapon will be at when it is not being used.")]
    public Vector3 awayPos;
    [Tooltip("This will be how fast the sword attack plays")]
    public float attackSpeed;
    public float rotationalOffset;

    [Header("-If Chargable (Only for 'Melee' and 'Semi-Auto' attack types)")]
    public bool isChargable;
    public float maxDamage;
    public float timeTillMaxDamage;

    [HideInInspector]
    public float currentChargeTimer = 0;
    [HideInInspector]
    public bool startCharging = false;

    //[Tooltip("This is the amount that the sword will spin around the player when attacking.")]
    //public float attackRange;
    [HideInInspector]
    public bool isAttacking;

    [Header("Variables for all ranged characters")]
    public GameObject bullet;
    public float firerate;
    public float bulletSpeed;
    public float timeTillDespawn;
    public bool canBounce;
    [Header("Ranged-Split Fire")]
    public float radius;
    public int bulletsPerShot;
    [Header("Ranged-Burst Fire")]
    public int bulletsPerBurst;
    public float timeBetweenBursts;
    [Header("Needler/Pinshot")]
    public bool isNeedler;
    public bool isPinshot;
    public float explosionDamage;
    public float timeBeforeExplosion;
    [Header("Boomerang")]
    public float timeBeforeReturning;


    [HideInInspector]
    public bool firing = false;
    private float timeBetweenBurstsTimer = 0;
    private int currentBulletnum;

    [Header("Building Characters")]
    public GameObject drop;
    [Tooltip("How large of a radius the spawned item will have")]
    public float dropRadius;
    [Tooltip("How much force to push enemies back if this is a bomb")]
    public float explosionForce;






    [HideInInspector]
    public float currentFirerateTimer = 0;
    [HideInInspector]
    public float currentAttackSpeedTimer = 0;


    [HideInInspector]
    public float attackSizeMultiplier = 1;
    [HideInInspector]
    public float attackSpeedMultiplier = 1;
    [HideInInspector]
    public float firerateMultiplier = 1;
    [HideInInspector]
    public float baseDamageMulitplier = 1;
    [HideInInspector]
    public float maxDamageMultiplier = 1;

    // Start is called before the first frame update
    public void Start()
    {
        dodgeSpeedMultiplier = 1;
        attackSizeMultiplier = 1;
        attackSpeedMultiplier = 1;
        firerateMultiplier = 1;
        baseDamageMulitplier = 1;
        maxDamageMultiplier = 1;

        currentDodgeSpeedMultiplier = 1;

        //this will be taking care of whether or not the player might accidentally have the wrong weapon for anything
        if(attackType == AttackType.Melee)
        {
            bullet = null;
            drop = null;
        }
        if (attackType == AttackType.Ranged_Basic)
        {
            weapon = null;
            drop = null;
        }
        if (attackType == AttackType.Ranged_Burst_Fire)
        {
            weapon = null;
            drop = null;
        }
        if (attackType == AttackType.Ranged_Semi_Auto)
        {
            weapon = null;
            drop = null;
        }
        if (attackType == AttackType.Ranged_Split_Fire)
        {
            weapon = null;
            drop = null;
        }
        if (attackType == AttackType.Boomerang)
        {
            weapon = null;
            drop = null;
        }
        if (attackType == AttackType.Builder)
        {
            weapon = null;
            bullet = null;
        }

        currentBulletnum = bulletsPerBurst;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Time.timeScale != 0)
        {
            if (currentDodgeTime > 0)
            {
                currentDodgeSpeedMultiplier = dodgeSpeedMultiplier;
            }
            else
            {
                currentDodgeSpeedMultiplier = 1;
            }

            currentDodgeWaitTime -= Time.deltaTime;
            currentDodgeTime -= Time.deltaTime;


            if (attackType == AttackType.Ranged_Basic || attackType == AttackType.Ranged_Split_Fire || attackType == AttackType.Ranged_Burst_Fire || attackType == AttackType.Boomerang)
            {
                currentFirerateTimer -= Time.deltaTime;
                timeBetweenBurstsTimer -= Time.deltaTime;
            }
            if (attackType == AttackType.Melee)
             {
                currentAttackSpeedTimer -= Time.deltaTime;
            }
        }
    }


    public void SetMultipliers(float _attackSize, float _attackSpeed, float _firerate, float _baseDamage, float _maxDamage)
    {
        attackSizeMultiplier = _attackSize;
        attackSpeedMultiplier = _attackSpeed;
        firerateMultiplier = _firerate;
        baseDamageMulitplier = _baseDamage;
        maxDamageMultiplier = _maxDamage;
    }


    private void SetBulletVariables(GameObject attack, Transform parentTransform, bool isBoomerang)
    {
        //attack.transform.parent = parentTransform;
        attack.GetComponent<Attack>().SetBulletVariables(canPierce, maxAmountOfEnemiesCanPassThrough, pierceMultiplier, isPinshot, isNeedler, timeBeforeExplosion, explosionDamage);
        attack.GetComponent<BasicBullet>().SetVariables(bulletSpeed, timeTillDespawn, canBounce);

        if (isBoomerang)
        {
           attack.GetComponent<BasicBullet>().isBoomerang = isBoomerang;
           attack.GetComponent<BasicBullet>().timeBeforeReturning = timeBeforeReturning;
           attack.GetComponent<BasicBullet>().player = GameObject.FindGameObjectWithTag("Player1");
        }
    }

    private void SetMeleeVariables(GameObject attack, Transform parentTransform)
    {
        attack.transform.parent = parentTransform;
        //attack.GetComponent<Attack>().canPierce = canPierce;
        if (attack.transform.childCount > 0)
        {
            foreach (Transform child in attack.transform)
            {
                try
                {
                    child.GetComponent<Attack>().maxAmountOfEnemiesCanPassThrough = maxAmountOfEnemiesCanPassThrough;
                    child.GetComponent<Attack>().pierceMultiplier = pierceMultiplier;
                }
                catch { }
            }
        }
        else
        {
            attack.GetComponent<Attack>().maxAmountOfEnemiesCanPassThrough = maxAmountOfEnemiesCanPassThrough;
            attack.GetComponent<Attack>().pierceMultiplier = pierceMultiplier;
        }
    }

    public void MeleeAttack(Vector3 pos, Transform attackDirection, Transform parentTransform, float damage)
    {

        float randNum = Random.Range(0, 1);
        if(randNum < critChance)
        {
            damage *= critDamageMulitiplier;
        }

        GameObject attack = Instantiate(weapon, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + rotationalOffset));
        SetMeleeVariables(attack, parentTransform);
        currentAttackSpeedTimer = attackSpeed;

        if (attack.transform.childCount > 0)
        {
            foreach (Transform child in attack.transform)
            {
                try
                {
                    child.GetComponent<Attack>().damage = damage;
                }
                catch { }
            }
        }
        else
        {
            attack.GetComponent<Attack>().damage = damage;
        }
       
    }

    public void Builder(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        float damage = baseDamage * baseDamageMulitplier;
        float randNum = Random.Range(0, 1);
        if (randNum < critChance)
        {
            damage *= critDamageMulitiplier;
        }

        GameObject attack = Instantiate(drop, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        attack.GetComponentInChildren<Attack>().damage = damage;
        attack.GetComponentInChildren<CircleCollider2D>().radius = dropRadius;
        if(this.characterName == "cherry")
        {
            attack.GetComponentInChildren<Attack>().force = explosionForce;
            attack.GetComponentInChildren<Attack>().radius = dropRadius;
            attack.GetComponentInChildren<Attack>().isBomb = true;
        }
    }

    public void RangedBasic(Vector3 pos, Transform attackDirection, Transform parentTransform, float damage)
    {
        float randNum = Random.Range(0, 1);
        if (randNum < critChance)
        {
            damage *= critDamageMulitiplier;
        }
        currentFirerateTimer = firerate * firerateMultiplier;
        GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        SetBulletVariables(attack, parentTransform, false);
        attack.GetComponent<Attack>().damage = damage;
    }

    public void RangedBoomerang(Vector3 pos, Transform attackDirection, Transform parentTransform, float damage)
    {
        float randNum = Random.Range(0, 1);
        if (randNum < critChance)
        {
            damage *= critDamageMulitiplier;
        }
        currentFirerateTimer = firerate * firerateMultiplier;
        GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        SetBulletVariables(attack, parentTransform, true);
        attack.GetComponent<Attack>().damage = damage;
    }

    public void RangedSplit(Vector3 pos, Transform attackDirection, Transform parentTransform, float damage)
    {
        float randNum = Random.Range(0, 1);
        if (randNum < critChance)
        {
            damage *= critDamageMulitiplier;
        }
        currentFirerateTimer = firerate * firerateMultiplier;
        float angleInterval = radius / bulletsPerShot;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + /*attackRotationalOffset*/ ((radius/2) - (i*angleInterval))));
            SetBulletVariables(attack, parentTransform, false);
            attack.GetComponent<Attack>().damage = damage;
        }
    }

    public void InitiateBurstFire()
    {
        if(firing == false && timeBetweenBurstsTimer < 0)
        {
            firing = true;
            currentBulletnum = bulletsPerBurst;
        }
    }

    public void BurstFire(Vector3 pos, Transform attackDirection, Transform parentTransform, float damage)
    {
        float randNum = Random.Range(0, 1);
        if (randNum < critChance)
        {
            damage *= critDamageMulitiplier;
        }
        if (currentFirerateTimer < 0 && currentBulletnum > 0)
        {
            currentFirerateTimer = firerate * firerateMultiplier;

            GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
            SetBulletVariables(attack, parentTransform, false);
            attack.GetComponent<Attack>().damage = damage;

            currentBulletnum--;

            if(currentBulletnum == 0)
            {
                firing = false;
                timeBetweenBurstsTimer = timeBetweenBursts;
            }
        }
    }
}
