using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LitterUI : MonoBehaviour
{
    #region Singleton
    public static LitterUI instance { get; private set; }

    #endregion

    #region Variables

    [Header("Level Text")]
    public Animation levelTextCity;
    public Animation levelTextLake;
    public Animation levelTextBeach;

    [Header("Loading")]
    public LoadingUI loadingUI;

    [Header("Collectables")]
    public GameObject collectablesUIGO;
    public Image collectablesUIImage;
    public List<Sprite> collectablesID;

    [Header("End Message")]
    public GameObject endMessageGO;

    #endregion

    #region Events

    public static UnityAction<bool> onCollectUIToggle;

    #endregion

    #region Awake / Destroy

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

    #endregion

    #region Enable / Disable

    private void OnEnable()
    {
        LitterGameManager.onNewLevel += OnNewLevelCallback;
        LitterGameManager.onShowEndMessage += OnShowEndMessage;
        LitterCollectable.OnCollectableObtain += OnCollectableObtain;
    }

    private void OnDisable()
    {
        LitterGameManager.onNewLevel -= OnNewLevelCallback;
        LitterGameManager.onShowEndMessage -= OnShowEndMessage;
        LitterCollectable.OnCollectableObtain -= OnCollectableObtain;
    }

    #endregion

    #region Event Callbacks

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

    private void OnCollectableObtain(int id)
    {
        if(id >= 0 && id < collectablesID.Count)
        {
            collectablesUIImage.sprite = collectablesID[id];
            collectablesUIGO.SetActive(true);

            onCollectUIToggle?.Invoke(true);
        }
    }

    private void OnShowEndMessage()
    {
        endMessageGO.SetActive(true);
    }

    #endregion

    #region Button Callbacks

    public void OnCollectableConfirm()
    {
        collectablesUIGO.SetActive(false);

        onCollectUIToggle?.Invoke(false);
    }

    public void OnEndGame()
    {
        endMessageGO.SetActive(false);
        LitterGameManager.instance.EndGame();
    }

    #endregion
}
