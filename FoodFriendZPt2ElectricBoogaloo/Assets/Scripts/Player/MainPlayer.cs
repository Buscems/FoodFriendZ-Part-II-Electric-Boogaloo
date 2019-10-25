using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using UnityEngine.SceneManagement;
using Rewired.ControllerExtensions;
using TMPro;

//with much love ~ Jesse <3
public class MainPlayer : MonoBehaviour
{
    //[ALL VARIABLES]
    #region [ALL VARIABLES]
    public int health;
    [HideInInspector] public int currency;

    #region Audio
    [Header("Audio")]
    public AudioSource audioSource;
    [Tooltip("These are the players sound clips")]

    public AudioClip[] clips;
    #endregion

    #region ReWired Variables
    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Tooltip("Turn on if the player is using the mouse")]
    public bool usingMouse;
    public GameObject mouseCursor;
    #endregion

    #region Direction Stuff
    Vector3 direction;
    public Transform attackDirection;
    #endregion

    #region Scripts
    [Header("Scripts")]
    //public scripts
    public BasePlayer currentChar;
    public GetOddsScript getOddsScript;

    //private scripts
    private CameraShake cam;
    #endregion

    [Header("Animator")]
    public Animator anim;

    #region Stats Hidden in the Inspector
    [HideInInspector] public float speed;

    #region Stat Multipliers ([MovementSpeed], [AttackAttributes])
    [HideInInspector]
    //movement
    public float speedMultiplier = 1;

    //attack
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

    #region Stun Timers
    [HideInInspector]
    public float stunTimer;
    public float maxStunTimer;
    #endregion

    #region Chance Based Stats
    float stunChance = 0;

    //elemental
    float burnChance = 0;
    #endregion

    #endregion

    #region rigid body stuff
    Rigidbody2D rb;
    Vector3 velocity;
    #endregion

    #region Scripts accessed by party members
    [Header("Current Party")]
    public BasePlayer triangle;
    public BasePlayer square;
    public BasePlayer circle;

    [HideInInspector]
    public BasePlayer cross;
    #endregion

    [Header("All Playable Characters")]
    public BasePlayer[] allCharacters;

    //Active Item??
    private bool isHolding = false;

    #region Character Displays
    private Image upCharacter;
    private Image upHighlight;

    private Image downCharacter;
    private Image downHighlight;

    private Image leftCharacter;
    private Image leftHighlight;

    private Image rightCharacter;
    private Image rightHighlight;
    #endregion

    #region puffs
    [Header("Effects - Puffs")]
    public GameObject dashPoof;
    public GameObject swapPuff;
    public GameObject walkPuff;
    #endregion
    #region Poof Timers
    [Header("Poof Timer")]
    public float maxPoofTime;
    private float currentPoofTimer;

    #endregion

    //Interactable Enviormental Prefabs
    #region Chest
    bool touchingChest;
    ChestScript currentChest;
    #endregion

    //[TEMPORARY]
    #region **TEMPORARY ELEMENTS
    [Header("**TEMPORARY ELEMENTS")]
    public TextMeshProUGUI youDiedText;
    #endregion

    public GameObject item = null;

    #endregion

    //[AWAKE METHOD]
    #region Awake METHOD
    private void Awake()
    {

        switch (PlayerPrefs.GetInt("startCharacter"))
        {
            case 1:
                currentChar = allCharacters[0];
                break;
            case 2:
                currentChar = allCharacters[1];
                break;
            case 3:
                currentChar = allCharacters[2];
                break;
            case 4:
                currentChar = allCharacters[3];
                break;
            case 5:
                currentChar = allCharacters[4];
                break;
            case 6:
                currentChar = allCharacters[5];
                break;
            case 7:
                currentChar = allCharacters[6];
                break;
            case 8:
                currentChar = allCharacters[7];
                break;
        }

        cross = currentChar;

        //Rewired Code
        #region ReWired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
        #endregion

        //camera
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        #region Character Compass
        upCharacter = GameObject.Find("Up_Character").GetComponent<Image>();
        upHighlight = GameObject.Find("Up_Highlight").GetComponent<Image>();

        downCharacter = GameObject.Find("Down_Character").GetComponent<Image>();
        downHighlight = GameObject.Find("Down_Highlight").GetComponent<Image>();

        leftCharacter = GameObject.Find("Left_Character").GetComponent<Image>();
        leftHighlight = GameObject.Find("Left_Highlight").GetComponent<Image>();

        rightCharacter = GameObject.Find("Right_Character").GetComponent<Image>();
        rightHighlight = GameObject.Find("Right_Highlight").GetComponent<Image>();

        upHighlight.enabled = false;
        leftHighlight.enabled = false;
        rightHighlight.enabled = false;

        //down is true
        downHighlight.enabled = true;
        #endregion

        #region [if null] statements
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
        #endregion

        //??
        downCharacter.sprite = cross.hudIcon;
    }
    #endregion

    //[START METHOD]
    #region Start METHOD
    void Start()
    {

        //poof timer
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

        //access current character stats & rb
        rb = GetComponent<Rigidbody2D>();

        currentChar.Start();
        
        //**temporary
        youDiedText.gameObject.SetActive(false);
    }
    #endregion

    //[EVERY FRAME]
    #region Every Frame Method

    //[update]
    void Update()
    {

        #region Pause Call
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
        #endregion

        #region if Player is ALIVE
        //if player is alive
        if (health > 0)
        {
            //if game is not paused
            if (Time.timeScale != 0)
            {
                //not stunned
                if (stunTimer <= 0)
                {
                    PlayerMovement();
                }

                //IF STUNNED, then reduce timer down
                else
                {
                    stunTimer -= Time.deltaTime;
                }

                #region All Methods Called
                //calls all ANIMATION/MOVEMENT METHODS
                LookDirection();
                AnimationHandler();

                //calls all LOGIC METHODS
                AttackLogic();
                SwapLogic();
                DodgeLogic();
                #endregion

                //[INTERACTIONS WITH OBJECTS]
                #region Chest
                //this is for interacting with a chest
                if (touchingChest && myPlayer.GetButtonDown("Cross"))
                {
                    touchingChest = false;
                    currentChest.OpenChest();
                }
                #endregion
            }

            if (Input.GetKeyDown(KeyCode.E) && item != null)
            {
                UseItem();
            }
        }
        #endregion

        #region if Player is DEAD
        //if player is dead
        else
        {
            //freeze time
            Time.timeScale = 0;

            #region [TEMPORARY (Game Over) CODE]
            //**temporary
            GetComponent<ScreenTransition>().fadeObject.color = new Color(0, 0, 0, 1);
            youDiedText.gameObject.SetActive(true);

            //**temporary - Load Dans Scene
            if (myPlayer.GetButtonDown("Cross"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Dans licc center");
            }
            #endregion
        }
        #endregion
    }

    //[fixedUpdate]
    private void FixedUpdate()
    {
        #region Movement Mechanics on Player
        //applied player movement
        Vector3 currentPos = transform.position;
        currentPos.z = 1;
        rb.MovePosition(currentPos + (velocity * (speed)) * Time.deltaTime);

        //applies the transformation
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        #endregion
    }
    #endregion

    //[LOGIC VOID METHODS]
    #region Every Logic Void METHOD
    private void DodgeLogic()
    {

        //if player presses (DODGE)
        if (myPlayer.GetButtonDown("Dodge"))
        {
            //dodge timer check
            if (currentChar.currentDodgeWaitTime < 0)
            {
                currentChar.currentDodgeWaitTime = currentChar.dodgeWaitTime + currentChar.dodgeLength;
                currentChar.currentDodgeTime = currentChar.dodgeLength;

                //dash effect
                Instantiate(dashPoof, transform.position, Quaternion.identity);
            }
        }
    }

    private void UseItem()
    {
        Instantiate(item, transform.position, Quaternion.Euler(attackDirection.transform.eulerAngles.x, attackDirection.transform.eulerAngles.y, attackDirection.transform.eulerAngles.z));
        item = null;
    }

    private void SwapLogic()
    {
        #region Button Pressed Detector
        //press
        if (myPlayer.GetButtonDown("Hold"))
        {
            isHolding = true;
        }

        //release
        if (myPlayer.GetButtonUp("Hold"))
        {
            isHolding = false;
        }
        #endregion

        //holding face buttons
        if (isHolding)
        {
            if (myPlayer.GetButtonDown("Cross") && currentChar != cross)
            {
                currentChar = cross;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                //down true
                downHighlight.enabled = true;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Square") && currentChar != square)
            {
                currentChar = square;
                upHighlight.enabled = false;
                //left true
                leftHighlight.enabled = true;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Triangle") && currentChar != triangle)
            {
                currentChar = triangle;
                //up true
                upHighlight.enabled = true;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Circle") && currentChar != circle)
            {
                currentChar = circle;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                //right true
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
                }
            }
            if (currentChar.attackType == BasePlayer.AttackType.Builder)
            {
                currentChar.Builder(transform.position, attackDirection, transform);
            }
        }
        if (currentChar.attackType == BasePlayer.AttackType.Ranged_Burst_Fire || currentChar.attackType == BasePlayer.AttackType.Napolean)
        {
            if (currentChar.firing)
            {
                currentChar.BurstFire(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }
        }


        if (myPlayer.GetButton("Attack") && currentChar.currentFirerateTimer < 0)
        {

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Basic)
            {
                currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Split_Fire)
            {
                currentChar.RangedSplit(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }
            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Burst_Fire || currentChar.attackType == BasePlayer.AttackType.Napolean)
            {
                currentChar.InitiateBurstFire();
            }
            if (currentChar.attackType == BasePlayer.AttackType.Boomerang)
            {
                currentChar.RangedBoomerang(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier);
            }
        }

        //if player PRESSES attack button
        if (myPlayer.GetButton("Attack"))
        {
            //chargable check
            if (currentChar.isChargable)
            {
                //[TEMP VARIABLES]
                float tempDamage = currentChar.baseDamage * baseDamageMulitplier;

                #region Charge Timer Mechanics
                if (currentChar.currentChargeTimer > currentChar.timeTillMaxDamage)
                {
                    //apply temp variable
                    tempDamage = currentChar.maxDamage * maxDamageMultiplier;

                    //(Melee Attacks)
                    if (currentChar.attackType == BasePlayer.AttackType.Melee && currentChar.currentAttackSpeedTimer < 0)
                    {
                        currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage);
                    }

                    //(Semi-Auto Attacks)
                    if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                    {
                        currentChar.RangedBasic(transform.position, attackDirection, transform, tempDamage);
                    }

                    //reset charge timer
                    currentChar.currentChargeTimer = 0;
                }
                #endregion                           
            }
        }

        //if player RELEASES attack button
        if (myPlayer.GetButtonUp("Attack"))
        {
            //chargible attribute check
            if (currentChar.isChargable)
            {
                //temp variables
                float tempDamage;

                #region Charge Timer Mechanics
                //if timer is hit
                if (currentChar.currentChargeTimer > currentChar.timeTillMaxDamage)
                {
                    tempDamage = currentChar.maxDamage * maxDamageMultiplier;
                }

                //if released [EARLY]
                else
                {
                    //damage based proportionally on time held before release
                    tempDamage = (currentChar.maxDamage * maxDamageMultiplier) * (currentChar.currentChargeTimer / currentChar.timeTillMaxDamage);
                }
                #endregion

                //(Melee Attacks)
                if (currentChar.attackType == BasePlayer.AttackType.Melee)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage);
                }

                //(Semi-Auto Attacks)
                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    currentChar.RangedBasic(transform.position, attackDirection, transform, tempDamage);
                }

                #region Reset "Charge" Attribute
                //reset timer
                currentChar.currentChargeTimer = 0;

                //turn off charge
                currentChar.startCharging = false;
                #endregion
            }
        }
    }

    void StartCharge()
    {
        currentChar.startCharging = true;
        currentChar.currentChargeTimer = 0;
    }
    #endregion

    //[MOVEMENT AND ANIMATION METHODS]
    #region Movenent and Animation Methods
    private void AnimationHandler()
    {
        //handles which character the player currently is, in terms of animation
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

        #region Direction of Animation
        //this will switch the animation of the current character
        if (direction == new Vector3(0, 0))
        {
            if (velocity.x > 0 && velocity.y < 0)
            {
                anim.SetFloat("Blend", 0);
            }
            else if (velocity.x < 0 && velocity.y < 0)
            {
                anim.SetFloat("Blend", 1);
            }
            else if (velocity.x > 0 && velocity.y > 0)
            {
                anim.SetFloat("Blend", 2);
            }
            else if (velocity.x < 0 && velocity.y > 0)
            {
                anim.SetFloat("Blend", 3);
            }
        }
        else
        {
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
        #endregion
    }

    private void PlayerMovement()
    {
        //updates current character
        currentChar.Update();

        //[APPLIES Multiplier to movementSpeed]
        speed = (currentChar.Mspeed * speedMultiplier) * currentChar.currentDodgeSpeedMultiplier;

        #region Character Movement Code
        //updates character position
        currentChar.currentPosition = this.transform.position;

        //velocity update
        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");
        #endregion

        //[IF STATEMENTS]
        #region [if statements]

        #region Poof [if] statements
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
        #endregion

        #region using Mouse [if] statements
        //not using mouse
        if (!usingMouse)
        {
            mouseCursor.SetActive(false);
            direction.x = myPlayer.GetAxisRaw("DirectionHorizontal");
            direction.y = myPlayer.GetAxisRaw("DirectionVertical");
        }

        //using mouse
        else if (usingMouse)
        {
            mouseCursor.SetActive(true);
            direction = new Vector3((mouseCursor.transform.position.x - this.transform.position.x), (mouseCursor.transform.position.y - this.transform.position.y)).normalized;
        }
        #endregion

        #region Checks if character is moving
        //check if character is moving
        if (velocity.x != 0)
        {
            currentChar.currentDirection.x = velocity.x;
        }
        if (velocity.y != 0)
        {
            currentChar.currentDirection.y = velocity.y;
        }
        #endregion
        #endregion
    }

    private void LookDirection()
    {
        if (direction == new Vector3(0, 0))
        {
            attackDirection.transform.right = velocity;
        }
        else
        {
            // [y]
            if (direction.y != 0)
            {
                attackDirection.transform.right = direction;
            }

            // [x]
            if (direction.x != 0)
            {
                attackDirection.transform.right = direction;
            }
        }
    }
    #endregion

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

        #region Item
        if (other.gameObject.tag == "Item")
        {
            if(item == null)
            {
                item = other.gameObject;
                Destroy(other.gameObject);
            }
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
            cam.StartShake();
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