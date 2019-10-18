using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarEnemy : MonoBehaviour
{

    public BaseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
