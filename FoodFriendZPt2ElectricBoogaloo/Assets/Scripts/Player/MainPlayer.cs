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
    #region All Variables
    public int health;
    [HideInInspector] public int currency;

    public bool isSlow;
    public bool isStuck;
    public bool isFast;

    [Header("Audio")]
    public AudioSource audioSource;
    [Tooltip("These are the players sound clips")]

    public AudioClip[] clips;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    [Header("Mouse Functionality")]
    [Tooltip("Turn on if the player is using the mouse")]
    public bool usingMouse;
    public GameObject mouseCursor;

    [Header("Direction for Controller")]
    [Tooltip("This is so the player sees the direction of attack on controller")]
    public GameObject pointer;
    Vector3 direction;
    public Transform attackDirection;

    [Header("Scripts")]
    //public scripts
    public BasePlayer currentChar;
    public GetOddsScript getOddsScript;

    //private scripts
    private CameraShake cam;

    [Header("Animator")]
    public Animator anim;

    [HideInInspector] public float speed;

    [HideInInspector]
    //movement
    public float speedMultiplier = 1;
    [HideInInspector]
    public float slowMultiplier = 1;

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
    [HideInInspector]
    public float critChanceMultiplier = 1;

    [HideInInspector]
    public float stunTimer;
    public float maxStunTimer;

    //chances
    public float evasiveChance = 0;

    Rigidbody2D rb;
    Vector3 velocity;

    [Header("Current Party")]
    public BasePlayer triangle;
    public BasePlayer square;
    public BasePlayer circle;

    [HideInInspector]
    public BasePlayer cross;

    [Header("All Playable Characters")]
    public BasePlayer[] allCharacters;

    //Active Item??
    private bool isHolding = false;

    private Image upCharacter;
    private Image upHighlight;

    private Image downCharacter;
    private Image downHighlight;

    private Image leftCharacter;
    private Image leftHighlight;

    private Image rightCharacter;
    private Image rightHighlight;

    [Header("Effects - Puffs")]
    public GameObject dashPoof;
    public GameObject swapPuff;
    public GameObject walkPuff;

    [Header("Poof Timer")]
    public float maxPoofTime;
    private float currentPoofTimer;

    //Interactable Enviormental Prefabs
    bool touchingChest;
    ChestScript currentChest;

    //[TEMPORARY]
    [Header("**TEMPORARY ELEMENTS")]
    public TextMeshProUGUI youDiedText;

    private GetOddsScript getOdds;
    private SpriteRenderer sr;
    private SaveGame saveData;

    //elements
    private float bleedMultiplier = 1;
    private float burnMultiplier = 1;
    private float poisonMultiplier = 1;
    private float freezeMultiplier = 1;
    private float stunMultiplier = 1;

    #endregion

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
            case 9:
                currentChar = allCharacters[8];
                break;
            case 10:
                currentChar = allCharacters[9];
                break;
            case 11:
                currentChar = allCharacters[10];
                break;
            case 12:
                currentChar = allCharacters[11];
                break;
            case 13:
                currentChar = allCharacters[12];
                break;
            case 14:
                currentChar = allCharacters[13];
                break;
            case 15:
                currentChar = allCharacters[14];
                break;
            case 16:
                currentChar = allCharacters[15];
                break;
            case 17:
                currentChar = allCharacters[16];
                break;
            case 18:
                currentChar = allCharacters[17];
                break;
        }

        cross = currentChar;

        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);

        //camera
        cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();

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

        //??
        downCharacter.sprite = cross.hudIcon;

        getOdds = GetComponent<GetOddsScript>();
        sr = GetComponent<SpriteRenderer>();
    }

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
        critChanceMultiplier = 1;
        #endregion

        Cursor.visible = false;

        //access current character stats & rb
        rb = GetComponent<Rigidbody2D>();

        currentChar.Start();

        //**temporary
        youDiedText.gameObject.SetActive(false);
        saveData = GetComponent<SaveGame>();
    }

    void Update()
    {
        //if the player is using the mouse turn the controller pointer off and vice versa
        /*
        if (usingMouse)
        {
            pointer.SetActive(false);
        }
        else
        {
            pointer.SetActive(true);
        }
        */

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

                //calls all ANIMATION/MOVEMENT METHODS
                LookDirection();
                AnimationHandler();

                //calls all LOGIC METHODS
                AttackLogic();
                SwapLogic();
                DodgeLogic();
                sr.color = new Color(1, sr.color.g + 10f * Time.deltaTime, sr.color.b + 10f * Time.deltaTime); ;

                //[INTERACTIONS WITH OBJECTS]
                //this is for interacting with a chest
                if (touchingChest && myPlayer.GetButtonDown("Cross"))
                {
                    touchingChest = false;
                    currentChest.OpenChest();
                }
            }


        }

        //if player is dead
        else
        {
            //freeze time
            Time.timeScale = 0;

            //**temporary
            GetComponent<ScreenTransition>().fadeObject.color = new Color(0, 0, 0, 1);
            youDiedText.gameObject.SetActive(true);

            //**temporary - Load Dans Scene
            if (myPlayer.GetButtonDown("Cross"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("Dans licc center");
            }
        }
    }

    private void FixedUpdate()
    {
        //applied player movement
        Vector3 currentPos = transform.position;
        currentPos.z = 1;
        rb.MovePosition(currentPos + (velocity * (speed)) * Time.deltaTime);

        //applies the transformation
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    //[LOGIC VOID METHODS]
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



    private void SwapLogic()
    {
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

        //holding face buttons
        if (isHolding)
        {
            if (myPlayer.GetButtonDown("Cross") && currentChar != cross && cross != null)
            {
                currentChar = cross;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                //down true
                downHighlight.enabled = true;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Square") && currentChar != square && square != null)
            {
                currentChar = square;
                upHighlight.enabled = false;
                //left true
                leftHighlight.enabled = true;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Triangle") && currentChar != triangle && triangle != null)
            {
                currentChar = triangle;
                //up true
                upHighlight.enabled = true;
                leftHighlight.enabled = false;
                rightHighlight.enabled = false;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
            if (myPlayer.GetButtonDown("Circle") && currentChar != circle && circle != null)
            {
                currentChar = circle;
                upHighlight.enabled = false;
                leftHighlight.enabled = false;
                //right true
                rightHighlight.enabled = true;
                downHighlight.enabled = false;
                currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);
                Instantiate(swapPuff, transform.position, Quaternion.identity);
            }
        }
    }

    public void CharacterSwap(BasePlayer _character, string _faceDirection)
    {
        if (_faceDirection == "Square" && square != null)
        {
            if (currentChar == square)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            square = _character;
            leftCharacter.sprite = square.hudIcon;
        }
        if (_faceDirection == "Triangle" && triangle != null)
        {
            if (currentChar == triangle)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            triangle = _character;
            upCharacter.sprite = triangle.hudIcon;
        }
        if (_faceDirection == "Cross" && cross != null)
        {
            if (currentChar == cross)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            cross = _character;
            downCharacter.sprite = cross.hudIcon;
        }
        if (_faceDirection == "Circle" && circle != null)
        {
            if (currentChar == circle)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            circle = _character;
            rightCharacter.sprite = circle.hudIcon;
        }
        currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);
    }

    public void AddCharacter(BasePlayer _character, string _faceDirection)
    {
        if (_faceDirection == "Square")
        {
            if (currentChar == square)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            square = _character;
            leftCharacter.sprite = square.hudIcon;
        }
        if (_faceDirection == "Triangle")
        {
            if (currentChar == triangle)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            triangle = _character;
            upCharacter.sprite = triangle.hudIcon;
        }
        if (_faceDirection == "Circle")
        {
            if (currentChar == circle)
            {
                Instantiate(swapPuff, transform.position, Quaternion.identity);
                currentChar = _character;
            }
            circle = _character;
            rightCharacter.sprite = circle.hudIcon;
        }
        currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);
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
            #region [[[TEMPORARY DO NOT DELETE ME!!]]]
            /*
            //[TEMPORARILY STORING HERE. DO NOT DELETE!!!]

            //[STUN]
            if (inflictStunChance > 0)
            {
                if (getOddsScript.getStunOdds(inflictStunChance))
                {
                    print("Enemy is [STUNED]");
                }
            }

            //[BLEED]
            if (inflictBleedChance > 0)
            {
                if (getOddsScript.getStunOdds(inflictBleedChance))
                {
                    print("Enemy is [BLEEDING]");
                }
            }

            //[BURN]
            if (inflictBurnChance > 0)
            {
                if (getOddsScript.getStunOdds(inflictBurnChance))
                {
                    print("Enemy is [BURNING]");
                }
            }

            //[POISON]
            if (inflictPoisonChance > 0)
            {
                if (getOddsScript.getStunOdds(inflictPoisonChance))
                {
                    print("Enemy is [POISONED]");
                }
            }

            //[FREEZE]
            if (inflictFreezeChance > 0)
            {
                if (getOddsScript.getStunOdds(inflictFreezeChance))
                {
                    print("Enemy is [FREEZING]");
                }
            }
            */
            #endregion


            if (currentChar.isChargable)
            {
                StartCharge();
            }
            else
            {
                if (currentChar.attackType == BasePlayer.AttackType.Melee && currentChar.currentAttackSpeedTimer < 0)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
                }
                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
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
                currentChar.BurstFire(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
            }
        }


        if (myPlayer.GetButton("Attack") && currentChar.currentFirerateTimer < 0)
        {

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Basic)
            {
                currentChar.RangedBasic(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
            }

            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Split_Fire)
            {
                currentChar.RangedSplit(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
            }
            if (currentChar.attackType == BasePlayer.AttackType.Ranged_Burst_Fire || currentChar.attackType == BasePlayer.AttackType.Napolean)
            {
                currentChar.InitiateBurstFire();
            }
            if (currentChar.attackType == BasePlayer.AttackType.Boomerang)
            {
                currentChar.RangedBoomerang(transform.position, attackDirection, transform, currentChar.baseDamage * baseDamageMulitplier, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
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

                if (currentChar.currentChargeTimer > currentChar.timeTillMaxDamage)
                {
                    //apply temp variable
                    tempDamage = currentChar.maxDamage * maxDamageMultiplier;

                    //(Melee Attacks)
                    if (currentChar.attackType == BasePlayer.AttackType.Melee && currentChar.currentAttackSpeedTimer < 0)
                    {
                        currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
                    }

                    //(Semi-Auto Attacks)
                    if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                    {
                        currentChar.RangedBasic(transform.position, attackDirection, transform, tempDamage, getOdds.GetStunOdds(currentChar.bleedChance * bleedMultiplier), getOdds.GetStunOdds(currentChar.burnChance * burnMultiplier), getOdds.GetStunOdds(currentChar.poisonChance * poisonMultiplier), getOdds.GetStunOdds(currentChar.freezeChance * freezeMultiplier), getOdds.GetStunOdds(currentChar.stunChance * stunMultiplier));
                    }

                    //reset charge timer
                    currentChar.currentChargeTimer = 0;
                }
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

                //(Melee Attacks)
                if (currentChar.attackType == BasePlayer.AttackType.Melee)
                {
                    currentChar.MeleeAttack(transform.position, attackDirection, transform, tempDamage, getOdds.GetStunOdds(currentChar.bleedChance), getOdds.GetStunOdds(currentChar.burnChance), getOdds.GetStunOdds(currentChar.poisonChance), getOdds.GetStunOdds(currentChar.freezeChance), getOdds.GetStunOdds(currentChar.stunChance));
                }

                //(Semi-Auto Attacks)
                if (currentChar.attackType == BasePlayer.AttackType.Ranged_Semi_Auto)
                {
                    currentChar.RangedBasic(transform.position, attackDirection, transform, tempDamage, getOdds.GetStunOdds(currentChar.bleedChance), getOdds.GetStunOdds(currentChar.burnChance), getOdds.GetStunOdds(currentChar.poisonChance), getOdds.GetStunOdds(currentChar.freezeChance), getOdds.GetStunOdds(currentChar.stunChance));
                }

                //reset timer
                currentChar.currentChargeTimer = 0;

                //turn off charge
                currentChar.startCharging = false;
            }
        }
    }

    void StartCharge()
    {
        currentChar.startCharging = true;
        currentChar.currentChargeTimer = 0;
    }

    //[MOVEMENT AND ANIMATION METHODS]
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
            case "taco":
                anim.SetInteger("characterID", 9);
                break;
            case "donut":
                anim.SetInteger("characterID", 10);
                break;
            case "hotdog":
                anim.SetInteger("characterID", 11);
                break;
            case "napoleon":
                anim.SetInteger("characterID", 12);
                break;
            case "blueberryMuffin":
                anim.SetInteger("characterID", 13);
                break;
            case "cone":
                anim.SetInteger("characterID", 14);
                break;
            case "lobsterTail":
                anim.SetInteger("characterID", 15);
                break;
            case "samosa":
                anim.SetInteger("characterID", 16);
                break;
            case "tunaCan":
                anim.SetInteger("characterID", 17);
                break;
        }

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
    }

    private void PlayerMovement()
    {
        //updates current character
        currentChar.Update();

        //[APPLIES Multiplier to movementSpeed]
        speed = (currentChar.Mspeed * speedMultiplier) * currentChar.currentDodgeSpeedMultiplier * slowMultiplier;

        if (isSlow == true)
        {
            slowMultiplier = .5f;
        }

        else if (isFast == true)
        {
            slowMultiplier = 1.5f;
        }

        else if (isStuck == true)
        {
            slowMultiplier = 0;
        }

        else
        {
            slowMultiplier = 1;
        }

        //updates character position
        currentChar.currentPosition = this.transform.position;

        //velocity update
        velocity.x = myPlayer.GetAxisRaw("MoveHorizontal");
        velocity.y = myPlayer.GetAxisRaw("MoveVertical");

        //[IF STATEMENTS]

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

        //check if character is moving
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

    //[REWIRED METHODS]
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

    //[COLLIDER METHODS]
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "StatBoost")
        {
            //applies all public variables on equipment to multipliers
            PowerUps temp = other.gameObject.GetComponent<PowerUps>();

            //if equipment has a bonus effect OR does more than just boost multipliers, it'll be implmented here
            StartCoroutine(temp.Pickup());

            speedMultiplier += temp.movementSpeed;
            attackSizeMultiplier += temp.attackSize;
            attackSpeedMultiplier += temp.attackSpeed;
            firerateMultiplier += temp.attackSpeed;
            baseDamageMulitplier += temp.attackDamage;
            maxDamageMultiplier += temp.attackDamage;
            bleedMultiplier *= temp.bleedChance;
            burnMultiplier *= temp.burnChance;
            poisonMultiplier *= temp.poisonChance;
            freezeMultiplier *= temp.freezeChance;
            stunMultiplier *= temp.stunChance;


            health += temp.healAmount;

            currentChar.SetMultipliers(attackSizeMultiplier, attackSpeedMultiplier, firerateMultiplier, baseDamageMulitplier, maxDamageMultiplier, critChanceMultiplier);



            //destroys item
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Slow")
        {
            StartCoroutine(Slow(3));
        }


        if (other.gameObject.tag == "Fast")
        {
            StartCoroutine(Fast(3));
        }


        if (other.gameObject.tag == "Stuck")
        {
            StartCoroutine(Stuck(3));
        }



        if (other.gameObject.tag == "Chest")
        {
            touchingChest = true;
            currentChest = other.gameObject.GetComponentInParent<ChestScript>();
        }
    }

    public IEnumerator Slow(float effectTime)
    {
        isSlow = true;
        yield return new WaitForSeconds(effectTime);
        isSlow = false;
    }
    public IEnumerator Fast(float effectTime)
    {
        isFast = true;
        yield return new WaitForSeconds(effectTime);
        isFast = false;
    }
    public IEnumerator Stuck(float effectTime)
    {
        isStuck = true;
        yield return new WaitForSeconds(effectTime);
        isStuck = false;
    }

    public void AddCharacterToSaveFile(string name)
    {
        saveData.Load();
        GameData gameData = saveData.gameData;
        for (int i = 0; i < gameData.CharacterListNames.Length; i++)
        {
            if (name.ToLower() == gameData.CharacterListNames[i].ToLower())
            {
                gameData.CharacterList[i] = true;
                break;
            }
        }
    }

    //getting hit method
    public void GetHit(int damage)
    {
               
        //[EVASIVENESS CHECK]
        //guarenteed hurt
        if (evasiveChance <= 0)
        {
            if (currentChar.currentDodgeTime < 0)
            {
                health -= damage;
                sr.color = new Color(1, .35f, .35f);
                audioSource.clip = clips[0];
                cam.StartShake();
            }
        }
        //is player has evasive chance
        else
        {

            //gets odds from odds script 
            if (getOddsScript.GetStunOdds(evasiveChance))
            //[SUCCESS]
            {
                print("LUCKY!!");
            }
            else
            //[FAILURE]
            {
                if (currentChar.currentDodgeTime < 0)
                {
                    health -= damage;
                    cam.StartShake();
                }
            }
        }
    }

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