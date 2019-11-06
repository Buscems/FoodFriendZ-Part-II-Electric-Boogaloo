using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{

    public float fallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.position -= new Vector3(0, fallSpeed, 0);

        if(transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }

    }
}
