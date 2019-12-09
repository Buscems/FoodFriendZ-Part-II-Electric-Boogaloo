using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public GameObject[] levels;

    private int currentLevelNum = -1;
    private GameObject currentScene = null;

    private GameObject player;
    private GameObject mainCamera;

    public AstarPath path;
    bool hasScanned;

    private GameObject[] otherArray;
    private GameObject[] bArray;
    public bool isTutorial;

    bool hasChangedLevel;

    void Start()
    {
        List<GameObject> bList = new List<GameObject>();
        List<GameObject> otherList = new List<GameObject>();

        for(int i = 0; i < levels.Length; i++)
        {
            if (levels[i].gameObject.name.Contains("atLe"))
            {
                bList.Add(levels[i]);
            }
            else
            {
                otherList.Add(levels[i]);
            }
        }

        otherArray = otherList.ToArray();
        bArray = bList.ToArray();

        if (levels.Length == 1)
        {
            bArray = new GameObject[] { levels[0] };
            otherArray = new GameObject[] { levels[0] };
        }

        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
        NextLevel();
        //int randNumber = Random.Range(0, levels.Length);
        //currentScene = Instantiate(levels[randNumber], levels[randNumber].transform.position, Quaternion.identity);
        //Vector3 spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
        //layer.transform.position = spawnPoint;
        //mainCamera.transform.position = spawnPoint;
        Manager.currentLevel = 0;
    }

    private void LateUpdate()
    {
        if (!hasScanned)
        {
            path.Scan(path.graphs);
            hasScanned = true;
        }
    }

    public void NextLevel()
    {
        int chooseArray = Random.Range(0, 100);
        int newNumber;


        if (isTutorial)
        {
            chooseArray = 99;
        }

        if (chooseArray > 70)
        {
            newNumber = Random.Range(0, bArray.Length);
        }
        else
        {
            newNumber = Random.Range(0, otherArray.Length);
        }



        if (levels.Length > 1)
        {
            while (newNumber == currentLevelNum)
            {
                if (chooseArray > 70)
                {
                    newNumber = Random.Range(0, bArray.Length);
                }
                else
                {
                    newNumber = Random.Range(0, otherArray.Length);
                }
            }
        }
        try
        {
            currentLevelNum = newNumber;
            hasScanned = false;
            Debug.Log(chooseArray);
            Destroy(currentScene);
            if (chooseArray > 70)
            {
                currentScene = Instantiate(bArray[newNumber], bArray[newNumber].transform.position, Quaternion.identity);
                Debug.Log(bArray[newNumber].name);
            }
            else
            {
                currentScene = Instantiate(otherArray[newNumber], otherArray[newNumber].transform.position, Quaternion.identity);
                Debug.Log(otherArray[newNumber].name);
            }                
            Vector3 spawnPoint = GameObject.Find("SPAWNPOINT").transform.position;
            //print(spawnPoint);
            //player.transform.position = spawnPoint;
            mainCamera.transform.position = spawnPoint;
            if (!hasChangedLevel)
            {
                Manager.currentLevel += 1;
                hasChangedLevel = true;
            }
        }
        catch { }
    }
}
