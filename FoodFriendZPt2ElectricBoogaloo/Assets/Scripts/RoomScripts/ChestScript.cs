using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestScript : MonoBehaviour
{

    public GameObject[] items;

    [Header("Different Rarities")]
    public float wellDoneChance;
    public float mediumWellChance;
    public float mediumRareChance;
    public float rareChance;

    [Header("Different Costs for Rarity")]
    float baseCost;
    public float wellDoneCost;
    public float mediumWellCost;
    public float mediumRareCost;
    public float rareCost;

    Queue<GameObject> wellDone = new Queue<GameObject>();
    Queue<GameObject> mediumWell = new Queue<GameObject>();
    Queue<GameObject> mediumRare = new Queue<GameObject>();
    Queue<GameObject> rare = new Queue<GameObject>();

    GameObject currentPowerup;

    public Animator anim;

    //for the sparkles
    public GameObject sparkles;
    GameObject currentSparkle;

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
            baseCost = wellDoneCost;
        }
        else if (rarityChance <= mediumWellChance)
        {
            currentPowerup = mw[Random.Range(0, mw.Length)];
            baseCost = mediumWellCost;
            anim.SetInteger("rarity", 1);
        }
        else if (rarityChance <= mediumRareChance)
        {
            currentPowerup = mr[Random.Range(0, mr.Length)];
            baseCost = mediumRareCost;
            anim.SetInteger("rarity", 2);
        }
        else if (rarityChance <= rareChance)
        {
            currentPowerup = r[Random.Range(0, r.Length)];
            baseCost = rareCost;
            anim.SetInteger("rarity", 3);
            currentSparkle = Instantiate(sparkles, transform.position, Quaternion.identity);
        }
        else
        {
            currentPowerup = wd[Random.Range(0, wd.Length)];
            baseCost = wellDoneCost;
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
    }

    public void SpawnItem()
    {
        GameObject pu = Instantiate(currentPowerup, transform.position, Quaternion.identity);
        pu.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(currentSparkle);
    }

}
