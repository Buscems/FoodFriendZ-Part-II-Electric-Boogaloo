using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSetTime : MonoBehaviour
{
    public float timeToWait;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeToWait -= Time.deltaTime;

        if (timeToWait < 0)
            Destroy(gameObject);
    }
}
