using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class revolvingBullet : MonoBehaviour
{

    public Transform revolveAround;
    public float speed;

    private Vector3 zAxis = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(revolveAround.position, zAxis, speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            Destroy(gameObject);
        }
    }
}
