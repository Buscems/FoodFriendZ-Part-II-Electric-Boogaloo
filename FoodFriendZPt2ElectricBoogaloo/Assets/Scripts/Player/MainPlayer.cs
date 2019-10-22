using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;
using Rewired.ControllerExtensions;
using TMPro;

public class MainPlayer : MonoBehaviour
{
    public int health;
    [HideInInspector]
    public int currency;

    #region Audio
    [Header("Audio")]
    public AudioSource audioSource;
    [Tooltip("These are the players sound clips")]
    /*
     * 1 = deathSound
     */
    public AudioClip[] clips;
    #endregion

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Tooltip("Turn on if the player is using the mouse")]
    public bool usingMouse;
    public GameObject mouseCursor;

    Vector3 direction;

    public Transform attackDirection;

    public BasePlayer currentChar;

    public GetOddsScript getOddsScript;
    public Animator anim;

    #region Stats Hidden in the Inspector
    [HideInInspector]
    public float speed;

    #region Stat Multiplier
    [HideInInspector]
    public float speedMultiplier = 1;

    [HideInInspector]
    public float attackSizeMultiplier = 1;
    [HideInInspector]
    public float attackSpeedMultiplier = 1;
    [HideInInspector]
    public float firerateMultiplier = 1;
    [HideInInspector]
    public float baseDamageMulitplier = 1;
    [HideInInspector]
    public float maxDamageMultiplier = 1;
    #endregion

    [HideInInspector]
    public float stunTimer;
    public float maxStunTimer;

    //chance based stats
    float stunChance = 0;
    float burnChance = 0;
    #endregion

    Rigidbody2D rb;
    Vector3 velocity;

    private CameraShake cam;

    [Header("Current Party")]
    public BasePlayer triangle;
    public BasePlayer square;
    public BasePlayer circle;
    [HideInInspector]
    public BasePlayer cross;

    [Header("All Playable Characters")]
    public BasePlayer[] allCharacters;

    private bool isHolding = false;

    public GameObject dashPoof;
    public GameObject swapPuff;
    public GameObject walkPuff;

    private Image upCharacter;
    private Image upHighlight;

    private Image downCharacter;
    private Image downHighlight;

    private Image leftCharacter;
    private Image leftHighlight;

    private Image rightCharacter;
    private Image rightHighlight;

    public float maxPoofTime;
    private float currentPoofTimer;

    bool touchingChest;
    ChestScript currentChest;

    //temporary
    public TextMeshProUGUI youDiedText;

    #region Awake METHOD
    private void Awake()
    {
        cross = currentChar;
        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);

        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        // try
        // {
        upCharacter = GameObject.Find("Up_Character").GetComponent<Image>();
        upHighlight = GameObject.Find("Up_Highlight").GetComponent<Image>();

        downCharacter = GameObject.Find("Down_Character").GetComponent<Image>();
        downHighlight = GameObject.Find("Down_Highlight").GetComponent<Image>();

        leftCharacter = GameObject.Find("Left_Character").GetComponent<Image>();
        leftHighlight = GameObject.Find("Left_Highlight").GetComponent<Image>();

        rightCharacter = GameObject.Find("Right_Character").GetComponent<Image>();
        rightHighlight = GameObject.Find("Right_Highlight").GetComponent<Image>();
        //  }
        //  catch { }

        upHighlight.enabled = false;
        leftHighlight.enabled = false;
        rightHighlight.enabled = false;
        downHighlight.enabled = true;

        if (triangle != null)
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
    #endregion

    //[START METHOD]
    #region Start METHOD
    void Start()
    {
        currentPoofTimer = maxPoofTime;

        #region Set Multipliers to 1
        speedMultiplier = 1;

        attackSizeMultiplier = 1;
        attackSpeedMultiplier = 1;
        firerateMultiplier = 1;
        baseDamageMulitplier = 1;
        maxDamageMultiplier = 1;
        #endregion

        Cursor.visible = false;
        currentChar.Start();
        rb = GetComponent<Rigidbody2D>();

        youDiedText.gameObject.SetActive(false);
    }
    #endregion

    //[EVERY FRAME]
    #region Every Frame Method
    void Update()
    {
        if (myPlayer.GetButtonDown("Pause"))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else
            {
                Time.timeScale = 0;
            }
        }

        #region if Player is ALIVE
        //if player is alive
        if (health > 0)
        {
            //if game is not paused
            if (Time.timeScale != 0)
            {
                if (stunTimer <= 0) { PlayerMovement(); }
                else { stunTimer -= Time.deltaTime; }

                LookDirection();
                AttackLogic();
                AnimationHandler();
                SwapLogic();
                DodgeLogic();

                //this is for interacting with a chest
                if (touchingChest && myPlayer.GetButtonDown("Cross"))
                {
                    currentChest.OpenChest();
                }

            }
        }
        #endregion

        #region if Player is DEAD
        //if player is dead
        else
        {
            Time.timeScale = 0;

            GetComponent<ScreenTransition>().fadeObject.color = new Color(0, 0, 0, 1);
            youDiedText.gameObject.SetActive(true);

            if (myPlayer.GetButtonDown("Cross"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Dans licc center");
            }
        }
        #endregion
    }

    private void FixedUpdate()
    {
        //applied player movement
        Vector3 currentPos = transform.position;
        currentPos.z = 1;
        rb.MovePosition(currentPos + (velocity * (speed)) * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
    #endregion

    //[LOGIC VOID METHODS]
    #region Every Logic Void METHOD
    private void DodgeLogic()
    {
        //print("current dwt " + currentChar.currentDodgeTime);

        if (myPlayer.GetButtonDown("Dodge"))
        {
            if (currentChar.currentDodgeWaitTime < 0)
            {
                currentChar.currentDodgeWaitTime = currentChar.dodgeWaitTime + currentChar.dodgeLength;
                currentChar.currentDodgeTime = currentChar.dodgeLength;
                Instantiate(dashPoof, transform.position, Quaternion.identity);
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
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Square"))
            {
                currentChar = square;
                upHighlight.enabled = false;
                leftHighlight.enabled = true;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Triangle"))
            {
                currentChar = triangle;
                upHighlight.enabled = true;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Circle"))
            {
                currentChar = circle;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                rightHighlight.enabled = true;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
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
                if (currentChar.attackType == BasePlayer.AttackType.Melee && currentChar.currentAttackSpeedTimer < 0)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
                }
                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
                    audioSource.clip = clips[0];
                    audioSource.Play();
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
                audioSource.clip = clips[0];
                audioSource.Play();
                currentChar.BurstFire(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }
        }

        if (myPlayer.GetButton("Attack") && currentChar.currentFirerateTimer < 0)
        {

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Basic)
            {
                audioSource.clip = clips[0];
                audioSource.Play();
                currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Split_Fire)
            {
                audioSource.clip = clips[0];
                audioSource.Play();
                currentChar.RangedSplit(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }
            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Burst_Fire)
            {
                audioSource.clip = clips[0];
                audioSource.Play();
                currentChar.InitiateBurstFire();
            }
            if (currentChar.attackType == BasePlayer.AttackType.Boomerang)
            {
                audioSource.clip = clips[0];
                audioSource.Play();
                currentChar.RangedBoomerang(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }
        }

        print(currentChar.attackType);

        if (myPlayer.GetButton("Attack"))
        {
            if (currentChar.isChargable)
            {
                //print(currentChar.currentChargeTimer);
                float tempDamage = currentChar.baseDamage * baseDamageMulitplier;
                if (currentChar.currentChargeTimer > currentChar.timeTillMaxDamage)
                {
                    tempDamage = currentChar.maxDamage * maxDamageMultiplier;

                    if (currentChar.attackType == BasePlayer.AttackType.Melee && currentChar.currentAttackSpeedTimer < 0)
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
                    tempDamage = currentChar.maxDamage * maxDamageMultiplier;
                }
                else
                {
                    tempDamage = (currentChar.maxDamage * maxDamageMultiplier) * (currentChar.currentChargeTimer / currentChar.timeTillMaxDamage);
                }


                if (currentChar.attackType == BasePlayer.AttackType.Melee)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage);
                }

                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    audioSource.clip = clips[0];
                    audioSource.Play();
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
    #endregion

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
            case "cannoli":
                anim.SetInteger("characterID", 5);
                break;
            case "burger":
                anim.SetInteger("characterID", 6);
                break;
            case "sashimi":
                anim.SetInteger("characterID", 7);
                break;
            case "fries":
                anim.SetInteger("characterID", 8);
                break;
        }

        //this will switch the animation of the current character
        if (direction.x > 0 && direction.y < 0)
        {
            anim.SetFloat("Blend", 0);
        }
        else if (direction.x < 0 && direction.y < 0)
        {
            anim.SetFloat("Blend", 1);
        }
        else if (direction.x > 0 && direction.y > 0)
        {
            anim.SetFloat("Blend", 2);
        }
        else if (direction.x < 0 && direction.y > 0)
        {
            anim.SetFloat("Blend", 3);
        }
    }

    private void PlayerMovement()
    {
        currentChar.Update();
        speed = (currentChar.speed * speedMultiplier) * currentChar.currentDodgeSpeedMultiplier;
        currentChar.currentPosition = this.transform.position;

        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");

        if (velocity.x > .3f || velocity.y > .3f || velocity.x < -.3f || velocity.y < -.3f)
        {
            currentPoofTimer -= Time.deltaTime;

            if (currentPoofTimer < 0)
            {
                GameObject poof = Instantiate(walkPuff, transform.position, Quaternion.identity);
                Vector3 size = poof.transform.eulerAngles / 3;
                poof.transform.eulerAngles = size;
                currentPoofTimer = maxPoofTime;
            }
        }

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


    //[REWIRED METHODS]
    #region Every Rewired Method
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

    #endregion

    //[COLLIDER METHODS]
    #region Collider METHODS
    private void OnTriggerEnter2D(Collider2D other)
    {
        #region Stat Boosters
        if (other.gameObject.tag == "StatBoost")
        {
            PowerUps temp = other.gameObject.GetComponent<PowerUps>();

            speedMultiplier += temp.movementSpeed;
            attackSizeMultiplier += temp.attackSize;
            attackSpeedMultiplier += temp.attackSpeed;
            firerateMultiplier += temp.attackSpeed;
            baseDamageMulitplier += temp.attackDamage;
            maxDamageMultiplier += temp.attackDamage;
            health += temp.healAmount;

            currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
            Debug.Log(other.name);

            Destroy(other.gameObject);
        }
        #endregion

        #region Chest
        if (other.gameObject.tag == "Chest")
        {
            touchingChest = true;
            currentChest = other.gameObject.GetComponentInParent<ChestScript>();
        }
        #endregion
    }

    //getting hit method
    public void GetHit(int damage)
    {
        if (currentChar.currentDodgeTime < 0)
        {
            health -= damage;
        }
    }
    #endregion

    //[RETURN METHODS]
    public bool HitEnemy(string tag)
    {
        if (tag == "Projectile")
        {
            return true;
        }
        else
        {
            cam.StartShake();
            return false;
        }
    }
}