using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaLikeEnemy : MonoBehaviour
{
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    private float characterVelocity = 5f;

    public bool isDizzy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementPerSecond = movementDirection * characterVelocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDizzy && collision.gameObject.tag == "Player1"){
            transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
        }
    }
}
