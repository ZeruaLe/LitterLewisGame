using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    public LitterLevel level;

    private bool _hasTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasTriggered)
        {
            if (collision.CompareTag("Player"))
            {
                _hasTriggered = true;

                level.FinishLevel();
            }
        }
    }
}
