using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikey : MonoBehaviour
{

    BaseEnemy baseEnemy;
    [SerializeField] GameObject spike;
    [SerializeField]  GameObject frontLeft;
    [SerializeField]  GameObject midLeft;
    [SerializeField]  GameObject backLeft;
    [SerializeField]  GameObject frontRight;
    [SerializeField]  GameObject midRight;
    [SerializeField]  GameObject backRight;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }

    
    void Update()
    {
        if (baseEnemy.aggroScript.aggro)
        {
            Debug.Log("spike");
            Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);
        }

    }
}
