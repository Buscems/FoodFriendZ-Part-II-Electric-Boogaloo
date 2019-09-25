using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BaseEnemy : MonoBehaviour
{

    [Header("Generic Enemy Values")]
    [Tooltip("How much health the enemy will have(This will be a high number for now so that the player can have high damage numbers")]
    public float health;
    [Tooltip("How fast we want the enemy to move")]
    public float speed;

    Animator anim;

    //variables for other enemy scripts to reference
    [HideInInspector]
    public Aggro aggroScript;

    [Header("Item Drop Rate")]
    public GameObject[] items;

    [Tooltip("How often the enemy will actually drop an item \n \n The closer the number is to 0, the more likely it will be to drop an item")]
    public float dropRate;

    [Tooltip("How much money the enemy will drop when killed")]
    public float money;

    bool itemDrop;

    // Start is called before the first frame update
    public void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        itemDrop = false;
        if(Random.Range(0, dropRate) == 0)
        {
            itemDrop = true;
        }

        //referencing any scripts that other enemies might need to
        aggroScript = GetComponent<Aggro>();

    }

    // Update is called once per frame
    public void Update()
    {

        if(health <= 0)
        {
            Death();
        }    

    }

    void Death()
    {
        //This is the code for dropping an item
        if (itemDrop)
        {
            Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
        }

        //if there is an animation for death
        if (anim != null)
        {
            //play the death animation
            anim.SetBool("Death", true);
        }
        if(anim == null)
        {
            Destroy(this.gameObject);
        }

    }
}
