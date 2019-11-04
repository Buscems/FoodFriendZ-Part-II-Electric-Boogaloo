using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    BombThrower BombThrower;
    Vector2 targetPos;
    Vector2 startPos;
    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float height;


    void Start()
    {
        BombThrower = GetComponent<BombThrower>();
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    
    void Update()
    {

        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;
        float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = height * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        Vector3 nextPos = new Vector3(nextX, baseY + arc, 0);
        rb.MovePosition(transform.position + nextPos * speed * Time.deltaTime);
    }
}
