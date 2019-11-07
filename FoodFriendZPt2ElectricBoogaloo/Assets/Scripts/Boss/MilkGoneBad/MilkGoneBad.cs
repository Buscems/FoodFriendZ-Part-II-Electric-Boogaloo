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
    float tackleCooldown;
    public float tackleCooldownMax;


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
                tackleCooldown -= Time.deltaTime;
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
        if (!tackle)
        {
            velocity = (baseBoss.aggroScript.currentTarget.transform.position - transform.position).normalized;
        }
        rb.MovePosition(rb.position + velocity * baseBoss.speed * Time.deltaTime);
    }

    void Stage2()
    {
        baseBoss.walkIntoDamage = 1;
        if (!tackle && (this.transform.position - baseBoss.aggroScript.currentTarget.position).magnitude < tackleDistance && tackleCooldown <= 0)
        {
            StartCoroutine(StartTackle());
            print("Yer");
        }

    }

    IEnumerator StartTackle()
    {
        tackle = true;
        velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(chargeTime);
        tackleTarget = baseBoss.aggroScript.currentTarget;
        StartCoroutine(Tackle());
    }

    IEnumerator Tackle()
    {
        while(transform.position != tackleTarget.position)
        {
            velocity = (tackleTarget.position - transform.position).normalized;
            yield return null;
        }
        tackleCooldown = tackleCooldownMax;
        tackle = false;
    }

    void Stage3()
    {

    }

}
