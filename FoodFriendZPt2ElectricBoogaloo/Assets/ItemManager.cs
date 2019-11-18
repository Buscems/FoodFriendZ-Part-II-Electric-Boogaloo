using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using Rewired.ControllerExtensions;

public class ItemManager : MonoBehaviour
{
    //[ALL VARIABLES]
    //scripts
    private MainPlayer mp;
    public PowerUps PowerUpScript;

    //active item
    public GameObject item = null;

    //particle effects
    public ParticleSystem itemParticleSystem;
    ParticleSystem refilledCDParticle;

    //timer stuff
    float MAXcurCDTimer;
    public float curCDTimer;    //recharge timer
    public float curEffectTimer;

    [HideInInspector] public float rechargeRateMultiplier = 1;
    [HideInInspector] public bool isRechargeRateDoubled;

    //[UI STUFF]
    //colors
    Color white = new Color(1, 1, 1);
    Color red = new Color(1, 0, 0);
    Color black = new Color(0, 0, 0);

    //cool down bar
    public Color readyCDcolor;
    public Color rechargingCDcolor;
    Slider CoolDownSlider;
    Image CoolDownFillBar;

    //item display
    [HideInInspector] public Sprite itemSprite;
    Image EquippedItemFrameIMG;
    Image curEquippedItemIMG;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Header("Rewired")]
    [Tooltip("Number identifier for each player, must be above 0")]
    public int playerNum;

    void Awake()
    {
        //make sure this enables
        this.enabled = true;

        //Rewired Code
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);

        //assigning scripts
        mp = GetComponent<MainPlayer>();

        //assignning UI
        CoolDownSlider = GameObject.Find("CoolDownBar").GetComponent<Slider>();
        refilledCDParticle = GameObject.Find("temp_refillParticles").GetComponent<ParticleSystem>();
        CoolDownFillBar = GameObject.Find("CDFill").GetComponent<Image>();

        curEquippedItemIMG = GameObject.Find("curItemEquipedIMG").GetComponent<Image>();
        EquippedItemFrameIMG = GameObject.Find("EquippedItemFrame").GetComponent<Image>();
    }

    void Start()
    {
        //FOR DEBUGGING
        if (item != null)
        {
            PowerUpScript = item.GetComponent<PowerUps>();
        }
    }

    void Update()
    {
        #region [Item Display Mechanics]
        //[DISPLAY ITEM]
        if (item != null)
        {
            //if ready
            if (CoolDownSlider.value >= 1)
            {
                //white for actual colors
                curEquippedItemIMG.color = white;
                EquippedItemFrameIMG.color = white;
            }
            //if not ready
            else
            {
                curEquippedItemIMG.color = black;
                EquippedItemFrameIMG.color = red;
            }
        }
        //if player has nothing
        else
        {
            curEquippedItemIMG.sprite = null;
            EquippedItemFrameIMG.color = red;
        }
        #endregion

        #region Slider Color Mechanics
        CoolDownSlider.value = 1 - (curCDTimer / MAXcurCDTimer);

        //effects color of the slider
        if (CoolDownSlider.value < 1)
        {
            CoolDownFillBar.color = rechargingCDcolor;
        }
        //if the bar is full
        else
        {
            CoolDownFillBar.color = readyCDcolor;
        }
        #endregion

        //check if player can use the item

        //[use ACTIVE ITEM button]
        if (item != null)
        {
            //active item button
            if (myPlayer.GetButtonDown("UseItem"))
            {
                if (curCDTimer == 0)
                {
                    UseItem(true);

                    //particle effect
                    itemParticleSystem.Play();
                }

                //cooldown isn't ready
                else if (curCDTimer > 0)
                {
                    print("Cannot Use Item Yet!!");
                }
            }
        }

        //cooldown timer check
        if (curEffectTimer > 0)
        {
            curEffectTimer -= Time.deltaTime;

            //when timer hits
            if (curEffectTimer <= 0)
            {
                curEffectTimer = 0;
                UseItem(false);
            }
        }

        //recharging
        else if (curCDTimer > 0)
        {
            curCDTimer -= Time.deltaTime * rechargeRateMultiplier;

            //double condition
            if (isRechargeRateDoubled)
            {
                curCDTimer -= Time.deltaTime * rechargeRateMultiplier;
            }

            //when timer hits
            if (curCDTimer <= 0)
            {
                refilledCDParticle.Play();

                //resets timer
                curCDTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            //if player has no item
            if (item == null)
            {
                AssignItem(other.gameObject);
            }

            //if player has an item
            else if (item != null)
            {
                //swapping items
                GameObject prevItem = item;

                //places item in game world
                prevItem.transform.position = this.gameObject.transform.position + new Vector3(2, 0, 0);
                prevItem.GetComponent<SpriteRenderer>().enabled = true;
                prevItem.GetComponent<BoxCollider2D>().enabled = true;

                AssignItem(other.gameObject);

                //for debugging
            }
        }
    }

    //[METHODS]
    void UseItem(bool _isEffectActive)
    {
        //assign arguments
        PowerUpScript.effectIsActive = _isEffectActive;

        //use powerup script
        StartCoroutine(PowerUpScript.Pickup());

        //set timers
        if (_isEffectActive)
        {
            curEffectTimer = PowerUpScript.effectDuration;
            curCDTimer = PowerUpScript.maxCoolDownDuration;
        }
    }

    void AssignItem(GameObject _item)
    {
        //access item componenets
        item = _item;
        PowerUpScript = _item.GetComponent<PowerUps>();

        curEquippedItemIMG.sprite = _item.GetComponent<SpriteRenderer>().sprite;

        //make invisible
        _item.GetComponent<SpriteRenderer>().enabled = false;
        _item.GetComponent<BoxCollider2D>().enabled = false;

        MAXcurCDTimer = PowerUpScript.maxCoolDownDuration;
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

}
