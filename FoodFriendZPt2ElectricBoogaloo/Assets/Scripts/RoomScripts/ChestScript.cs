using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChestScript : MonoBehaviour
{

    [HideInInspector]
    public MainPlayer player;

    public GameObject[] items;

    public TextMeshProUGUI chestPrice;

    [Header("Different Rarities")]
    public float wellDoneChance;
    public float mediumWellChance;
    public float mediumRareChance;
    public float rareChance;

    [Header("Different Costs for Rarity")]
    public int baseCost;
    public int wellDoneCost;
    public int mediumWellCost;
    public int mediumRareCost;
    public int rareCost;

    Queue<GameObject> wellDone = new Queue<GameObject>();
    Queue<GameObject> mediumWell = new Queue<GameObject>();
    Queue<GameObject> mediumRare = new Queue<GameObject>();
    Queue<GameObject> rare = new Queue<GameObject>();

    public GameObject currentPowerup;

    public Animator anim;

    //for the sparkles
    public GameObject sparkles;
    public GameObject currentSparkle;

    //for testing
    public bool wellDun;
    public bool medWell;
    public bool medRare;
    public bool rur;

    public int maxHits;
    [HideInInspector]
    public int hits;
    public bool hasOpened;
    bool resetHits;

    [Header("Shake Variables")]
    Vector3 basePos;
    bool shake;
    public float shakeTime;
    public float shakeBuffer;

    // Start is called before the first frame update
    void Start()
    {
        chestPrice.enabled = false;
        basePos = transform.position;

        player = GameObject.Find("Player").GetComponent<MainPlayer>();

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
            if(currentSparkle != null)
            {
                Destroy(currentSparkle);
            }
            wellDun = true;
        }
        else if (rarityChance <= mediumWellChance)
        {
            currentPowerup = mw[Random.Range(0, mw.Length)];
            baseCost = mediumWellCost;
            if (currentSparkle != null)
            {
                Destroy(currentSparkle);
            }
            anim.SetInteger("rarity", 1);
            medWell = true;
        }
        else if (rarityChance <= mediumRareChance)
        {
            currentPowerup = mr[Random.Range(0, mr.Length)];
            baseCost = mediumRareCost;
            if (currentSparkle != null)
            {
                Destroy(currentSparkle);
            }
            anim.SetInteger("rarity", 2);
            medRare = true;
        }
        else if (rarityChance <= rareChance)
        {
            currentPowerup = r[Random.Range(0, r.Length)];
            baseCost = rareCost;
            anim.SetInteger("rarity", 3);
            currentSparkle = Instantiate(sparkles, transform.position, Quaternion.identity);
            rur = true;
        }
        /*
        else
        {
            currentPowerup = wd[Random.Range(0, wd.Length)];
            baseCost = wellDoneCost;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(hits >= maxHits && !hasOpened && player.currency > baseCost)
        {
            player.currency -= baseCost;
            OpenChest();
            hasOpened = true;
        }

        if(hits > 0 && !resetHits)
        {
            StartCoroutine(ResetHits());
        }

        if (shake)
        {
            this.transform.position = new Vector3(Random.Range(basePos.x - shakeBuffer, basePos.x + shakeBuffer), Random.Range(basePos.y - shakeBuffer, basePos.y + shakeBuffer));
        }
        else
        {
            this.transform.position = basePos;
        }

    }

    IEnumerator ResetHits()
    {
        resetHits = true;
        yield return new WaitForSeconds(1.5f);
        hits = 0;
        resetHits = false;
    }

    public void StartShake()
    {
        if (!shake)
        {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        shake = true;
        yield return new WaitForSeconds(shakeTime);
        shake = false;
    }

    public void OpenChest()
    {
        if(anim != null)
        {
            anim.SetBool("Open", true);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void SpawnItem()
    {
        StartCoroutine(ChestItem());
        GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator ChestItem()
    {
        GameObject pu = Instantiate(currentPowerup, transform.position, Quaternion.identity);
        pu.GetComponent<BoxCollider2D>().enabled = false;
        if (currentSparkle != null)
        {
            Destroy(currentSparkle);
        }
        yield return new WaitForSeconds(.5f);
        pu.GetComponent<BoxCollider2D>().enabled = true;
    }

}
