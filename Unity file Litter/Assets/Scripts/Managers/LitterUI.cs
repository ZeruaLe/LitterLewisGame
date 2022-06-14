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