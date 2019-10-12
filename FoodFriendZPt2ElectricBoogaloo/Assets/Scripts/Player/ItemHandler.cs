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

    // Start is called before the first frame update
    void Start()
    {
        statBoostPanel = GameObject.Find("*EquipmentToolBarPanel");        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "StatBoost")
        {
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

            if (flag == false)
            {
                GameObject statBoost = Instantiate(statBoostHolder, transform.position, Quaternion.identity);
                statBoost.transform.parent = statBoostPanel.transform;
                statBoost.transform.GetChild(0).GetComponent<Image>().sprite = other.gameObject.GetComponent<SpriteRenderer>().sprite;
                statBoost.transform.localScale = new Vector3(1, 1, 1);
                statBoost.name = other.gameObject.GetComponent<PowerUps>().powerUpName;
                statBoostGameObjects.Add(statBoost);
            }

            foreach (GameObject statBoost in statBoostGameObjects)
            {
                if (statBoost.name == other.gameObject.GetComponent<PowerUps>().powerUpName)
                {
                    print("fghfghffffffffffffffff");
                    statBoost.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "x" + counter;
                }
            }
        }
    }
}
