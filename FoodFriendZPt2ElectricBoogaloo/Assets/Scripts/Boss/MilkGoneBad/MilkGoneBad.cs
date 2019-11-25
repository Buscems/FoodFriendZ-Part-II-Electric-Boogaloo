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
    Vector3 tackleTarget;
    float tackleCooldown;
    public float tackleCooldownMax;
    bool hitWall;
    public float stunTime;
    [SerializeField]
    bool stun;

    [Header("Ball of Milk")]
    public float rollForce;


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
                    baseBoss.speed *= 4f;
                    startStage2 = true;
                }
                tackleCooldown -= Time.deltaTime;
            }
            //stage3
            if (baseBoss.stage == BaseBoss.BossStage.stage3)
            {
                if (!startStage3)
                {
                    baseBoss.anim.SetTrigger("Ball");
                    baseBoss.walkIntoDamage = 1;
                    startStage3 = true;
                }
                Stage3Movement();
                
            }
        }

    }

    void Stage1()
    {
        this.GetComponent<ShootingEnemy>().enabled = true;
    }

    void Stage1Movement()
    {
        if (!stun)
        {
            if (!tackle)
            {
                velocity = (baseBoss.aggroScript.currentTarget.transform.position - transform.position).normalized;
                rb.MovePosition(rb.position + velocity * baseBoss.speed * Time.deltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + velocity * tackleSpeed * Time.deltaTime);
            }
            //this will switch the animation of the boss
            if (!stun)
            {
                if (direction == new Vector3(0, 0))
                {
                    if (velocity.x > 0 && velocity.y < 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 0);
                    }
                    else if (velocity.x < 0 && velocity.y < 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 1);
                    }
                    else if (velocity.x > 0 && velocity.y > 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 2);
                    }
                    else if (velocity.x < 0 && velocity.y > 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 3);
                    }
                }
            }
            else
            {
                if (direction == new Vector3(0, 0))
                {
                    if (velocity.x > 0 && velocity.y < 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 4);
                    }
                    else if (velocity.x < 0 && velocity.y < 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 5);
                    }
                    else if (velocity.x > 0 && velocity.y > 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 6);
                    }
                    else if (velocity.x < 0 && velocity.y > 0)
                    {
                        baseBoss.anim.SetFloat("Blend", 7);
                    }
                }
            }
        }
    }

    void Stage3Movement()
    {
        velocity = (baseBoss.aggroScript.currentTarget.transform.position - transform.position).normalized;
        rb.AddForce(velocity * rollForce);

        transform.up = -velocity;
        if (this.GetComponent<ShootingEnemy>().enabled)
        {
            this.GetComponent<ShootingEnemy>().enabled = false;
        }

    }

    void Stage2()
    {
        if (!stun && !tackle && (this.transform.position - baseBoss.aggroScript.currentTarget.position).magnitude < tackleDistance && tackleCooldown <= 0)
        {
            StartCoroutine(StartTackle());
        }
    }

    IEnumerator StartTackle()
    {
        tackle = true;
        velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(chargeTime);
        tackleTarget = baseBoss.aggroScript.currentTarget.position;
        StartCoroutine(Tackle());
    }

    IEnumerator Tackle()
    {
        Vector3 startPos = transform.position;
        Vector3 attackDir = (tackleTarget - transform.position).normalized;
        while ((startPos - transform.position).magnitude < tackleDistance || !hitWall)
        {
            velocity = attackDir;
            yield return null;
        }
        tackleCooldown = tackleCooldownMax;
        tackle = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TilesHere" && tackle)
        {
            hitWall = true;
            StartCoroutine(Stun());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "TilesHere" && tackle)
        {
            hitWall = false;
        }
    }

    IEnumerator Stun()
    {

        stun = true;
        if (this.GetComponent<ShootingEnemy>().enabled)
        {
            this.GetComponent<ShootingEnemy>().enabled = false;
        }
        yield return new WaitForSeconds(stunTime);
        this.GetComponent<ShootingEnemy>().enabled = true;
        stun = false;

    }

}
