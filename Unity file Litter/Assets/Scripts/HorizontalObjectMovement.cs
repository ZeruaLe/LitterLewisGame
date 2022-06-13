using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObjectMovement : MonoBehaviour
{
    [SerializeField]
    private float objectSpeed;
    [SerializeField]
    private Transform rightPoint;
    [SerializeField]
    private Transform leftPoint;
    private bool isRight;

    // When a game object's collider 2d touches with this game object's collider 2d.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.activeInHierarchy && collision.gameObject.activeInHierarchy)
            collision.transform.parent = gameObject.transform; // Make the player a child of the game object.
    }

    // When a game object's collider 2d that touches with this game object's collider 2d stops touching it. 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.activeInHierarchy && collision.gameObject.activeInHierarchy)
            collision.transform.parent = transform.root; // Make the player not a child of this game object.
    }

    void Update()
    {
        // if game object position is further than the rightPoint
        if (transform.position.x >= rightPoint.position.x)
        {
            isRight = true;
        }

        // if game object position is further than the leftPoint
        if (transform.position.x <= leftPoint.position.x)
        {
            isRight = false;
        }

        // if isRight is true , then move the gameobject's position , to the leftPoint with a speed of "crusherspeed*Time.deltaTime"
        if (isRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, leftPoint.position, objectSpeed * Time.deltaTime);
        }
        // if isRight is false , then move the gameobject's position , to the rightPoint with a speed of "crusherspeed*Time.deltaTime"
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, rightPoint.position, objectSpeed * Time.deltaTime);
        }


    }
}
