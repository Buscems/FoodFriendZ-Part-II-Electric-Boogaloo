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

    [HideInInspector]
    public Vector3 currentPosition;
    public Vector3 currentDirection;
    [Tooltip("This will be how much the sprite should be rotated for the attack animation")]
    public float attackRotationalOffset;

    [Tooltip("Can be 'Melee', 'Melee-Charge', 'Ranged-Basic', 'Ranged-Burst Fire', 'Ranged-Semi Auto', 'Ranged-Charge Fire', 'Ranged-Split Fire' or 'Builder'")]
    public string attackType;

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

    [Header("Ranged Characters")]
    public GameObject bullet;
    public float firerate;
    public float bulletSpeed;
    [Header("Ranged-Split Fire")]
    public float radius;
    public int bulletsPerShot;

    [Header("Building Characters")]
    public GameObject drop;

    [HideInInspector]
    public float currentFirerateTimer = 0;

    // Start is called before the first frame update
    public void Start()
    {
        //this will be taking care of whether or not the player might accidentally have the wrong weapon for anything
        if(attackType == "Melee")
        {
            bullet = null;
            drop = null;
        }
        if (attackType == "Ranged-Basic")
        {
            weapon = null;
            drop = null;
        }
        if (attackType == "Ranged-Burst Fire")
        {
            weapon = null;
            drop = null;
        }
        if (attackType == "Ranged-Semi Auto")
        {
            weapon = null;
            drop = null;
        }
        if (attackType == "Ranged-Split Fire")
        {
            weapon = null;
            drop = null;
        }
        if (attackType == "builder")
        {
            weapon = null;
            bullet = null;
        }

    }

    // Update is called once per frame
    public void Update()
    {
        if (attackType == "Ranged-Basic" || attackType == "Ranged-Split Fire")
        {
            currentFirerateTimer -= Time.deltaTime;
        }
    }

    public void MeleeAttack(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        GameObject attack = Instantiate(weapon, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + attackRotationalOffset));
        attack.transform.parent = parentTransform;
        attack.GetComponent<Attack>().damage = attackDamage;
    }

    public void RangedBasic(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + attackRotationalOffset));
        attack.transform.parent = parentTransform;
        attack.GetComponent<Attack>().damage = attackDamage;
        attack.GetComponent<BasicBullet>().bulletSpeed = bulletSpeed;
    }

    public void RangedSplit(Vector3 pos, Transform attackDirection, Transform parentTransform)
    {
        float angleInterval = radius / bulletsPerShot;

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject attack = Instantiate(bullet, pos + (attackDirection.transform.right * offset), Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z + /*attackRotationalOffset*/ ((radius/2) - (i*angleInterval))));
            attack.transform.parent = parentTransform;
            attack.GetComponent<Attack>().damage = attackDamage;
            attack.GetComponent<BasicBullet>().bulletSpeed = bulletSpeed;
        }
    }
}
