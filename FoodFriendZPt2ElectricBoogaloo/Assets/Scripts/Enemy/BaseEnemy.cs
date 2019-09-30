using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{

    [Header("Generic Enemy Values")]
    [Tooltip("How much health the enemy will have(This will be a high number for now so that the player can have high damage numbers")]
    public float health;
    [Tooltip("How fast we want the enemy to move")]
    public float speed;

    Animator anim;

    //variables for other enemy scripts to reference
    public Aggro aggroScript;

    [Header("Item Drop Rate")]
    public GameObject[] items;

    [Tooltip("How often the enemy will actually drop an item \n \n The closer the number is to 0, the more likely it will be to drop an item")]
    public float dropRate;

    [Tooltip("How much money the enemy will drop when killed")]
    public float money;

    bool itemDrop;

    GameObject objectToDestroy;

    // Start is called before the first frame update
    public void Start()
    {
        //this is making sure that if there are any parents of the main object, it knows to destroy the parent so that nothing is left behind.
        if(this.gameObject.transform.parent != null)
        {
            objectToDestroy = this.gameObject.transform.parent.gameObject;
        }
        else
        {
            objectToDestroy = this.gameObject;
        }

        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        itemDrop = false;
        if(Random.Range(0, dropRate) == 0)
        {
            //itemDrop = true;
        }

    }

    // Update is called once per frame
    public void Update()
    {

            

    }

    private void LateUpdate()
    {
        if (health <= 0)
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
            DestroyThisObject();
        }

    }

    public void DestroyThisObject()
    {
        Destroy(objectToDestroy);
    }

}
