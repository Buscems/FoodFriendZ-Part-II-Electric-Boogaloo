using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private MainPlayer mp;
    public GameObject item = null;
    public float curTimer;

    void Start()
    {
        mp = GetComponent<MainPlayer>();
    }

    void Update()
    {
        //check if player can use the item
        if (Input.GetKeyDown(KeyCode.E) && item != null)
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        Instantiate(item, transform.position, Quaternion.Euler(mp.attackDirection.transform.eulerAngles.x, mp.attackDirection.transform.eulerAngles.y, mp.attackDirection.transform.eulerAngles.z));
        //item = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            if (item == null)
            {
                item = other.gameObject;
                Destroy(other.gameObject);
            }
        }
    }
}
