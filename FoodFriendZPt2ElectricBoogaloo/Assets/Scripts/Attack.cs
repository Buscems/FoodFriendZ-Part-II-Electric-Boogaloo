using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    [HideInInspector]
    public bool canPierce = false;
    [HideInInspector]
    public float pierceMultiplier = 1;
    [HideInInspector]
    public int maxAmountOfEnemiesCanPassThrough = -1;

    private int currentEnemiesPassed;

    bool isBomb;

    // Start is called before the first frame update
    void Start()
    {
        currentEnemiesPassed = maxAmountOfEnemiesCanPassThrough;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            //decrease the enemy's health, this will be for regular enemies as well as boss enemies
            other.GetComponent<BaseEnemy>().health -= damage;

            if (!isBomb)
            {
                try
                {
                    //if attack is non pierce-able, destroy on collision with enemy
                    if (transform.root.GetComponent<MainPlayer>().HitEnemy(gameObject.tag))
                    {
                        if (!canPierce)
                        {
                            Destroy(gameObject);
                        }
                    }
                }
                catch
                {
                    Destroy(gameObject);
                }

                damage *= pierceMultiplier;

                if (currentEnemiesPassed != -1)
                {
                    if (currentEnemiesPassed == 0)
                    {
                        Destroy(gameObject);
                    }

                    currentEnemiesPassed -= 1;
                }
            }
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
