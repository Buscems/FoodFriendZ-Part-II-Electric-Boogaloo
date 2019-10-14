using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{

    [Header("Generic Enemy Values")]
    [Tooltip("How much health the enemy will have(This will be a high number for now so that the player can have high damage numbers")]
    public float maxHealth;
    [HideInInspector]
    public float health;
    [HideInInspector]
    public float healthPercent;
    [Tooltip("How fast we want the enemy to move")]
    public float speed;
    [Tooltip("How much damage this enemy deals to the player when the player runs into them (Should only be between 0 and 1)")]
    [Range(0, 1)]
    public int walkIntoDamage;

    Animator anim;

    [Tooltip("How much money the boss will drop when killed")]
    public int money;

    [Tooltip("This is how many doors need to be opened after the boss dies")]
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //keeping track of the percentage of the bosses health to have different stages
        healthPercent = health / maxHealth;

    }

    public void Death()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

}
