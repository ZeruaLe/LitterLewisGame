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
    public Transform cameraPanStartPoint;

    public bool isPanning => _isPanning;
    private bool _isPanning;
    private UnityAction _onPanComplete;

    #endregion

    private void OnEnable()
    {
        instance = this;
    }

    public void SetupForCameraPan()
    {
        playerCamera.enabled = false;
        transform.position = cameraPanStartPoint.position;
    }

    public void DoCameraPan(UnityAction onComplete)
    {
        _onPanComplete = onComplete;
        _isPanning = true;
        cameraPan.Play();
    }

    public void OnCameraPanComplete()
    {
        _isPanning=false;
        _onPanComplete?.Invoke();
    }
}
