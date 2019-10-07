using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{

    public MainPlayer player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<MainPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player1"){
            player = gameObject.GetComponent<MainPlayer>();

        }
    }
}
