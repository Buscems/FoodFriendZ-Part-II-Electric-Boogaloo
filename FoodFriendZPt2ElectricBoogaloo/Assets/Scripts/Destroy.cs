using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public float timeUntilDestroy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnOffSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TurnOffSound()
    {
        yield return new WaitForSeconds(timeUntilDestroy);
        Destroy(this.gameObject);
    }

}
