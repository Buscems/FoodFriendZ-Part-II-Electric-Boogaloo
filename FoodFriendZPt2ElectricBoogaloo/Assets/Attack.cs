using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    private bool isProjectile = false;
    private bool isMelee = false;

    [HideInInspector]
    private float pierceMultiplier = 1;

    // Start is called before the first frame update
    void Start()
    {
        if(gameObject.tag == "Projectile")
        {
            isProjectile = true;
            pierceMultiplier = GetComponent<BasicBullet>().pierceMultiplier;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            //decrease the enemy's health, this will be for regular enemies as well as boss enemies
            other.GetComponent<BaseEnemy>().health -= damage;

                try
                {
                    //if projectile destroy gameobject after doing damage to enemy
                    if (transform.root.GetComponent<MainPlayer>().HitEnemy(gameObject.tag))
                    {
                        if(isProjectile)
                        {
                            if (GetComponent<BasicBullet>().canPierce)
                            {
                                damage *= pierceMultiplier;
                            }
                            else
                            {
                                Destroy(gameObject);
                            }
                        }
                    }
                }
                catch
                {
                    Destroy(gameObject);
                }

        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
