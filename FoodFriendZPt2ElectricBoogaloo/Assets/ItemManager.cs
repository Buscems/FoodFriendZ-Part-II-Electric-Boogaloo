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

    //timer stuff
    float MAXcurCDTimer;
    public float curCDTimer;

    public float curEffectTimer;


    //[UI]
    public Slider CoolDownSlider;
    public Image CoolDownFillBar;

    public Color readyCDcolor;
    public Color rechargingCDcolor;

    //[START]
    void Start()
    {
        mp = GetComponent<MainPlayer>();

        //FOR DEBUGGING
        if (item != null)
        {
            PowerUpScript = item.GetComponent<PowerUps>();
        }
    }

    void Update()
    {
        #region SliderMechanics
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

        else if (curCDTimer > 0)
        {
            //trickle timer down
            curCDTimer -= Time.deltaTime;

            //when timer hits
            if (curCDTimer <= 0)
            {
                //resets timer
                curCDTimer = 0;
            }
        }
    }

    //[METHODS]
    private void UseItem(bool _isEffectActive)
    {
        PowerUpScript.effectIsActive = _isEffectActive;

        // Instantiate(item, transform.position, Quaternion.Euler(mp.attackDirection.transform.eulerAngles.x, mp.attackDirection.transform.eulerAngles.y, mp.attackDirection.transform.eulerAngles.z));

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

    private void OnTriggerEnter2D(Collider2D other)
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
