using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakoyakiLogic : MonoBehaviour
{
    public GameObject attack;
    private bool didAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (didAttack == false)
            { 
                didAttack = true;
                GameObject _attack = Instantiate(attack, transform.position, Quaternion.identity);
                _attack.transform.parent = transform;
                try
                {
                    if (_attack.transform.childCount > 0)
                    {
                        foreach (Transform child in attack.transform)
                        {
                            try
                            {
                                child.GetComponent<Attack>().damage = GetComponent<Attack>().damage;
                            }
                            catch { }
                        }
                    }
                    else
                    {
                        attack.GetComponent<Attack>().damage = GetComponent<Attack>().damage;
                    }
                }
                catch { }
            }
        }
    }
}
