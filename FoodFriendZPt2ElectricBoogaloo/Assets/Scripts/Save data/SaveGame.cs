using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveGame : MonoBehaviour
{

    [HideInInspector]
    public GameData gameData;

    #region Instance
    private bool load = false;
    private static SaveGame instance;

    public static SaveGame Instance
    {
        get
        {
            print("top");
            if (instance == null)
            {
                print("null");
                instance = FindObjectOfType<SaveGame>();
                if (instance == null)
                {
                    instance = new GameObject("Spawned SaveSystem", typeof(SaveGame)).GetComponent<SaveGame>();
                    print("madse new");
                }
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }
    #endregion

    private string savefileName = "SaveFile.ss";
    private bool loadOnStart = false;
    private BinaryFormatter formatter;

    // Start is called before the first frame update
    void Start()
    {
        formatter = new BinaryFormatter();

        if (loadOnStart)
        {
            Load();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaveToFile();
        }
    }

    public void Load()
    {
        load = true;

        try
        {
            //get current save file from directory. (this file its getting is a binary file)
            var file = new FileStream("SaveData/" + savefileName, FileMode.Open, FileAccess.Read);

            //convert binary file data into a new instance of the GameData class
            gameData = (GameData)formatter.Deserialize(file);

            file.Close();
        }
        //if there is no save file yet, then make one
        catch
        {
            Debug.Log("No save file found");
            SaveToFile();
        }
    }

    public void SaveToFile()
    {
        try
        {
            //make root folder to store all data if it doesnt exist already
            if (!Directory.Exists("SaveData"))
            {
                print("directory doesnt exist");
                Directory.CreateDirectory("SaveData");
            }
        }
        catch (IOException ex)
        {
            Debug.Log(ex.Message);
        }

        //convert data to binary format
        SaveToGameData();
        //save binary file
        SaveToBinaryFormat();

    }

    public void SaveToGameData()
    {
        //if there is no current instance of GameData, make one
        if (gameData == null)
        {
            gameData = new GameData();
        }


        //make temp array, (the real array is in the GameData script) 
        //please refer to GameData script for correct format of each array
        int[] tempGameStats = new int[5];



        //below is example from another project, set all the variable to the new temporay array because we
        //will then put those files into the save file. 
        /*
        temp[0] = conductor.visualOffset.ToString();
        temp[1] = artistName;
        temp[2] = conductor.bpm.ToString();
        temp[3] = songName;
        temp[5] = (lengthOfMeasure / 4).ToString();
        */

        //finally send data to the instance of the GameData script we just made
        //note that this doesnt save to file on computer yet
        gameData.GameStats = tempGameStats;
    }

    public void SaveToBinaryFormat()
    {
        print("SaveData/" + savefileName);

        var file = new FileStream("SaveData/" + savefileName, FileMode.OpenOrCreate, FileAccess.Write);
        formatter.Serialize(file, gameData);
        file.Close();
    }


}
