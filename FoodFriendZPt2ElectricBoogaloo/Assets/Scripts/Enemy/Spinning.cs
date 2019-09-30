using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{

    public float rotateSpeed;
    private Vector2 startPos;
    private Vector2 currentPos;

    public GameObject bullet;

    public float timeInBetweenBullets;
    public float bulletSpeed;

    BaseEnemy baseEnemy;
    bool isShooting;

    void Start()
    {
        startPos = transform.position;

        baseEnemy = GetComponent<BaseEnemy>();

    }

    
    void Update()
    {
        transform.position = currentPos;
        currentPos.x = startPos.x;
        currentPos.y = startPos.y;

        if (baseEnemy.aggroScript.aggro)
        {
            transform.Rotate(0, 0, rotateSpeed);
            if (!isShooting)
            {
                StartCoroutine(Fire());
                isShooting = true;
            }
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    IEnumerator Fire()
    {
        while (baseEnemy.aggroScript.aggro)
        {
            var temp = Instantiate(bullet, transform.position, Quaternion.identity);
            temp.GetComponent<EnemyBullet>().velocity = this.transform.up;
            temp.GetComponent<EnemyBullet>().speed = bulletSpeed;
            yield return new WaitForSeconds(timeInBetweenBullets);
        }
        isShooting = false;
    }

}
