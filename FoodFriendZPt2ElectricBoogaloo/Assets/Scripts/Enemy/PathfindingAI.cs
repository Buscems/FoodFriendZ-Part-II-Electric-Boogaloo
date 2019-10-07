using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class PathfindingAI : MonoBehaviour
{
    //this is all from a Brackey's video about pathfinding 
    //https://www.youtube.com/watch?v=jvtFUfJ6CP8
    //I plan on making this work a little differently so that it will work for all of the different levels we might want

    Transform target;
    float speed;
    public float nextWaypointDistance;
    
    
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    BaseEnemy baseEnemy;

    bool move;

    // Start is called before the first frame update
    void Start()
    {
        move = true;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        baseEnemy = GetComponent<BaseEnemy>();

        speed = baseEnemy.speed;

        InvokeRepeating("UpdatePath", 0, .5f);

    }

    void UpdatePath()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            target = baseEnemy.aggroScript.currentTarget;
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //only apply force to the enemy if they are in aggro
        if (baseEnemy.aggroScript.aggro)
        {
            //this is going to be in case any enemies want a staggered type of movement
            if (move)
            {
                rb.MovePosition(rb.position + force);
            }
        }

    }

    public void StartMove()
    {
        move = true;
    }

    public void StopMove()
    {
        move = false;
    }

}
