using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkGoneBad : MonoBehaviour
{

    BaseBoss baseBoss;

    Vector3 direction;

    Rigidbody2D rb;

    [HideInInspector]
    public Vector2 velocity;

    bool startStage2;
    bool startStage3;

    [Header("Tackle Variables")]
    public float tackleDistance;
    public float tackleSpeed;
    public bool tackle;
    public float chargeTime;
    Transform tackleTarget;


    // Start is called before the first frame update
    void Start()
    {

        baseBoss = GetComponent<BaseBoss>();
        rb = GetComponent<Rigidbody2D>();

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
                print("yer");
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
        velocity = (transform.position - baseBoss.aggroScript.currentTarget.transform.position).normalized;
        rb.MovePosition(rb.position + velocity * baseBoss.speed * Time.deltaTime);

    }

    void Stage2()
    {

        if (!tackle && (this.transform.position - baseBoss.aggroScript.currentTarget.position).magnitude < tackleDistance)
        {
            StartCoroutine(StartTackle());
            print("Yer");
        }

    }

    IEnumerator StartTackle()
    {
        tackle = true;
        yield return new WaitForSeconds(chargeTime);
        tackleTarget = baseBoss.aggroScript.currentTarget;
        StartCoroutine(Tackle());
    }

    IEnumerator Tackle()
    {
        while(transform.position != tackleTarget.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, tackleTarget.position, tackleSpeed * Time.deltaTime);
            yield return null;
        }
        tackle = false;
    }

    void Stage3()
    {

    }

}
