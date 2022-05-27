using System.Collections.Generic;
using UnityEngine;

public class CallbackSpawner : MonoBehaviour
{
    public float objectSpawnForceRange = 100f;
    public float objectSpawnAngleRange = 30f;
    public List<GameObject> objects = new List<GameObject>();



    public void Spawn()
    {
        if (objects == null || objects.Count == 0)
            return;

        int roll = Random.Range(0, objects.Count);
        GameObject newGO = Instantiate(objects[roll], transform.position, Quaternion.identity);

        if(newGO.TryGetComponent(out Rigidbody2D rigi))
        {
            // Get a random angle.
			float finalAngle = Random.Range(0, objectSpawnAngleRange) - objectSpawnAngleRange / 2;
			Vector2 finalRot = Quaternion.Euler(0,0, finalAngle) * transform.up;

            float force = Random.Range(0, objectSpawnForceRange);

            rigi.AddForce(force * finalRot);
        }
    }
}
