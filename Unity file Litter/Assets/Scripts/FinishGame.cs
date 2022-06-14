using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    private bool _isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isTriggered)
        {
            if (collision.CompareTag("Player"))
            {
                _isTriggered = true;

                LitterGameManager.instance.FinishGame();
            }
        }
    }
}
