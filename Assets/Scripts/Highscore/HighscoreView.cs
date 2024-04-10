using Assets.Scripts.Highscore;
using TMPro;
using UnityEngine;

/// <summary>
/// HighscoreView is responsible for the presentation of the players with the highest highscore.
/// The logic is implemented by the HighscoreController such as building the highscore layout, by providing specific parameters.
/// </summary>
public class HighscoreView : MonoBehaviour, IHighscoreView
{
    [SerializeField]
    private GameObject playerRow;
    public GameObject PlayerRow
    {
        get { return playerRow; }
        set { playerRow = value; }
    }

    [SerializeField]
    private TMP_FontAsset font;
    public TMP_FontAsset Font
    {
        get { return font; }
        set { font = Font; }
    }

    private Canvas canvas;
    public Canvas Canvas
    {
        get { return canvas; }
        set { canvas = value; }
    }

    private string[] headColumns;

    private HighscoreController highscoreController;

    private void Awake()
    {
        highscoreController = new HighscoreController(this);

        // initialising headcolumns for th player table
        headColumns = new string[] { "Name", "Score", "Time", "Date" };

        // creating the canvas for the scene
        canvas = new GameObject("Canvas").AddComponent<Canvas>();
    }

    // Start is called before the first frame update
    /// <summary>
    /// Implements Logic Methods from the HighscoreController
    /// </summary>
    private void Start()
    {
        highscoreController.BuildHighscoreLayout(canvas, "Highscore", font);
        highscoreController.InstantiateHeadColumns(headColumns);
        highscoreController.InstantiatePlayers(GameManager.Instance.RequestThread.GetPlayers());
    }
}

