using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject myLog;
    public GameObject dipperLog;
    private Transform logBoss;

    public float minWait;
    public float maxWait;

    public float dipperRatio;

    private void Start()
    {
        logBoss = GameObject.Find("Logs").transform;

        StartCoroutine(PeriodicSpawn());
    }

    IEnumerator PeriodicSpawn()
    {
        while (true)
        {
            if (dipperLog == null)
            {
                Instantiate(myLog, transform.position, Quaternion.identity).transform.SetParent(logBoss);
                float myWait = Random.Range(minWait, maxWait);
                yield return new WaitForSeconds(myWait);
            }
            else
            {
                if(Random.Range(0.0f,1.0f) < dipperRatio)
                    Instantiate(dipperLog, transform.position, Quaternion.identity).transform.SetParent(logBoss);
                else
                    Instantiate(myLog, transform.position, Quaternion.identity).transform.SetParent(logBoss);

                float myWait = Random.Range(minWait, maxWait);
                yield return new WaitForSeconds(myWait);
            }
        }
    }
}
