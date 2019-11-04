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
    float offset;
    private Vector3 targetPos;

    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        targetPos = transform.position;
        originalScale = transform.localScale.x;
        endScale = 7;
        offset = endScale * .0005f;
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            transform.localScale = new Vector3(Mathf.MoveTowards(transform.localScale.x, endScale, Time.deltaTime * speed), transform.localScale.y, 0);
            transform.position = new Vector3(transform.position.x + offset, transform.position.y, 0);
        }
    }
}
