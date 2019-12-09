using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired.ControllerExtensions;
using TMPro;
using UnityEngine.UI;

public class ShopTrigger : MonoBehaviour
{

    public int price;
    public GameObject powerUp;
    public GameObject priceText;
    public bool looking;
    public PowerUps power;
    public MainPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        priceText.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player1").GetComponent<MainPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.currency >= price && Input.GetKeyDown(KeyCode.E) && looking == true)
        {
            Debug.Log("woot");
            player.speedMultiplier += power.movementSpeed;
            player.attackSizeMultiplier += power.attackSize;
            player.attackSpeedMultiplier += power.attackSpeed;
            player.firerateMultiplier += power.attackSpeed;
            player.baseDamageMulitplier += power.attackDamage;
            player.maxDamageMultiplier += power.attackDamage;
            player.health += power.healAmount;

            player.currentChar.SetMultipliers(player.attackSizeMultiplier, player.attackSpeedMultiplier, player.firerateMultiplier, player.baseDamageMulitplier, player.maxDamageMultiplier, player.critChanceMultiplier);
            player.currency -= price;
            Destroy(gameObject);
            Destroy(powerUp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1")
        {
            Debug.Log("yes");
            priceText.SetActive(true);
            looking = true;
        }
            
        }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        priceText.SetActive(false);
        looking = false;
    }
}
