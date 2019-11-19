using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolvingBullet : MonoBehaviour
{
    public GameObject proj;
    public Transform revolveAround;
    public float speed;
    public Vector3 velocity;
    BaseEnemy baseEnemy;

    Rigidbody2D rb;

    private Vector3 zAxis = new Vector3(0, 0, 1);

    public bool revolve;
    public bool shoot;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        rb = GetComponent<Rigidbody2D>();
        revolve = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (revolve == true)
        {
            transform.RotateAround(revolveAround.position, zAxis, speed);
        }

        /*if (baseEnemy.aggroScript.aggro == true){
            revolve = false;
            rb.MovePosition(transform.position + velocity * speed * Time.deltaTime);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            Destroy(proj);
        }
    }
}
