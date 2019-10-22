using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public GameObject[] levels;

    private int currentLevelNum = -1;
    private GameObject currentScene;

    private GameObject player;
    private GameObject mainCamera;

    public AstarPath path;
    bool hasScanned;

    void Start()
    {
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
        int randNumber = Random.Range(0, levels.Length);
        currentScene = Instantiate(levels[randNumber], transform.position, Quaternion.identity);
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
        player.transform.position = spawnPoint;
        mainCamera.transform.position = spawnPoint;
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

        while (newNumber == currentLevelNum)
        {
            newNumber = Random.Range(0, levels.Length);
        }

        try
        {
            hasScanned = false;
            Destroy(currentScene);
            currentScene = Instantiate(levels[newNumber], Vector3.zero, Quaternion.identity);
            Vector3 spawnPoint = GameObject.Find("SPAWNPOINT").transform.position;
            //print(spawnPoint);
            player.transform.position = spawnPoint;
            mainCamera.transform.position = spawnPoint;
        }
        catch { }

    }
}
