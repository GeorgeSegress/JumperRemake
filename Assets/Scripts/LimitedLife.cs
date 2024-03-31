using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLife : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}
