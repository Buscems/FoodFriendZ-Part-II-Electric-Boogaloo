using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrieker : MonoBehaviour
{

    BaseEnemy baseEnemy;
    public CircleCollider2D alert;
    PathfindingAI path;

    // Start is called before the first frame update
    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        path = GetComponent<PathfindingAI>();
        path.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (baseEnemy.aggroScript.aggro == true){
            alert.radius = 5f;
        }
    }
}
