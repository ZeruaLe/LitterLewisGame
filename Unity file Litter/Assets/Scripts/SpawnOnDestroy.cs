using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnDestroy : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    private void OnDisable()
    {
        if (!enabled)
            return;
        if(objectToSpawn != null)
            Instantiate(objectToSpawn, spawnPoint == null ? transform.position : spawnPoint.position, Quaternion.identity, transform.root);
    }
}
