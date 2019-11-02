using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extending : MonoBehaviour
{

    BaseEnemy baseEnemy;
    [SerializeField] float max;
    [SerializeField] float speed;
    float originalScale;
    float endScale;
    private Vector3 targetPos;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        targetPos = transform.position;
        originalScale = transform.localScale.x;
        endScale = 7;
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, endScale, Time.deltaTime * speed), transform.localScale.y, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }
}
