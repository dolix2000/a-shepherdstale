using UnityEngine;
using Assets.Scripts.Highscore;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Post game 'HighScore' View - Button events and data input
/// </summary>
public class UIHighScoreView : UIView, IHighscoreView
{
    [SerializeField]
    private GameObject playerRow;

    [SerializeField]
    private GameObject playerTable;

    [SerializeField]
    HighScoreController highScoreController;

    public GameObject PlayerTable
    {
        get { return playerTable; }
        set { playerTable = value; }
    }

    public GameObject PlayerRow
    {
        get { return playerRow; }
        set { playerRow = value; }
    }

    [SerializeField]
    private TMP_FontAsset arialFont;
    public TMP_FontAsset Font
    {
        get { return arialFont; }
        set { arialFont = Font; }
    }

    private Canvas canvas;
    public Canvas Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }

    private void Awake()
    {
        GameManager.Instance.RequestThread.RequestCall();
    }

    /// <summary>
    /// Logic Methods from the HighscoreController
    /// </summary>
    void Start()
    {
        highScoreController.InstantiatePlayers(GameManager.Instance.RequestThread.GetPlayers());
    }

    /// Button events in View

    // StartGame Button - call click event
    public UnityAction OnCloseHighScoreClicked;

    /// <summary>
    /// Method called by StartGameButton
    /// </summary>
    /// 
    public void CloseHighScore()
    {
        OnCloseHighScoreClicked?.Invoke();
    }

    // Refresh Button - call click event
    public UnityAction OnRefreshClicked;

    /// <summary>
    /// Method called by Refresh Scoreboard button
    /// </summary>
    /// 
    public void RefreshHighScore()
    {
        OnRefreshClicked?.Invoke();
    }
}
