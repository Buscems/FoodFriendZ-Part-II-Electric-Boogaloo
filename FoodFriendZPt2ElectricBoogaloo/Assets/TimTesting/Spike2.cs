using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike2 : MonoBehaviour
{

    BaseEnemy baseEnemy;
    PathfindingAI path;
    [SerializeField] GameObject spike;
    [SerializeField] GameObject frontLeft;
    [SerializeField] GameObject midLeft;
    [SerializeField] GameObject backLeft;
    [SerializeField] GameObject frontRight;
    [SerializeField] GameObject midRight;
    [SerializeField] GameObject backRight;
    [SerializeField] float DownTime;
    [SerializeField] float ShootTime;
    [SerializeField] private bool reload;
    private float savedSpeed;


    void Start()
    {
        baseEnemy = GetComponent<BaseEnemy>();
        reload = false;
        savedSpeed = baseEnemy.speed;
        path = GetComponent<PathfindingAI>();
    }


    void Update()
    {
        if (baseEnemy.aggroScript.aggro && reload == false)
        {
            path.enabled = false;
            StartCoroutine(shooting());

        }

    }

    void Shoot()
    {

        Instantiate(spike, frontLeft.transform.position, frontLeft.transform.rotation);

    }

    IEnumerator shooting()
    {
        reload = true;
        Shoot();
        Debug.Log("yeeeee");
        yield return new WaitForSeconds(ShootTime);
        path.enabled = true;
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        Debug.Log("yerrrr");
        yield return new WaitForSeconds(DownTime);
        reload = false;
    }

}
