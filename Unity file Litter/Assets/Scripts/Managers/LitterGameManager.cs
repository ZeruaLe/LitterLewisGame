using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelID
{
    city,
    lake,
    beach
}

public class LitterGameManager : MonoBehaviour
{
    #region Singleton

    public static LitterGameManager instance { get; private set; }

    #endregion

    #region Variables

    [Header("Level Settings")]
    public int citySceneIndex = 1;
    public int lakeSceneIndex = 2;
    public int beachSceneIndex = 3;

    [Header("Menu Settings")]
    public GameObject menuGO;

    private static LitterLevel _levelCity;
    private static LitterLevel _levelLake;
    private static LitterLevel _levelBeach;
    private static LitterLevel _curLevel;

    #endregion

    #region Awake / Enable

    private void Awake()
    {
        // Init our instance
        if (instance == null)
            instance = this;

        if (instance == this)
        {
            // Awake
            DontDestroyOnLoad(gameObject);

            SceneManager.LoadSceneAsync(citySceneIndex, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(lakeSceneIndex, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(beachSceneIndex, LoadSceneMode.Additive);
        }
        else
        {
            // Destroy ourselves if we are not the correct manager
            Destroy(gameObject);
        }
    }

    #endregion

    #region Level

    public static void RegisterLevel(LitterLevel level)
    {
        switch (level.levelId)
        {
            case LevelID.city:
                _levelCity = level;
                break;
            case LevelID.lake:
                _levelLake = level;
                break;
            case LevelID.beach:
                _levelBeach = level;
                break;
        }

        level.gameObject.SetActive(false);
        //SceneManager.MoveGameObjectToScene(level.gameObject, SceneManager.GetActiveScene());
    }

    public static void SwitchLevel(LitterLevel levelToSwitchTo)
    {
        _curLevel.ToggleLevel(false);

        _curLevel = levelToSwitchTo;
        levelToSwitchTo.ToggleLevel(true);
    }

    public static void StartGame()
    {
        instance.menuGO.SetActive(false);
        _levelCity.ToggleLevel(true);
        _curLevel = _levelCity;
    }

    #endregion

    public void Update()
    {
        if (Input.GetKey(KeyCode.U))
        {
            if (_curLevel != _levelCity)
                SwitchLevel(_levelCity);

        } else if (Input.GetKey(KeyCode.I))
        {
            if (_curLevel != _levelLake)
                SwitchLevel(_levelLake);
        } else if(Input.GetKey(KeyCode.O))
        {
            if (_curLevel != _levelBeach)
                SwitchLevel(_levelBeach);
        }
    }
}
