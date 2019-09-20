using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasePlayer : ScriptableObject
{
    public float speed;
    public float attackDamage;
    [Tooltip("This is going to be the size of the weapon, z is always 1.")]
    public Vector3 attackSize;
    [Tooltip("Can attack go through enemies, or get destroyed on collision")]
    public bool canPierce = false;
    [Tooltip("How much more damage does the attack do, per enemy passed through. A value of 1 means damage does not change.")]
    public float pierceMultiplier = 1;
    [Tooltip("How many enemies can the attack pass through before being destroyed. Keep -1 if its infinite.")]
    public int maxAmountOfEnemiesCanPassThrough = -1;

    [HideInInspector]
    public Vector3 currentPosition;
    [HideInInspector]
    public Vector3 currentDirection;

    [Tooltip("This is the name of the character. Make it all lower case")]
    public string characterName;
    // [Tooltip("Can be 'Melee', 'Melee-Charge', 'Ranged-Basic', 'Ranged-Burst Fire', 'Ranged-Semi Auto', 'Ranged-Charge Fire', 'Ranged-Split Fire' or 'Builder'")]
    //public string attackType;
    public enum AttackType { Melee, Melee_Charge, Ranged_Basic, Ranged_Burst_Fire, Ranged_Semi_Auto, Ranged_Charge_Fire, Ranged_Split_Fire, Builder};

    public AttackType attackType;

    [Header("Melee Characters")]
    public GameObject weapon;
    [Tooltip("This is the position the weapon will be at when it is not being used.")]
    public Vector3 awayPos;
    [Tooltip("This will be how far the weapon is from the player when it is activated.")]
    public float offset;
    [Tooltip("This will be how fast the sword attack plays")]
    public float attackSpeed;
    //[Tooltip("This is the amount that the sword will spin around the player when attacking.")]
    //public float attackRange;
    [HideInInspector]
    public bool isAttacking;

    [Header("Variables for all ranged characters")]
    public GameObject bullet;
    public float firerate;
    public float bulletSpeed;
    public float timeTillDespawn;
    [Header("Ranged-Split Fire")]
    public float radius;
    public int bulletsPerShot;
    [Header("Ranged-Burst Fire")]
    public int bulletsPerBurst;
    public float timeBetweenBursts;

    [HideInInspector]
    public bool firing = false;
    private float timeBetweenBurstsTimer = 0;
    private int currentBulletnum;

    [Header("Building Characters")]
    public GameObject drop;

    [HideInInspector]
    public float currentFirerateTimer = 0;

    // Start is called before the first frame update
    public void Start()
    {
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
        if (attackType == AttackType.Ranged_Basic || attackType == AttackType.Ranged_Split_Fire || attackType == AttackType.Ranged_Burst_Fire)
        {
            currentFirerateTimer -= Time.deltaTime;
            timeBetweenBurstsTimer -= Time.deltaTime;
        }
    }

    private void SetBulletVariables(GameObject attack, Transform parentTransform)
    {
        attack.transform.parent = parentTransform;
        attack.GetComponent<Attack>().damage = attackDamage;
        attack.GetComponent<Attack>().canPierce = canPierce;
        attack.GetComponent<Attack>().maxAmountOfEnemiesCanPassThrough = maxAmountOfEnemiesCanPassThrough;
        attack.GetComponent<Attack>().pierceMultiplier = pierceMultiplier;
        attack.GetComponent<BasicBullet>().bulletSpeed = bulletSpeed;
        attack.GetComponent<BasicBullet>().timeTillDespawn = timeTillDespawn;        
    }

    public void MeleeAttack(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {

        GameObject attack = Instantiate(weapon, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        attack.transform.parent = parentTransform;
        attack.GetComponent<Attack>().damage = attackDamage;
    }

    public void Builder(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {

        GameObject attack = Instantiate(drop, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        attack.GetComponent<Attack>().damage = attackDamage;
    }

    public void RangedBasic(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        currentFirerateTimer = firerate;
        GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        SetBulletVariables(attack, parentTransform);
    }

    public void RangedSplit(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        currentFirerateTimer = firerate;
        float angleInterval = radius / bulletsPerShot;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + /*attackRotationalOffset*/ ((radius/2) - (i*angleInterval))));
            SetBulletVariables(attack, parentTransform);
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

    public void BurstFire(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        if(currentFirerateTimer < 0 && currentBulletnum > 0)
        {
            currentFirerateTimer = firerate;

            GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
            SetBulletVariables(attack, parentTransform);

            currentBulletnum--;

            if(currentBulletnum == 0)
            {
                firing = false;
                timeBetweenBurstsTimer = timeBetweenBursts;
            }
        }
    }
}
