using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CheckpointResetSystem : MonoBehaviour
{

    #region Singleton

    public static CheckpointResetSystem instance { get; private set; }

    #endregion

    #region Variables

    public PlayerController playerMain => PlayerController.instance;
    public Camera mainCam => CameraController.instance.myCamera;

    [Header("Settings")]
    //for checkpoint reset system
    public Checkpoint startingCheckpoint;
    public float respawnTime = 3f;

    private Checkpoint _curCheckpoint;
    public static Checkpoint curCheckpoint => instance._curCheckpoint;

    #endregion

    #region Events

    public static UnityAction<Checkpoint> OnNewCheckpoint; 

    #endregion

    #region Awake / Destroy / Start

    private void Awake()
    {
        // Init our instance
        if (instance == null)
            instance = this;

        if (instance == this)
        {
            // Awake
            PlayerController.OnDeath += OnPlayerDeath;
        }
        else
        {
            // Destroy ourselves if we are not the correct manager
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;

        PlayerController.OnDeath -= OnPlayerDeath;
    }

    private void Start()
    {
        //if (startingCheckpoint != null)
        //{
        //    SetCheckpoint(startingCheckpoint);
        //    RespawnPlayer();
        //}
    }

    #endregion

    #region Event Callbacks

    private void OnPlayerDeath()
    {
        delayEffect(RespawnPlayer);
    }

    #endregion

    #region Checkpoints

    public void SetCheckpoint(Checkpoint newCheckpoint)
    {
        // Check not current checkpoint already
        if (newCheckpoint == curCheckpoint)
            return;

        _curCheckpoint = newCheckpoint;
        OnNewCheckpoint?.Invoke(newCheckpoint);
    }

    #endregion

    #region Respawning

    [ContextMenu("Force Respawn")]
    public void RespawnPlayer()
    {
        if(playerMain != null && curCheckpoint != null)
            playerMain.Respawn(curCheckpoint.spawnPointTf != null ? curCheckpoint.spawnPointTf.position : curCheckpoint.transform.position);
    }

    #endregion

    public void resetPlayerAtStart()
    {

        //placing player at start Checkpoint "START"

        //playerMain.transform.position = startCheck.transform.position; //place player again at start position (By getting the startCheck {checkpoint} position and placing the player at that position)
        //delayEffect();

        //placing player at start Checkpoint "END"

    }

    private void delayEffect(UnityAction onComplete) {

        //mainCam.cullingMask = 2; //disabling culling mask (means it will not render the other objects in the scene except for Canvas UI {delay effect} )

        //switch (gameManagerLivesSystemScript.lives)
        //{

        //    case 2: //If players live count is 2
        //        lives2remain.SetActive(true); //if lives count is 2 then show the screen about 2 lives left
        //        break;

        //    case 1: //If players live count is 1
        //        life1remain.SetActive(true); //if life count is 1 then show the screen about 1 life left
        //        break;

        //}

        StartCoroutine(delayEffectWait(onComplete)); //calling delayEffectWait method

    }

    IEnumerator delayEffectWait(UnityAction onComplete)  //its a standalone method
    {

        yield return new WaitForSeconds(respawnTime); //Wait for 3 seconds to execute next line of code

        //lives2remain.SetActive(false); //after 3 seconds disable the text screen
        //life1remain.SetActive(false); //after 3 seconds disable the text screen

        //mainCam.cullingMask = -1; //enabling culling mask (means it will now render all the objects in the scene again)

        onComplete?.Invoke();
    }
	
}