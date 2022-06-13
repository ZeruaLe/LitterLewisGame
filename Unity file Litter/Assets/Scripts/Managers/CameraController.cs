using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    #region Singleton

    public static CameraController instance { get; private set; }

    #endregion

    #region Variables

    public Camera myCamera;
    public CameraMovement playerCamera;
    public Animation cameraPan;


    #endregion

    private void OnEnable()
    {
        instance = this;
    }

    public void DoCameraPan()
    {

    }
}
