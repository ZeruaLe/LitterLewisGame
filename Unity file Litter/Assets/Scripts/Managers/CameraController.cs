using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Singleton

    public static CameraController instance { get; private set; }

    #endregion

    #region Variables

    public Camera myCamera;


    #endregion

    private void OnEnable()
    {
        instance = this;
    }
}
