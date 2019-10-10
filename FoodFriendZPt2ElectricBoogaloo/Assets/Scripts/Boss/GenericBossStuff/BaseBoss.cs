using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoss : MonoBehaviour
{

    [Tooltip("This is how many doors need to be opened after the boss dies")]
    public GameObject[] doors;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

}
