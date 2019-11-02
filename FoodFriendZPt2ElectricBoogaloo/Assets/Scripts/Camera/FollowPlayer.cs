using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    
    [HideInInspector]
    public Transform player;

    public float radius;

    [HideInInspector]
    public float distance;

    public float cameraSpeed;

    public float bossCameraSpeed;

    private Vector2 velocity = Vector2.zero;

    [HideInInspector]
    public bool bossCamera;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void FixedUpdate()
    {
        Vector3 playerPos = player.position;
        distance = Vector2.Distance(playerPos, Camera.main.transform.position);

        if (Mathf.Abs(distance) >= radius)
        {
            
            playerPos.z = -10;
            Vector3 currentPos = transform.position;
            currentPos.z = -10;
            if (!bossCamera)
            {
                transform.position = Vector3.Slerp(currentPos, playerPos, player.transform.parent.transform.parent.GetComponent<MainPlayer>().speed * cameraSpeed * Time.deltaTime);
            }
            else
            {
                //transform.position = Vector3.MoveTowards(currentPos, playerPos, bossCameraSpeed * Time.fixedDeltaTime);
                transform.position = Vector3.Lerp(currentPos, playerPos, bossCameraSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
