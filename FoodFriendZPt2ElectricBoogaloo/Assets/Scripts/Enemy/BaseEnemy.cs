﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    ItemExtension ieScript;
    public GameObject splat;

    public AudioSource hitSound;

    [Header("Generic Enemy Values")]
    [Tooltip("How much health the enemy will have(This will be a high number for now so that the player can have high damage numbers")]
    public float health;
    [HideInInspector] public float maxHP;
    [Tooltip("How fast we want the enemy to move")]
    public float speed;
    private float origSpeed;
    [Tooltip("How much damage this enemy deals to the player when the player runs into them (Should only be between 0 and 1)")]
    [Range(0, 1)]
    public int walkIntoDamage;

    [HideInInspector]
    public Animator anim;

    //variables for other enemy scripts to reference
    public Aggro aggroScript;

    [Header("Item Drop Rate")]
    public GameObject[] items;

    [Tooltip("How often the enemy will actually drop an item \n \n The closer the number is to 1, the more likely it will be to drop an item")]
    [Range(0, 1)]
    public float dropRate;

    [Tooltip("How much money the enemy will drop when killed")]
    public int money;

    bool itemDrop;

    public GameObject objectToDestroy;

    private float bleedTimer = 0;
    private float bleedDamage;
    private float bleedTickRate = 0;
    private float currentBleedTickRate = 0;
    private float burnTimer = 0;
    private float burnDamage;
    private float poisonTimer = 0;
    private float poisonDamage;
    private float freezeTimer = 0;
    private float stunTimer = 0;

    float bufferScaleX = 0.1f;
    float bufferScaleY = 0.1f;

    public float originalScaleX = 1f;
    public float originalScaleY = 1f;

    public float changeTime;
    public BoxCollider2D hit;

    private float slowDownPercentage = 1;

    private SpriteRenderer sr;

    public AudioSource deathSound;
    public AudioClip[] deathClips;
    public GameObject deathSound2;

    public bool cantBreakObj;

    private void Awake()
    {
        //assign script
        ieScript = GameObject.Find("Player").GetComponent<ItemExtension>();
        
        maxHP = health;
    }

    // Start is called before the first frame update
    public void Start()
    {
        origSpeed = speed;
        //this is making sure that if there are any parents of the main object, it knows to destroy the parent so that nothing is left behind.
        //if(this.gameObject.transform.parent != null && this.gameObject.transform.parent.name != "ENEMIES")
        if (objectToDestroy == null)
        {
            objectToDestroy = this.gameObject;
        }

        aggroScript = GetComponent<Aggro>();

        aggroScript.aggro = false;

        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        else if (this.transform.parent.GetComponent<Animator>() != null)
        {
            anim = this.transform.parent.GetComponent<Animator>();
        }
        if (Random.Range(0f, 1f) <= dropRate)
        {
            //Debug.Log("Cool");
            itemDrop = true;
        }

        if (transform.name == "turret")
        {
            sr = transform.parent.GetComponent<SpriteRenderer>();
        }
        else
        {
            sr = GetComponent<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        try
        {
            if (anim != null && aggroScript.aggro)
            {
                AnimationHandler();
            }
        }
        catch { }

        try
        {
            sr.color = new Color(1, sr.color.g + 4f * Time.deltaTime, sr.color.b + 4f * Time.deltaTime);
        }
        catch { }
        speed = origSpeed * slowDownPercentage;
        StatusEffectTimers();
        StatusEffects();
    }

    private void StatusEffectTimers()
    {
        bleedTimer -= Time.deltaTime;
        currentBleedTickRate -= Time.deltaTime;
        burnTimer -= Time.deltaTime;
        poisonTimer -= Time.deltaTime;
        freezeTimer -= Time.deltaTime;
        stunTimer -= Time.deltaTime;
    }

    public void StatusEffects()
    {
        if (bleedTimer > 0)
        {
            if (currentBleedTickRate < 0)
            {
                currentBleedTickRate = bleedTickRate;
                health -= bleedDamage;
            }
        }

        if (burnTimer > 0)
        {
            health -= burnDamage;
        }

        if (poisonTimer > 0)
        {
            health -= poisonDamage;
        }

        if (freezeTimer < 0 && stunTimer < 0 && poisonTimer < 0)
        {
            slowDownPercentage = 1;
        }

    }

    public void SetBleed(float[] _bleedVariables)
    {
        bleedDamage = _bleedVariables[0];
        bleedTimer = _bleedVariables[1];
        bleedTickRate = _bleedVariables[2];
    }
    public void SetBurn(float[] _burnVariables)
    {
        burnDamage = _burnVariables[0];
        burnTimer = _burnVariables[1];
    }
    public void SetPoison(float[] _poisonVariables)
    {
        poisonDamage = _poisonVariables[0];
        poisonTimer = _poisonVariables[1];
        freezeTimer = _poisonVariables[1]; ;
        stunTimer = _poisonVariables[1];
        slowDownPercentage = _poisonVariables[2];
    }
    public void SetFreeze(float[] _freezeVariables)
    {
        freezeTimer = _freezeVariables[0];
        stunTimer = _freezeVariables[0];
        slowDownPercentage = _freezeVariables[1];
    }
    public void SetStun(float _stun)
    {
        stunTimer = _stun;
        freezeTimer = _stun;
        slowDownPercentage = 0;
    }

    void AnimationHandler()
    {
        Vector3 direction = (aggroScript.currentTarget.position - aggroScript.currentPos).normalized;

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

    private void LateUpdate()
    {
        if (health <= 0)
        {
            Death();
            aggroScript.target[0].GetComponent<MainPlayer>().currency += money;
            EndGameDataDisplay.moneyGenerated += money;
        }
    }

    void Death()
    {
        //This is the code for dropping an item
        if (itemDrop)
        {
            //enemies no longer drop items
            //Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
        }

        /*
        //if there is an animation for death
        if (anim != null)
        {
            //play the death animation
            anim.SetBool("Death", true);
        }
        if(anim == null)
        {
            DestroyThisObject();
        }
        */
        try
        {
            deathSound.clip = deathClips[Random.Range(0, deathClips.Length)];
            Instantiate(deathSound.gameObject, transform.position, Quaternion.identity);
        }
        catch { }
        EndGameDataDisplay.enemiesKilled += 1;
        Destroy(objectToDestroy);

    }

    public void TakeDamage(float _damage)
    {
            health -= _damage;
            sr.color = new Color(1, .1f, .1f);
        try
        {
            hitSound.Play();
        }
        catch
        {

        }
    }

    public void DestroyThisObject()
    {
        
        Destroy(objectToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

            if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
            {

            //StartCoroutine(bufferTime());
                //item extension script
                ieScript.bEScript_mAtkPlayer = this;
                ieScript.hasEnemyHitPlayer = true;

                if (walkIntoDamage == 1)
                {
                    collision.GetComponent<MainPlayer>().GetHit(walkIntoDamage);
                    EndGameDataDisplay.damageDealt += walkIntoDamage;
                }
            }
        
    }

IEnumerator bufferTime(){

        hit.size = new Vector2(bufferScaleX, bufferScaleY);
        yield return new WaitForSeconds(changeTime);
        hit.size = new Vector2(originalScaleX, originalScaleY);

    }

    private void OnDestroy()
    {
        try
        {
            if (health <= 0)
            {
                GameObject g = Instantiate(splat, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
                if (transform.parent.name.Contains("Turret") || transform.parent.name.Contains("SOUL"))
                {
                    g.transform.parent = transform.parent.parent.parent;
                }
                else
                {
                    g.transform.parent = transform.parent;
                }
            }
        }
        catch { }
        try
        {
            Instantiate(deathSound2, transform.position, Quaternion.identity);
        }
        catch { }
    }

}
