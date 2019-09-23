using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class MainPlayer : MonoBehaviour
{
    public int health;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    Vector3 direction;

    public Transform attackDirection;

    public BasePlayer currentChar;

    public Animator anim;

    [HideInInspector]
    public float speed;

    Rigidbody2D rb;
    Vector3 velocity;

    private CameraShake cam;


    private void Awake()
    {

        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
    }

    // Start is called before the first frame update
    void Start()
    {

        currentChar.Start();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        LookDirection();

        AnimationHandler();

        if (currentChar.startCharging)
        {
            currentChar.currentChargeTimer += Time.deltaTime;
        }
        else
        {
            currentChar.currentChargeTimer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            health--;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            health++;
        }

        if (myPlayer.GetButtonDown("Attack"))
        {
            if (currentChar.isChargable)
            {
                StartCharge();
            }
            else
            {
                if (currentChar.attackType == BasePlayer.AttackType.Melee)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, currentChar.baseDamage);
                }
                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage);
                }
            }
            if (currentChar.attackType == BasePlayer.AttackType.Builder)
            {
                currentChar.Builder(transform.position, attackDirection, transform);
            }
        }
        if (currentChar.attackType == BasePlayer.AttackType.Ranged_Burst_Fire)
        {
            if (currentChar.firing)
            {
                currentChar.BurstFire(transform.position, attackDirection, transform, currentChar.baseDamage);
            }
        }

        if (myPlayer.GetButton("Attack") && currentChar.currentFirerateTimer < 0)
        {

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Basic)
            {
                currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage);
            }

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Split_Fire)
            {
                currentChar.RangedSplit(transform.position, attackDirection, transform, currentChar.baseDamage);
            }
            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Burst_Fire)
            {
                currentChar.InitiateBurstFire();
            }
        }

        if (myPlayer.GetButton("Attack"))
        {
            if (currentChar.isChargable)
            {
                //print(currentChar.currentChargeTimer);
                float tempDamage = currentChar.baseDamage;
                if (currentChar.currentChargeTimer > currentChar.timeTillMaxDamage)
                {
                    tempDamage = currentChar.maxDamage;

                    if (currentChar.attackType == BasePlayer.AttackType.Melee)
                    {
                        currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage);
                    }
                    if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                    {
                        currentChar.RangedBasic(transform.position, attackDirection, transform, tempDamage);
                    }

                    currentChar.currentChargeTimer = 0;
                }
            }
        }

        if (myPlayer.GetButtonUp("Attack"))
        {
            if (currentChar.isChargable)
            {
                float tempDamage;
                if (currentChar.currentChargeTimer > currentChar.timeTillMaxDamage)
                {
                    tempDamage = currentChar.maxDamage;
                }
                else
                {
                    tempDamage = currentChar.maxDamage * (currentChar.currentChargeTimer / currentChar.timeTillMaxDamage);
                }


                if (currentChar.attackType == BasePlayer.AttackType.Melee)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage);
                }

                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    currentChar.RangedBasic(transform.position, attackDirection, transform, tempDamage);
                }

                currentChar.currentChargeTimer = 0;

                currentChar.startCharging = false;
            }
        }
    }

    void StartCharge()
    {
        currentChar.startCharging = true;
        currentChar.currentChargeTimer = 0;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (velocity * speed) * Time.deltaTime);
    }

    void AnimationHandler()
    {
        //this will be handling which character the player currently is in terms of animation
        switch (currentChar.characterName)
        {
            case "tofu":
                anim.SetInteger("characterID", 1);
                break;

            case "onigiri":
                anim.SetInteger("characterID", 2);
                break;

            case "takoyaki":
                anim.SetInteger("characterID", 3);
                break;
        }
         
        //this will switch the animation of the current character
        if (direction.x > 0 && direction.y < 0)
        {
            anim.SetFloat("Blend", 0);
            //Debug.Log("Right Front");
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            anim.SetFloat("Blend", 1);
            //Debug.Log("Left Front");
        }
        else if (direction.x > 0 && direction.y > 0)
        {
            anim.SetFloat("Blend", 2);
            //Debug.Log("Right Back");
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            anim.SetFloat("Blend", 3);
            //Debug.Log("Left Back");
        }
    }

    private void PlayerMovement()
    {
        currentChar.Update();
        speed = currentChar.speed;
        currentChar.currentPosition = this.transform.position;

        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");

        direction.x = myPlayer.GetAxisRaw("DirectionHorizontal");
        direction.y = myPlayer.GetAxisRaw("DirectionVertical");

        if (velocity.x != 0)
        {
            currentChar.currentDirection.x = velocity.x;
        }
        if (velocity.y != 0)
        {
            currentChar.currentDirection.y = velocity.y;
        }
    }

    private void LookDirection()
    {
        if (direction.y != 0)
        {
            attackDirection.transform.right = direction;
        }

        if (direction.x != 0)
        {
            attackDirection.transform.right = direction;
        }
    }



    public bool HitEnemy(string tag)
    {       

        if(tag == "Projectile")
        {
            return true;
        }
        else
        {
            cam.StartShake();
            return false;
        }
    }

    public void GetHit(int damage)
    {
        health -= damage;
    }

    //these two methods are for ReWired, if any of you guys have any questions about it I can answer them, but you don't need to worry about this for working on the game - Buscemi
    void OnControllerConnected(ControllerStatusChangedEventArgs arg)
    {
        CheckController(myPlayer);
    }

    void CheckController(Player player)
    {
        foreach (Joystick joyStick in player.controllers.Joysticks)
        {
            var ds4 = joyStick.GetExtension<DualShock4Extension>();
            if (ds4 == null) continue;//skip this if not DualShock4
            switch (playerNum)
            {
                case 4:
                    ds4.SetLightColor(Color.yellow);
                    break;
                case 3:
                    ds4.SetLightColor(Color.green);
                    break;
                case 2:
                    ds4.SetLightColor(Color.blue);
                    break;
                case 1:
                    ds4.SetLightColor(Color.red);
                    break;
                default:
                    ds4.SetLightColor(Color.white);
                    Debug.LogError("Player Num is 0, please change to a number > 0");
                    break;
            }


        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "StatBoost")
        {
            BasePowerUp temp = other.gameObject.GetComponent<BasePowerUp>();

            currentChar.speed *= temp.movementSpeed;
            currentChar.attackSize *= temp.attackSize;
            currentChar.attackSpeed *= temp.attackSpeed; //for melee
            currentChar.firerate *= temp.attackSpeed; //for projectiles
            currentChar.baseDamage *= temp.attackDamage;
            currentChar.maxDamage *= temp.attackDamage;

            Destroy(other.gameObject);
        }
    }

}
