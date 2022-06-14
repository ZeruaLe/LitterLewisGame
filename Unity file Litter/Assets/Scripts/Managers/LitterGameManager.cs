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

    [Header("Debug")]
    public bool isDebug = false;

    public bool isGameReady => _isGameReady;

    private static LitterLevel _levelCity;
    private static LitterLevel _levelLake;
    private static LitterLevel _levelBeach;
    private static LitterLevel _curLevel;

    private int _curLevelNumber;
    private bool _isGameReady;

    #endregion

    #region Events

    public static UnityAction<LevelID> onNewLevel;
    public static UnityAction onStartGame;
    public static UnityAction onShowEndMessage;

    #endregion

    #region Awake / Enable

    private void Start()
    {
        SoundManagerScript.instance.Play("MenuTheme");
    }

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

            _isGameReady = false;

            LoadLevels();
        }
        else
        {
            // Destroy ourselves if we are not the correct manager
            Destroy(gameObject);
        }
    }

    private void LoadLevels()
    {
        if (!isDebug)
        {
            SceneManager.LoadSceneAsync(citySceneIndex, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(lakeSceneIndex, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(beachSceneIndex, LoadSceneMode.Additive);
        }
        else
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
        _isGameReady = true;
        onStartGame?.Invoke();

        _curLevelNumber = 0;
        HandleGameFlow();
    }

    public void FinishGame()
    {
        if (_curLevelNumber != 3)
            return;

        StartCoroutine(FinishGameRoutine());
    }

    private IEnumerator FinishGameRoutine()
    {
        // Set end theme up.
        SoundManagerScript.instance.Play("EndTheme");

        _levelBeach.levelPlayer.gameObject.SetActive(false);
        _levelBeach.levelCamera.SetupForCameraPan();

        // Do Beach Pan
        bool beachPanComplete = false;
        _levelBeach.levelCamera.DoCameraPan(() => beachPanComplete = true) ;
        yield return new WaitUntil(() => beachPanComplete);
        
        // Fade in to transition to Lake
        bool fadeInComplete = false;
        LitterUI.instance.loadingUI.FadeIn(() => fadeInComplete = true);
        yield return new WaitUntil(() => fadeInComplete);

        // Toggle on Lake.
        _levelBeach.gameObject.SetActive(false);
        _levelLake.EnableForPan();
        yield return new WaitForSeconds(0.5f);

        // Fade back in
        bool fadeOutComplete = false;
        LitterUI.instance.loadingUI.FadeOut(() => fadeOutComplete = true);
        yield return new WaitUntil(() => fadeOutComplete);

        // Do Lake Pan
        bool lakePanComplete = false;
        _levelLake.levelCamera.DoCameraPan(() => lakePanComplete = true);
        yield return new WaitUntil(() => lakePanComplete);

        // Fade in to transition to City
        fadeInComplete = false;
        LitterUI.instance.loadingUI.FadeIn(() => fadeInComplete = true);
        yield return new WaitUntil(() => fadeInComplete);

        // Toggle on City.
        _levelLake.gameObject.SetActive(false);
        _levelCity.EnableForPan();
        yield return new WaitForSeconds(0.5f);

        // Fade back in
        fadeOutComplete = false;
        LitterUI.instance.loadingUI.FadeOut(() => fadeOutComplete = true);
        yield return new WaitUntil(() => fadeOutComplete);

        // Do City Pan
        bool cityPanComplete = false;
        _levelCity.levelCamera.DoCameraPan(() => cityPanComplete = true);
        yield return new WaitUntil(() => cityPanComplete);

        // Fade in to transition to End Message
        fadeInComplete = false;
        LitterUI.instance.loadingUI.FadeIn(() => fadeInComplete = true);
        yield return new WaitUntil(() => fadeInComplete);

        onShowEndMessage?.Invoke();

        // Fade back in
        fadeOutComplete = false;
        LitterUI.instance.loadingUI.FadeOut(() => fadeOutComplete = true);
        yield return new WaitUntil(() => fadeOutComplete);
    }

    public void EndGame()
    {
        _isGameReady = false;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        LoadLevels();
    }

    #endregion

    public void Update()
    {
#if UNITY_EDITOR
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
#endif
    }
}
