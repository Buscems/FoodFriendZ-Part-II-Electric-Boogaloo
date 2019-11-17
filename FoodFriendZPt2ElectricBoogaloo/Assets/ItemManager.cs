using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    //[ALL VARIABLES]
    private MainPlayer mp;
    public PowerUps PowerUpScript;
    public GameObject item = null;

    public ParticleSystem itemParticleSystem;
    ParticleSystem refilledCDParticle;

    //timer stuff
    float MAXcurCDTimer;
    public float curCDTimer;    //recharge timer
    [HideInInspector] public float rechargeRateMultiplier = 1;

    public bool isRechargeRateDoubled;


    public float curEffectTimer;

    // ui stuff
    [HideInInspector] public Sprite itemSprite;

    Color white = new Color(1, 1, 1);
    Color red = new Color(1, 0, 0);
    Color black = new Color(0, 0, 0);

    #region [UI bar]
    //[UI]
    public Color readyCDcolor;
    public Color rechargingCDcolor;
    Slider CoolDownSlider;

    Image CoolDownFillBar;

    //to display item
    Image EquippedItemFrameIMG;
    Image curEquippedItemIMG;
    #endregion

    void Awake()
    {
        //make sure this enables
        this.enabled = true;

        //assigning stuff
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
        if (item != null || curEquippedItemIMG.sprite == null)
        {
            curEquippedItemIMG.sprite = itemSprite;


            //if ready
            if (CoolDownSlider.value == 1)
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
        else if (item == null)
        {
            curEquippedItemIMG.color = red;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (item != null && curCDTimer == 0)
            {
                UseItem(true);
                //particle effect
                itemParticleSystem.Play();
            }
            //if cooldown isn't ready yet
            else if (curCDTimer > 0)
            {
                print("Cannot Use Item Yet!!");
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
            //trickle timer down
            curCDTimer -= Time.deltaTime * rechargeRateMultiplier;

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

    //[METHODS]
    private void UseItem(bool _isEffectActive)
    {
        //
        PowerUpScript.effectIsActive = _isEffectActive;

        //use powerup script
        StartCoroutine(PowerUpScript.Pickup());

        //set timers
        if (_isEffectActive)
        {
            curEffectTimer = PowerUpScript.effectDuration;
            curCDTimer = PowerUpScript.maxCoolDownDuration;
        }

        else
        {
            curCDTimer = PowerUpScript.maxCoolDownDuration;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            //if the player currently possesses no active items
            if (item == null)
            {
                item = other.gameObject;
                PowerUpScript = other.gameObject.GetComponent<PowerUps>();

                MAXcurCDTimer = PowerUpScript.maxCoolDownDuration;
                curCDTimer = 0;
            }
            else
            {
                Debug.Log("Player already has an item");
            }


        }
    }
}
