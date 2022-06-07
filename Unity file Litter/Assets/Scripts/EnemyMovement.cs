using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private bool moveRight = true;
    private float distance = 2f;
    public Transform groundDetection;
    public LayerMask wallMask;
    public LayerMask patrolMask;

    // Runs every frame
    void Update()
    {    
        transform.Translate(Vector2.right * speed * Time.deltaTime);    // Enemy moves forward

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);  // Sets variable to see if there is a floor to walk across below the groundDetection
        RaycastHit2D wallInfo = Physics2D.Raycast(groundDetection.position, Vector2.zero, wallMask);    // Sets variable to see if there is a wall inside groundDetection
        RaycastHit2D patrolInfo = Physics2D.Raycast(groundDetection.position, Vector2.zero, patrolMask);    // Sets variable to see if theres a waypoint inside groundDetection

        // Runs if there is a Waypoint OR a Wall OR there is no Ground to walk across
        if (patrolInfo == true || wallInfo == true || groundInfo == false)
        {
            
            if (moveRight == true)  // if the enemy is moving right
            {
                transform.eulerAngles = new Vector3(0, -180, 0);    // turn the enemy game object around 180 (left)
                moveRight = false;  // Indicate the enemy is not moving right
            }
            else // if the enemy is move left
            {
                transform.eulerAngles = new Vector3(0, 0, 0);   // turn the enemy game object around 180 (right)
                moveRight = true;   // Indicate the enemy is move right
            }
        }
    }
}
