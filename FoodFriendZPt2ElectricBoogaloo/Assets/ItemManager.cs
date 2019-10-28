using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    //[ALL VARIABLES
    private MainPlayer mp;
    public PowerUps PowerUpScript;
    public GameObject item = null;

    //timer stuff
    public float curTimer;


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
        //check if player can use the item
        //[use ACTIVE ITEM button]
        if (Input.GetKeyDown(KeyCode.E) && item != null && curTimer == 0)
        {
            UseItem();
        }
        else if (curTimer > 0)
        {
            print("Cannot Use Item Yet!!");
        }

        //cooldown timer check
        if (curTimer > 0)
        {
            //trickle timer down
            curTimer -= Time.deltaTime;

            //when timer hits
            if (curTimer <= 0)
            {
                //resets timer
                curTimer = 0;
            }
        }
    }

    //[METHODS]
    private void UseItem()
    {
        Instantiate(item, transform.position, Quaternion.Euler(mp.attackDirection.transform.eulerAngles.x, mp.attackDirection.transform.eulerAngles.y, mp.attackDirection.transform.eulerAngles.z));

        //set timer
        curTimer = PowerUpScript.maxCoolDownDuration;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            if (item == null)
            {
                item = other.gameObject;
                Destroy(other.gameObject);

                //retrieves the instance of the power up script on the power up grabbed
                PowerUpScript = other.gameObject.GetComponent<PowerUps>();
            }
        }
    }
}
