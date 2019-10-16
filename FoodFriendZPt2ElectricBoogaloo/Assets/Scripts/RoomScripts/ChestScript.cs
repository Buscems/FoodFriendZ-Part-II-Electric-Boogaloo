using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    public GameObject[] items;

    public float wellDoneChance;
    public float mediumWellChance;
    public float mediumRareChance;
    public float rareChance;

    Queue<GameObject> wellDone = new Queue<GameObject>();
    Queue<GameObject> mediumWell = new Queue<GameObject>();
    Queue<GameObject> mediumRare = new Queue<GameObject>();
    Queue<GameObject> rare = new Queue<GameObject>();

    GameObject currentPowerup;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].GetComponent<PowerUps>().rarity == PowerUps.Rarity.wellDone)
            {
                wellDone.Enqueue(items[i]);
            }
            if (items[i].GetComponent<PowerUps>().rarity == PowerUps.Rarity.mediumWell)
            {
                mediumWell.Enqueue(items[i]);
            }
            if (items[i].GetComponent<PowerUps>().rarity == PowerUps.Rarity.mediumRare)
            {
                mediumRare.Enqueue(items[i]);
            }
            if (items[i].GetComponent<PowerUps>().rarity == PowerUps.Rarity.rare)
            {
                rare.Enqueue(items[i]);
            }
        }

        var wd = wellDone.ToArray();
        var mw = mediumWell.ToArray();
        var mr = mediumRare.ToArray();
        var r = rare.ToArray();

        var rarityChance = Random.Range(0, 100);
        if(rarityChance <= wellDoneChance)
        {
            currentPowerup = wd[Random.Range(0, wd.Length)];
        }
        else if (rarityChance <= mediumWellChance)
        {
            currentPowerup = mw[Random.Range(0, mw.Length)];
        }
        else if (rarityChance <= mediumRareChance)
        {
            currentPowerup = mr[Random.Range(0, mr.Length)];
        }
        else if (rarityChance <= rareChance)
        {
            currentPowerup = r[Random.Range(0, r.Length)];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenChest()
    {
        if(anim != null)
        {
            anim.SetBool("Open", true);
        }
        Instantiate(currentPowerup, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
