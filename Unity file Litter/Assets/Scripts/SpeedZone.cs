using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    public float speed = 4f;

    private PlayerController _playerController;
    private bool _isSpeedSet = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_playerController == null)
            {
                _playerController = collision.GetComponent<PlayerController>();
            }

            if(_playerController != null)
            {
                _playerController.SetSpeedModifier(speed);
                _isSpeedSet = true; 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isSpeedSet)
        {
            _isSpeedSet = false;
            if(_playerController != null)
                _playerController.SetSpeedModifier(0);
        }
    }
}
