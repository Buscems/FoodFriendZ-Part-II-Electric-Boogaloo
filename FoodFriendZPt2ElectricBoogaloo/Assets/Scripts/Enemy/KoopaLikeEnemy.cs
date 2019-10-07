using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaLikeEnemy : MonoBehaviour
{
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float characterVelocity = 5f;

    public bool notDizzy;
    public bool isDizzy;
    public bool isSpinning;

    public BaseEnemy enemy;
    public Tracking track;

    // Start is called before the first frame update
    void Start()
    {
        notDizzy = true;
        enemy = GetComponent<BaseEnemy>();
        track = GetComponent<Tracking>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notDizzy == true && enemy.aggroScript.aggro){
            track.follow = true;
        }
        

        if (isSpinning == true)
        {
            movementPerSecond = movementDirection * characterVelocity;
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (notDizzy && collision.gameObject.tag == "Player1"){
            notDizzy = false;
            enemy.aggroScript.aggro = false;
            track.follow = false;
            track.track = false;
            isDizzy = true;
        }
        /*if (isDizzy == true && collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            isDizzy = false;
            isSpinning = true;
        }*/
    }
}
