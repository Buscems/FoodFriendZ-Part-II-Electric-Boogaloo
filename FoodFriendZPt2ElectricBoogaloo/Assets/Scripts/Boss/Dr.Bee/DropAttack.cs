using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAttack : MonoBehaviour
{
    //cursor that will display where attack is
    public GameObject attack;

    BaseBoss baseBoss;

    //interval for attack time
    public float attackInterval;

    bool startAttack;

    // Start is called before the first frame update
    void Start()
    {
        baseBoss = GetComponent<BaseBoss>();
    }

    // Update is called once per frame
    void Update()
    {
        if(baseBoss.aggroScript.aggro && !startAttack)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        startAttack = true;
        var currentAttack = Instantiate(attack, baseBoss.aggroScript.currentTarget.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(attackInterval);
        startAttack = false;
    }

}
