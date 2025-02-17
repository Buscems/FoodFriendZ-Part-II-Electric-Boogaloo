﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{

    public bool ranged;
    public bool melee;
    [HideInInspector]
    public bool meleeDelete;

    private float spinSpeed;

    public float MeleePause;
    public GameObject meleeObject;
    //public GameObject meleePos;
    public float DownTime;

    public float rotateSpeed;
    private Vector2 startPos;
    private Vector2 currentPos;

    public GameObject bullet;

    public float timeInBetweenBullets;
    public float bulletSpeed;

    public int bulletDamage;

    BaseEnemy baseEnemy;
    SpinMelee SpinMelee;
    bool isShooting;

    BulletPool bulletPooler;

    float timer = 5;

    public Sprite[] bulletSprite;
    void Start()
    {
        startPos = transform.position;

        baseEnemy = GetComponent<BaseEnemy>();
        SpinMelee = GetComponent<SpinMelee>();
        baseEnemy.aggroScript.aggro = false;
        StopCoroutine(Fire());

        spinSpeed = rotateSpeed;

        if (melee)
        {
            meleeObject.SetActive(false);
        }

        bulletPooler = BulletPool.Instance;

    }


    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            transform.position = currentPos;
            currentPos.x = startPos.x;
            currentPos.y = startPos.y;

            if (baseEnemy.aggroScript.aggro)
            {
                transform.Rotate(0, 0, rotateSpeed);
                if (!isShooting && ranged && melee == false)
                {
                    StartCoroutine(Fire());
                    isShooting = true;
                    //BulletPool.Instance.SpawnFromPool("Projectile", transform.position, Quaternion.identity);
                }

                if (melee && ranged == false)
                {
                    // Debug.Log("start spin");
                    StartCoroutine(MeleeAttack());


                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    IEnumerator Fire()
    {
        while (baseEnemy.aggroScript.aggro)
        {
            var temp = Instantiate(bullet, transform.position, Quaternion.identity);
            //temp.transform.parent = transform.parent;
            //var temp = BulletPool.Instance.SpawnFromPool("Projectile", transform.position, Quaternion.identity);
            var randNum = Random.Range(0, bulletSprite.Length);
            temp.GetComponent<SpriteRenderer>().sprite = bulletSprite[randNum];
            temp.GetComponent<EnemyBullet>().velocity = this.transform.up;
            temp.GetComponent<EnemyBullet>().speed = bulletSpeed;
            temp.GetComponent<EnemyBullet>().damage = bulletDamage;
            yield return new WaitForSeconds(timeInBetweenBullets);
        }
        isShooting = false;
    }

    IEnumerator MeleeAttack()
    {

        if (meleeDelete == false)
        {
            rotateSpeed = spinSpeed;
            meleeObject.SetActive(true);
            yield return new WaitForSeconds(MeleePause);
            meleeDelete = true;
        }

        if (meleeDelete)
        {
            rotateSpeed = 0;
            meleeObject.SetActive(false);
            yield return new WaitForSeconds(DownTime);
            meleeDelete = false;
        }
    }

}


