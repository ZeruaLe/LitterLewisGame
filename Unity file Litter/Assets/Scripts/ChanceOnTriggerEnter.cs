using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChanceOnTriggerEnter : MonoBehaviour
{  
    public string tagMask = "Player";
    [Range(0f, 1f)]
    public float chance = 1f;
    public UnityEvent triggerEvent;
    public UnityEvent chanceTriggerEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagMask))
        {
            // Roll for chance
            if (Random.Range(0, 1f) < chance)
            {
                chanceTriggerEvent?.Invoke();
            } else
            {
                triggerEvent?.Invoke();
            }
        }      
    }
}
