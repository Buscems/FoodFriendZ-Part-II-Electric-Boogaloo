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
    [Tooltip("How much damage this enemy deals to the player when the player runs into them (Should only be between 0 and 1)")]
    [Range(0,1)]
    public int walkIntoDamage;

    Animator anim;

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
        else if(this.transform.parent.GetComponent<Animator>() != null)
        {
            anim = this.transform.parent.GetComponent<Animator>();
        }
        if(Random.Range(0f, 1f) <= dropRate)
        {
            Debug.Log("Cool");
            itemDrop = true;
        }

    }

    // Update is called once per frame
    public void Update()
    {
        if (anim != null && aggroScript.aggro)
        {
            AnimationHandler();
        }

    }

    void AnimationHandler()
    {
        Vector3 direction = (aggroScript.currentTarget.position - aggroScript.currentPos).normalized;

        //this will switch the animation of the current character
        if ( direction.x > 0 && direction.y < 0)
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
        }
    }

    void Death()
    {
        //This is the code for dropping an item
        if (itemDrop)
        {
            Instantiate(items[Random.Range(0, items.Length)], transform.position, Quaternion.identity);
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

        DestroyThisObject();

    }

    public void DestroyThisObject()
    {
        Destroy(objectToDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "PLayer2")
        {
            collision.GetComponent<MainPlayer>().GetHit(walkIntoDamage);
        }
    }

}
