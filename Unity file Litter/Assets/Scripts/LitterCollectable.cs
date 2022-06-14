using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LitterCollectable : MonoBehaviour
{
    public static UnityAction<int> OnCollectableObtain;

    public int id;

    public void Collect()
    {
        OnCollectableObtain?.Invoke(id);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Collect();
            
        }
    }
}
