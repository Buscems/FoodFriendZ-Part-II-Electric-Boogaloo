using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrBee : MonoBehaviour
{

    BaseBoss baseBoss;

    Vector3 direction;

    public GameObject honeyProjectile;

    Rigidbody2D rb;

    enum BossState { StageOne, StageTwo, Stage3 }
    BossState state;

    // Start is called before the first frame update
    void Start()
    {

        baseBoss= GetComponent<BaseBoss>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        

    }
}
