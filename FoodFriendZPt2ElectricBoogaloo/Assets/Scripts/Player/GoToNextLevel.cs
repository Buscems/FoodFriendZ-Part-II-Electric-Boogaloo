using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNextLevel : MonoBehaviour
{
    public GameObject[] levels;

    private int currentLevelNum = -1;
    private GameObject currentScene;

    private GameObject player;
    private GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = GameObject.Find("Main Camera");
        int randNumber = Random.Range(0, levels.Length);
        currentScene = Instantiate(levels[randNumber], transform.position, Quaternion.identity);
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("Spawnpoint").transform.position;
        player.transform.position = spawnPoint;
        camera.transform.position = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextLevel()
    {
        int newNumber = Random.Range(0, levels.Length);

        while(newNumber == currentLevelNum)
        {
            newNumber = Random.Range(0, levels.Length);
        }

        try
        {
            Destroy(currentScene);
            currentScene = Instantiate(levels[newNumber], Vector3.zero, Quaternion.identity);
            Vector3 spawnPoint = GameObject.Find("SPAWNPOINT").transform.position;
            print(spawnPoint);
            player.transform.position = spawnPoint;
            camera.transform.position = spawnPoint;
        }
        catch { }

    }
}
