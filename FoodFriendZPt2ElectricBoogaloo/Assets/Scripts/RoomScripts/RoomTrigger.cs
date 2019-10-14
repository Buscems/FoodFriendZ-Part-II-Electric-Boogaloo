using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{

    [Tooltip("This is how many triggers need to be activated when the player gets into the room, to lock them in")]
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //needs fixing to add a second player
        if(collision.gameObject.tag == "Player1")
        {
            Debug.Log("YEr");
            for(int i = 0; i < doors.Length; i++)
            {
                doors[i].SetActive(true);
            }
        }
    }

}
