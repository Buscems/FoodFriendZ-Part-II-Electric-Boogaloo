using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using Rewired.ControllerExtensions;

public class AttackDirection : MonoBehaviour
{

    Vector3 direction;

    //the following is in order to use rewired
    [Tooltip("Reference for using rewired")]
    private Player myPlayer;
    [Tooltip("Number identifier for each player, must be above 0")]
    private int playerNum;

    private void Awake()
    {
        //Rewired Code
        playerNum = transform.parent.GetComponent<MainPlayer>().playerNum;
        myPlayer = ReInput.players.GetPlayer(playerNum - 1);
        ReInput.ControllerConnectedEvent += OnControllerConnected;
        CheckController(myPlayer);
    }

       void Update()
    {
        direction.x = myPlayer.GetAxisRaw("DirectionHorizontal");
        direction.y = myPlayer.GetAxisRaw("DirectionVertical");

        if (direction.y != 0)
        {
            transform.right = direction;
        }

        if (direction.x != 0)
        {
            transform.right = direction;
        }
        //this.transform.localPosition = direction / 4;

        /*
        if (Input.GetAxis("RHorizontal") < 0)
        {
            Vector3 lookDirection = new Vector3(0, 0, -Input.GetAxis("RVertical"));
            transform.rotation = Quaternion.Euler(lookDirection * 90);
        }
        else
        {
            Vector3 lookDirection = new Vector3(0, 0, Input.GetAxis("RVertical"));
            transform.rotation = Quaternion.Euler(lookDirection * 90);
        }
        */
    }

    //these two methods are for ReWired, if any of you guys have any questions about it I can answer them, but you don't need to worry about this for working on the game - Buscemi
    void OnControllerConnected(ControllerStatusChangedEventArgs arg)
    {
        CheckController(myPlayer);
    }

    void CheckController(Player player)
    {
        foreach (Joystick joyStick in player.controllers.Joysticks)
        {
            var ds4 = joyStick.GetExtension<DualShock4Extension>();
            if (ds4 == null) continue;//skip this if not DualShock4
            switch (playerNum)
            {
                case 4:
                    ds4.SetLightColor(Color.yellow);
                    break;
                case 3:
                    ds4.SetLightColor(Color.green);
                    break;
                case 2:
                    ds4.SetLightColor(Color.blue);
                    break;
                case 1:
                    ds4.SetLightColor(Color.red);
                    break;
                default:
                    ds4.SetLightColor(Color.white);
                    Debug.LogError("Player Num is 0, please change to a number > 0");
                    break;
            }
        }
    }
}
