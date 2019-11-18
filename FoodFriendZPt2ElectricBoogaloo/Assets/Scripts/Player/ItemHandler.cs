using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemHandler : MonoBehaviour
{
    private GameObject statBoostPanel;
    public GameObject statBoostHolder;

    //holds every stat boost you puck up
    List<string> statBoostNames = new List<string>();
    //holds one of each stat boost you pick up
    List<GameObject> statBoostGameObjects = new List<GameObject>();

    //since we have two colliders on the player theres a chance the stat boost collides wih both colliders in the same frame which causes you to pick up the stat boost twice, this bool makes sure that doesnt happen
    bool canPickUp = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "StatBoost" && canPickUp)
        {
            canPickUp = false;
            bool flag = false;
            int counter = 1;

            foreach (string name in statBoostNames)
            {
                if (name == other.gameObject.GetComponent<PowerUps>().powerUpName)
                {
                    flag = true;
                    counter++;
                }
            }

            statBoostNames.Add(other.gameObject.GetComponent<PowerUps>().powerUpName);

            foreach (GameObject statBoost in statBoostGameObjects)
            {
                if (statBoost.name == other.gameObject.GetComponent<PowerUps>().powerUpName)
                {
                    statBoost.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + counter;
                }
            }
        }
    }

    void LateUpdate()
    {
        canPickUp = true;
    }
}
