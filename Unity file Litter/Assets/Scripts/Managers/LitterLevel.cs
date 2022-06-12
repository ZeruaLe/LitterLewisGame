using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterLevel : MonoBehaviour
{
    public LevelID levelId;
    public CameraController levelCamera;
    public Checkpoint startingCheckpoint;

    public void Awake()
    {
        LitterGameManager.RegisterLevel(this);
    }

    public void ToggleLevel(bool toggle)
    {
        if(toggle)
            CheckpointResetSystem.instance.SetCheckpoint(startingCheckpoint);
        levelCamera.gameObject.SetActive(toggle);
        gameObject.SetActive(toggle);
    }

    public void EnableForPan()
    {

    }
}
