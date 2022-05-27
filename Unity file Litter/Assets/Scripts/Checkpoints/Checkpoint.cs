using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    [Header("Settings")]
    public bool isRepeatable = false;
    public Transform spawnPointTf;
    public GameObject colliderGO;   
    [Space]
    public GameObject activeVisualsGO;
    public GameObject inActiveVisualsGO;

    bool isActive = false;

    private void OnEnable()
    {
        CheckpointResetSystem.OnNewCheckpoint += OnNewCheckpointCallback;        
    }

    private void OnDisable()
    {
        CheckpointResetSystem.OnNewCheckpoint -= OnNewCheckpointCallback;
    }

    private void Start()
    {
        ToggleActive(CheckpointResetSystem.curCheckpoint == this);
    }

    private void OnNewCheckpointCallback(Checkpoint curCheckpoint)
    {
        bool isCurCheckpoint = curCheckpoint == this;

        // If we are not the new checkpoint toggle inactive and hide if not repeatable.
        gameObject.SetActive(!(isActive && !isCurCheckpoint && !isRepeatable));
        ToggleActive(isCurCheckpoint);
    }

    public void SetAsCheckpoint()
    {
        CheckpointResetSystem.instance.SetCheckpoint(this);
    }

    public void ToggleActive(bool toggle)
    {
        if(activeVisualsGO != null)
            activeVisualsGO.SetActive(toggle);

        if(inActiveVisualsGO != null)
            inActiveVisualsGO.SetActive(!toggle);

        colliderGO.SetActive(!toggle);

        isActive = toggle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            if (!isActive)
            {
                SetAsCheckpoint();
            }
        }
    }
}
