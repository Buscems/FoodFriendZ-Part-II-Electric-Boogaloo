using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBody : MonoBehaviour
{
    public Transform wormHead;

    BaseEnemy baseEnemy;

    public float distToHead;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, wormHead.position) < distToHead)
        {
            transform.position = Vector2.MoveTowards(transform.position, wormHead.position, baseEnemy.speed * Time.deltaTime);
        }
    }
}
        /*if (wormHead.GetComponent<WormEnemy>().isDead == true){
            Destroy(gameObject);
        }
    }*/

