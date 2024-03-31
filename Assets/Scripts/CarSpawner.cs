using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject myCar;
    private Transform carBoss;

    public float maxWait;
    public float minWait;

    private void Start()
    {
        carBoss = GameObject.Find("Cars").transform;

        StartCoroutine(PeriodicSpawn());
    }

    IEnumerator PeriodicSpawn()
    {
        while(true)
        {
            Instantiate(myCar, transform.position, Quaternion.identity).transform.SetParent(carBoss);
            float myWait = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(myWait);
        }
    }
}
