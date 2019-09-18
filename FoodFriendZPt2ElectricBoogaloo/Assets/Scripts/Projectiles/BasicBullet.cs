using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    [HideInInspector]
    public float bulletSpeed;
    [HideInInspector]
    public float timeTillDespawn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        timeTillDespawn -= Time.deltaTime;

        if (timeTillDespawn < 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * bulletSpeed * Time.deltaTime;
    }
}
