using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tripmine : MonoBehaviour
{

    public float tripwireLength;
    public int damage;


    void Start()
    {

    } 

    
    void Update()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, tripwireLength);
        Debug.DrawRay(transform.position, transform.right, Color.red, tripwireLength);

        if(hit.collider.isTrigger)
        {
           
            if(hit.collider.gameObject.tag == "Player1" || hit.collider.gameObject.tag == "Player2")
            {
                Debug.Log("Ye");
                hit.collider.GetComponent<MainPlayer>().GetHit(damage);
            }
        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(damage);
            Destroy(this.gameObject);
        }
    }
}
