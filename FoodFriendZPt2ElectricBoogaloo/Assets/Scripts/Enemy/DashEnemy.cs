using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnemy : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPos;

    public bool track;

    public bool follow;

    public float trackTime;
    public float followTime;

    BaseEnemy baseEnemy;

    Vector3 direction;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (Tracker());
        baseEnemy = GetComponent<BaseEnemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (track == true){
            playerPos = player.transform.position;
            Debug.Log("track is true");
        }
        /*
        if (trackTimeStart >= maxTrackTime){
            track = false;
            follow = true;
            Vector2 playerCurPos = player.transform.position;

            if (followTimeStart >= stop){
                move.speed = 0f;
                follow = false;
                track = true;
                trackTimeStart = 0;
                followTimeStart = 0;
            }
        }
        */
    }
    
    private void FixedUpdate()
    {
        if (follow == true)
        {

            //transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
            Debug.Log("follow is true");
        }
    }

    IEnumerator Tracker(){
        track = true;
        yield return new WaitForSeconds(trackTime);
        track = false;
        StartCoroutine(Follow());
    }

    IEnumerator Follow(){
        follow = true;
        yield return new WaitForSeconds(followTime);
        follow = false;
        StartCoroutine(Tracker());
    }
}
