using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            collision.GetComponent<MainPlayer>().GetHit(1);
        }
    }

    public void EndAttack()
    {
        Destroy(this.gameObject);
    }

}
