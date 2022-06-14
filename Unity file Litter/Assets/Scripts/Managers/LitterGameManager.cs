using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private int _curLevelNumber;

    #endregion

    #region Events

    public static UnityAction<LevelID> onNewLevel;

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

                if (index != 0)
                    _curLevelNumber = index;

                if (index != 1)
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

        onNewLevel?.Invoke(levelToSwitchTo.levelId);
    }

    public void HandleGameFlow()
    {
        _curLevelNumber++;

        if (_curLevelNumber > 3)
        {
            Debug.Log("Ending Sequence");
        }
        else {
            LevelID levelToSwitchTo = LevelID.city;
            switch (_curLevelNumber)
            {
                case 1:
                    levelToSwitchTo = LevelID.city;
                    break;
                case 2:
                    levelToSwitchTo = LevelID.lake;
                    break;
                case 3:
                    levelToSwitchTo = LevelID.beach;
                    break;
            }

            // Switch to level;
            SwitchLevel(levelToSwitchTo);
        }
        
    }

    public void StartGame()
    {
        menuGO.SetActive(false);

        _curLevelNumber = 0;
        HandleGameFlow();
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
