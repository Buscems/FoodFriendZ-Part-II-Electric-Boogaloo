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

    bool hasChangedLevel;

    void Start()
    {
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
        int newNumber = Random.Range(0, levels.Length);

        if (levels.Length > 1)
        {
            while (newNumber == currentLevelNum)
            {
                newNumber = Random.Range(0, levels.Length);
            }
        }
        try
        {
            currentLevelNum = newNumber;
            hasScanned = false;
            Destroy(currentScene);
            currentScene = Instantiate(levels[newNumber], levels[newNumber].transform.position, Quaternion.identity);
            Vector3 spawnPoint = GameObject.Find("SPAWNPOINT").transform.position;
            //print(spawnPoint);
            player.transform.position = spawnPoint;
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
