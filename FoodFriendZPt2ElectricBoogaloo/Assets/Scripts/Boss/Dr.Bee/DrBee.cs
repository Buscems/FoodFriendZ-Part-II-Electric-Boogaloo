using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrBee : MonoBehaviour
{

    BaseBoss baseBoss;

    Vector3 direction;

    Rigidbody2D rb;

    [HideInInspector]
    public Vector2 velocity;

    bool startStage2;
    bool startStage3;

    // Start is called before the first frame update
    void Start()
    {

        baseBoss= GetComponent<BaseBoss>();
        rb = GetComponent<Rigidbody2D>();

        //randomizing movement for first phase
        var randNum = Random.Range(0, 1);
        if (randNum == 0)
        {
            velocity = new Vector2(-.5f, .5f);
        }
        else
        {
            velocity = new Vector2(.5f, .5f);
        }

        startStage2 = false;
        startStage3 = false;

    }

    // Update is called once per frame
    void Update()
    {

        //if the boss is in aggro, fight
        if (baseBoss.aggroScript.aggro)
        {
            //stage1
            if (baseBoss.stage == BaseBoss.BossStage.stage1)
            {
                Stage1();
                Stage1Movement();
            }
            //stage2
            if (baseBoss.stage == BaseBoss.BossStage.stage2)
            {
                Stage2();
                Stage1Movement();
                if (!startStage2)
                {
                    baseBoss.speed *= 1.5f;
                    startStage2 = true;
                }
            }
            //stage3
            if (baseBoss.stage == BaseBoss.BossStage.stage3)
            {
                Stage1Movement();
            }
        }

    }

    void Stage1()
    {
        this.GetComponent<ShootingEnemy>().enabled = true;
    }

    void Stage1Movement()
    {
        rb.MovePosition(rb.position + velocity * baseBoss.speed * Time.deltaTime);

    }

    void Stage2()
    {
        this.GetComponent<DropAttack>().enabled = true;
    }

    void Stage3()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TilesHere")
        {
            velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);
        }
    }

}
