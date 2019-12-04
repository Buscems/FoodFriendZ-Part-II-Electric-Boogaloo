using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public Rigidbody2D rb;
    public float thrust;
    public float KnockTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Slash") {
            rb.isKinematic = false;
            Vector2 difference = transform.position - collision.transform.position;
            difference = difference.normalized * thrust;
            rb.AddForce(difference, ForceMode2D.Impulse);
            StartCoroutine(KnockCo(rb));
        }
    }

    IEnumerator KnockCo(Rigidbody2D rb){
        yield return new WaitForSeconds(KnockTime);
        Debug.Log("yes");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }
}

