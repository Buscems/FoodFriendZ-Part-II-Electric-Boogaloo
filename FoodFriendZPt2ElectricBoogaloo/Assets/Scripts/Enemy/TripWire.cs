using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripWire : MonoBehaviour
{

    public bool exposed;

    public BaseEnemy baseEnemy;

    // Start is called before the first frame update
    void Start()
    {
        exposed = false;
        baseEnemy = GetComponent<BaseEnemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (baseEnemy.aggroScript.aggro == true){
            exposed = true;
        } else {
            exposed = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1"){
            Destroy(gameObject);
        }
    }
}
