using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LitterUI : MonoBehaviour
{
    [Header("Level Text")]
    public Animation levelTextCity;
    public Animation levelTextLake;
    public Animation levelTextBeach;

    [Header("Loading")]
    public LoadingUI loadingUI;


    #region Singleton
    public static LitterUI instance { get; private set; }

    #endregion

    private void Awake()
    {
        // Init our instance
        if (instance == null)
            instance = this;

        if (instance == this)
        {
            

        }
        else
        {
            // Destroy ourselves if we are not the correct manager
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        LitterGameManager.onNewLevel += OnNewLevelCallback;
    }

    private void OnDisable()
    {
        LitterGameManager.onNewLevel -= OnNewLevelCallback;
    }

    private void OnNewLevelCallback(LevelID level)
    {
        switch (level)
        {
            case LevelID.city:
                levelTextCity.Play();
                break;
            case LevelID.lake:
                levelTextLake.Play();
                break;
            case LevelID.beach:
                levelTextBeach.Play();
                break;
        }
    }
}
