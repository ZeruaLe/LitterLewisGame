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

    [Header("Debug")]
    public bool isDebug = false;

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
            transform.parent = null;
            DontDestroyOnLoad(gameObject);

            if (!isDebug)
            {
                SceneManager.LoadSceneAsync(citySceneIndex, LoadSceneMode.Additive);
                SceneManager.LoadSceneAsync(lakeSceneIndex, LoadSceneMode.Additive);
                SceneManager.LoadSceneAsync(beachSceneIndex, LoadSceneMode.Additive);
            } else
            {
                int index = SceneManager.GetActiveScene().buildIndex;
                if(index != 1)
                    SceneManager.LoadSceneAsync(citySceneIndex, LoadSceneMode.Additive);
                if (index != 2)
                    SceneManager.LoadSceneAsync(lakeSceneIndex, LoadSceneMode.Additive);
                if (index != 3)
                    SceneManager.LoadSceneAsync(beachSceneIndex, LoadSceneMode.Additive);
            }
        }
        else
        {
            // Destroy ourselves if we are not the correct manager
            Destroy(this);
        }
    }

    #endregion

    #region Level

    public void RegisterLevel(LitterLevel level)
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

        if(!isDebug)
            level.gameObject.SetActive(false);
        else
        {
            bool toggle = false;
            switch (level.levelId)
            {
                case LevelID.city:
                    toggle = SceneManager.GetActiveScene().buildIndex == citySceneIndex;
                    break;
                case LevelID.lake:
                    toggle = SceneManager.GetActiveScene().buildIndex == lakeSceneIndex;
                    break;
                case LevelID.beach:
                    toggle = SceneManager.GetActiveScene().buildIndex == beachSceneIndex;
                    break;
                default:
                    break;
            }
            level.gameObject.SetActive(toggle);
        }
    }

    public static void SwitchLevel(LevelID levelID)
    {
        if (instance.isDebug)
        {
            switch (levelID)
            {
                case LevelID.city:
                    if(SceneManager.GetActiveScene().buildIndex == instance.citySceneIndex)
                        SwitchLevel(_levelCity);
                    break;
                case LevelID.lake:
                    if (SceneManager.GetActiveScene().buildIndex == instance.lakeSceneIndex)
                        SwitchLevel(_levelLake);
                    break;
                case LevelID.beach:
                    if (SceneManager.GetActiveScene().buildIndex == instance.beachSceneIndex)
                        SwitchLevel(_levelBeach);
                    break;
            }
            return;
        }

        switch (levelID)
        {
            case LevelID.city:
                SwitchLevel(_levelCity);
                break;
            case LevelID.lake:
                SwitchLevel(_levelLake);
                break;
            case LevelID.beach:
                SwitchLevel(_levelBeach);
                break;
        }
    }

    public static void SwitchLevel(LitterLevel levelToSwitchTo)
    {
        if(_curLevel != null)
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
                SwitchLevel(LevelID.city);

        } else if (Input.GetKey(KeyCode.I))
        {
            if (_curLevel != _levelLake)
                SwitchLevel(LevelID.lake);
        } else if(Input.GetKey(KeyCode.O))
        {
            if (_curLevel != _levelBeach)
                SwitchLevel(LevelID.beach);
        }
    }
}
