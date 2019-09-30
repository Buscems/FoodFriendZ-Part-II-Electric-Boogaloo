using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracking : MonoBehaviour
{
    Vector3 playerPos;

    public bool track;

    public bool follow;

    [Tooltip("How long we want the enemy to sit still and track the player")]
    public float trackTime;
    [Tooltip("How long we want the enemy to take to get to it's destination")]
    public float followTime;

    BaseEnemy baseEnemy;

    bool startLoop;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        startLoop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //only run this code when the enemy is actually aggroed to the player
        if (baseEnemy.aggroScript.aggro)
        {
            if (!startLoop)
            {
                Debug.Log("Yer");
                StartCoroutine(Tracker());
                startLoop = true;
            }
            if (track == true)
            {
                playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
                //Debug.Log("track is true");
            }
            if (follow == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerPos, baseEnemy.speed * Time.deltaTime);
                //Debug.Log("follow is true");
            }
        }
        else
        {
            ResetTrack();
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

    void ResetTrack()
    {
        StopAllCoroutines();
        track = false;
        follow = false;
        playerPos = this.transform.position;
        startLoop = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "TilesHere")
        {
            ResetTrack();
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
