using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMove : MonoBehaviour
{

    Rigidbody2D rb;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        rb.MovePosition(transform.position + velocity * speed * Time.deltaTime);
    }
}
