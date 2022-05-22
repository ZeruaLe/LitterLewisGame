using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector2 xMinMax = new Vector2(-10, 10);
    [SerializeField] private Vector2 yMinMax = new Vector2(-10, 10);

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            Vector3 newPos = new Vector3(Mathf.Clamp(player.position.x, xMinMax.x, xMinMax.y), Mathf.Clamp(player.position.y + 0.5f, yMinMax.x, yMinMax.y), transform.position.z); // Moves the camera's position to the player's current X and Y position.
            transform.position = newPos;
        }
    }
}