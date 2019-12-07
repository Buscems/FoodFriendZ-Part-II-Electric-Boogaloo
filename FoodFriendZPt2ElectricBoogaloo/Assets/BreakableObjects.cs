using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObjects : MonoBehaviour
{
    public Animator obj;
    public BoxCollider2D collid;

    public GameObject debris1;
    public Transform spawn;
    

    // Start is called before the first frame update
    void Start()
    {
        collid.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1"){
            //obj.SetTrigger("Break");
            Destroy(gameObject);
            collid.enabled = false;
            Instantiate(debris1, transform.position, Quaternion.identity);
            
        }

        if (collision.gameObject.tag == "Projectile"){
            Destroy(collision.gameObject);
            //obj.SetTrigger("Break");
            collid.enabled = false;
            Instantiate(debris1, transform.position, Quaternion.identity);
            
        }

        if (collision.gameObject.tag == "Slash"){
            //obj.SetTrigger("Break");
            collid.enabled = false;
            Instantiate(debris1, transform.position, Quaternion.identity);
            
        }
    }
}
