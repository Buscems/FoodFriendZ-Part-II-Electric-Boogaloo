using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void Awake()
    {
        //make sure this enables
        this.enabled = true;

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
        if (item != null || curEquippedItemIMG.sprite == null)
        {
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

            curEquippedItemIMG.sprite = itemSprite;
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
        if (item != null)
        {
            //active item button
            if (Input.GetKeyDown(KeyCode.E))
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            //if player has no item
            if (item == null)
            {
                //access item componenets
                item = other.gameObject;
                PowerUpScript = other.gameObject.GetComponent<PowerUps>();

                MAXcurCDTimer = PowerUpScript.maxCoolDownDuration;
            }
            //if player has an item
            else if (item != null)
            {
                Debug.Log("swapping items");

                GameObject prevItem = item;

                //places item in game world
                prevItem.transform.position = this.gameObject.transform.position + new Vector3(2, 0, 0);
                prevItem.GetComponent<SpriteRenderer>().enabled = true;
                prevItem.GetComponent<BoxCollider2D>().enabled = true;

                //access item components
                item = other.gameObject;
                PowerUpScript = other.gameObject.GetComponent<PowerUps>();

                MAXcurCDTimer = PowerUpScript.maxCoolDownDuration;
            }
        }
    }
}
