using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    public float despawnTime = 10.0f;

    void Start()
    {
        StartCoroutine("DespawnObject");
    }

    IEnumerator DespawnObject()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }
}
