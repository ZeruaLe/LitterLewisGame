using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LitterLevel : MonoBehaviour
{
    public LevelID levelId;
    public CameraController levelCamera;
    public Checkpoint startingCheckpoint;
    public PlayerController levelPlayer;

    public void Awake()
    {
        if(LitterGameManager.instance != null)
            LitterGameManager.instance.RegisterLevel(this);
    }

    public void ToggleLevel(bool toggle)
    {
        // hack to set instance
        levelPlayer.gameObject.SetActive(false);

        if(toggle)
            CheckpointResetSystem.instance.SetCheckpoint(startingCheckpoint);
        levelCamera.gameObject.SetActive(toggle);
        levelPlayer.gameObject.SetActive(toggle);
        gameObject.SetActive(toggle);

        CheckpointResetSystem.instance.RespawnPlayer();
    }

    public void EnableForPan()
    {

    }

    private void Start()
    {
        if (LitterGameManager.instance.isDebug)
        {
            LitterGameManager.SwitchLevel(levelId);
        }
    }

    public void FinishLevel()
    {
        Debug.Log("Level Finish");
        LitterGameManager.instance.HandleGameFlow();
    }
}
