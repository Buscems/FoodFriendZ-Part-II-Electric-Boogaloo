using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shreiker : MonoBehaviour
{
    Aggro agrro;
    BaseEnemy baseEnemy;
    private GameObject[] nearbyEnemy;
    [SerializeField] private float shriekerRange;

    void Start()
    {
        agrro = GetComponent<Aggro>();
        baseEnemy = GetComponent<BaseEnemy>();
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {

            Alert(transform.position, shriekerRange);
            
        }
    }

    void Alert(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {

            hitColliders[i].GetComponent<BaseEnemy>().aggroScript.aggroRange = baseEnemy.aggroScript.aggroRange * 2;
            i++;
        }
    }
}
