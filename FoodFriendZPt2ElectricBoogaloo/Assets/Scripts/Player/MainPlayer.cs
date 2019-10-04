using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Rewired.ControllerExtensions;
using TMPro;

public class MainPlayer : MonoBehaviour
{
    public int health;
    [HideInInspector]
    public int currency;


    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    private TextMeshProUGUI currencyText;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Tooltip("Turn on if the player is using the mouse")]
    public bool usingMouse;
    public GameObject mouseCursor;

    Vector3 direction;

    public Transform attackDirection;

    public BasePlayer currentChar;

    public Animator anim;

    [HideInInspector]
    public float speed;

    [HideInInspector]
    public float stunTimer;
    public float maxStunTimer;

    Rigidbody2D rb;
    Vector3 velocity;

    private CameraShake cam;

    [Header("Current Party")]
    public BasePlayer triangle;
    public BasePlayer square;
    public BasePlayer circle;
    [HideInInspector]
    public BasePlayer cross;

    [Header("Big boi array of every playable character in the game")]
    public BasePlayer[] allCharacters;

    private bool isHolding = false;





    private Image upCharacter;
    private Image upHighlight;

    private Image downCharacter;
    private Image downHighlight;

    private Image leftCharacter;
    private Image leftHighlight;

    private Image rightCharacter;
    private Image rightHighlight;


    private void Awake()
    {
        cross = currentChar;
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
        currencyText = GameObject.Find("MoneyCount").GetComponent<TextMeshProUGUI>();
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        try
        {
            upCharacter = GameObject.Find("Up_Character").GetComponent<Image>();
            upHighlight = GameObject.Find("Up_Highlight").GetComponent<Image>();

            downCharacter = GameObject.Find("Down_Character").GetComponent<Image>();
            downHighlight = GameObject.Find("Down_Highlight").GetComponent<Image>();

            leftCharacter = GameObject.Find("Left_Character").GetComponent<Image>();
            leftHighlight = GameObject.Find("Left_Highlight").GetComponent<Image>();

            rightCharacter = GameObject.Find("Right_Character").GetComponent<Image>();
            rightHighlight = GameObject.Find("Right_Highlight").GetComponent<Image>();
        }
        catch { }

        upHighlight.enabled = false;
        leftHighlight.enabled = false;
        rightHighlight.enabled = false;
        downHighlight.enabled = true;

        if(triangle != null)
        {
            upCharacter.sprite = triangle.hudIcon;
        }
        if (square != null)
        {
           leftCharacter.sprite = square.hudIcon;
        }
        if (circle != null)
        {
            rightCharacter.sprite = circle.hudIcon;
        }

        downCharacter.sprite = cross.hudIcon;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        currentChar.Start();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        currencyText.SetText("" + currency);

        if (stunTimer <= 0) { PlayerMovement(); }
        else { stunTimer -= Time.deltaTime; }

        LookDirection();
        AttackLogic();
        AnimationHandler();
        SwapLogic();
        DodgeLogic();


       //temp health testing
        if (Input.GetKeyDown(KeyCode.Z))
        {
            health--;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            health++;
        }

    }

    private void DodgeLogic()
    {
        //print("current dwt " + currentChar.currentDodgeTime);

        if (myPlayer.GetButtonDown("Dodge"))
        {
            if(currentChar.currentDodgeWaitTime < 0)
            {
                currentChar.currentDodgeWaitTime = currentChar.dodgeWaitTime + currentChar.dodgeLength;
                currentChar.currentDodgeTime = currentChar.dodgeLength;
            }
        }
    }

    private void SwapLogic()
    {
        if (myPlayer.GetButtonDown("Hold"))
        {
            isHolding = true;
        }
        if (myPlayer.GetButtonUp("Hold"))
        {
            isHolding = false;
        }

        if (isHolding)
        {
            if (myPlayer.GetButtonDown("Cross"))
            {
                currentChar = cross;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                downHighlight.enabled = true;
            }
            if (myPlayer.GetButtonDown("Square"))
            {
                currentChar = square;
                upHighlight.enabled = false;
                leftHighlight.enabled = true;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
            }
            if (myPlayer.GetButtonDown("Triangle"))
            {
                currentChar = triangle;
                upHighlight.enabled = true;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
            }
            if (myPlayer.GetButtonDown("Circle"))
            {
                currentChar = circle;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                rightHighlight.enabled = true;
                downHighlight.enabled = false;
            }
        }
    }

    private void AttackLogic()
    {
        if (currentChar.startCharging)
        {
            currentChar.currentChargeTimer += Time.deltaTime;
        }
        else
        {
            currentChar.currentChargeTimer = 0;
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
            case "cherry":
                anim.SetInteger("characterID", 4);
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
        speed = currentChar.speed * currentChar.currentDodgeSpeedMultiplier;
        currentChar.currentPosition = this.transform.position;

        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");

        if (!usingMouse)
        {
            mouseCursor.SetActive(false);
            direction.x = myPlayer.GetAxisRaw("DirectionHorizontal");
            direction.y = myPlayer.GetAxisRaw("DirectionVertical");
        }
        if (usingMouse)
        {
            mouseCursor.SetActive(true);
            direction = new Vector3((mouseCursor.transform.position.x - this.transform.position.x), (mouseCursor.transform.position.y - this.transform.position.y)).normalized;
        }
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
        if (currentChar.currentDodgeTime < 0)
        {
            health -= damage;
        }
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
            PowerUps temp = other.gameObject.GetComponent<PowerUps>();

            currentChar.speed *= temp.movementSpeed;
            currentChar.attackSize *= temp.attackSize;
            currentChar.attackSpeed *= temp.attackSpeed; //for melee
            currentChar.firerate *= temp.attackSpeed; //for projectiles
            currentChar.baseDamage *= temp.attackDamage;
            currentChar.maxDamage *= temp.attackDamage;
            health += temp.healAmount;

            Debug.Log(other.name);

            Destroy(other.gameObject);
        }
    }

}
