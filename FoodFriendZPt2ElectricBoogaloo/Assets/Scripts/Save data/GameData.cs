using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //                           0                       1                        2                    3               4            5                 6                     7
    //game stats ->    [ Total money earned ] [ Total bullets Fired  ] [ Total enemies killed ] [ xxxxxxxxxxx ] [xxxxxxxxxx] [xxxxxxxxxx] [xxxxxxxxxxxxxxxxxxx] [xxxxxxxxxxxxxxxxxxxx] 

    public string[] gameStats { set; get; }

    void Start()
    {

    }

    void Update()
    {

    }

    public void SetVariables(string[] _gameStats)
    {
        gameStats = _gameStats;
    }
}
