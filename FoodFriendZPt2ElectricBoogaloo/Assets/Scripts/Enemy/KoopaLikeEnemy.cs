using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaLikeEnemy : MonoBehaviour
{
    //reflect script help from https://www.youtube.com/watch?v=Vr-ojd4Y2a4&t=579s

    private Vector3 playerPos;
    Vector3 dir;

    public bool awake;
    public bool dizzy;
    public bool spinning;

    public float timeForDizzy;
    public float timeForSpinning;

    BaseEnemy baseEnemy;
    public PathfindingAI path;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        awake = true;
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (awake == true) {
            path.enabled = true;
            baseEnemy.walkIntoDamage = 1;
            if (baseEnemy.aggroScript.aggro == true) {
                playerPos = baseEnemy.aggroScript.currentTarget.transform.position;
            }
        }

        if (dizzy == true) {
            path.enabled = false;
            baseEnemy.walkIntoDamage = 0;
        }

        if (spinning == true) {
            dizzy = false;
        }

        if (baseEnemy.health <= 2) {
            StartCoroutine(dizzyTime());
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player1") && dizzy == true) {
            StopAllCoroutines();
            baseEnemy.health += 4;
            spinning = true;
            Vector2 wallNormal = other.contacts[0].normal;
            dir = Vector2.Reflect(-rb.velocity, wallNormal).normalized;

            rb.velocity = dir * baseEnemy.speed * 4;
        }

        if (other.gameObject.CompareTag("TilesHere") && spinning == true){
            StartCoroutine(spinningTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && spinning == true){
            Destroy(collision.gameObject);
        }
    }

    IEnumerator dizzyTime() {
        awake = false;
        dizzy = true;
        yield return new WaitForSeconds(timeForDizzy);
        dizzy = false;
        awake = true;
    }

    IEnumerator spinningTime(){
        yield return new WaitForSeconds(timeForSpinning);
        spinning = false;
        awake = true;
    }
}
