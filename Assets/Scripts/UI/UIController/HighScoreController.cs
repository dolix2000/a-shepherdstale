using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Highscore;

/// <summary>
/// Controller for post game 'highscore' scene
/// Uses some code from HighscoreController by Lisa Do - implemented snippets to scale down for current UI.
/// </summary>
public class HighScoreController : SubController<UIHighScoreRoot>
{
    private GameObject[] playerRows;

    [SerializeField]
    private GameObject playerTable;

    [SerializeField]
    private GameObject headerRow;

    [SerializeField]
    private GameObject playerRowPrefab;

    [SerializeField]
    private GameObject highScoreView;

    private VerticalLayoutGroup tableLayout;

    private GameObject tableHeader;

    private void Start()
    {
        playerTable.AddComponent<VerticalLayoutGroup>();
        headerRow.AddComponent<VerticalLayoutGroup>();
        ModifyPlayerTable(playerTable);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc pressed");
            CloseHighScore();
        }
    }

    public override void UseController()
    {
        // UI events getting attached
        ui.HighScoreView.OnCloseHighScoreClicked += CloseHighScore;
        ui.HighScoreView.OnRefreshClicked += RefreshScoreBoard;
        base.UseController();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.HighScoreView.OnCloseHighScoreClicked -= CloseHighScore;
        ui.HighScoreView.OnRefreshClicked -= RefreshScoreBoard;

    }

    private void OnEnable()
    {
        Debug.Log("<color=white>Controller loaded: </color>" + this.GetType().Name);

        if (GameManager.Instance.GameState.Equals(GameState.WIN_SCENE))
        {
            GameManager.Instance.SetGameState(GameState.HIGHSCORE_DISPLAY);

        } else if (GameManager.Instance.GameState.Equals(GameState.MAIN_MENU)) {
            return;
        }

        Debug.Log("<color=green>Gamestate set to: </color>" + GameManager.Instance.GameState);
    }

    private void CloseHighScore()
    {
        if (GameManager.Instance.GameState.Equals(GameState.HIGHSCORE_DISPLAY))
        {
            root.ChangeController(RootController.ControllerTypeEnum.Winning);
        } 
        else if (GameManager.Instance.GameState.Equals(GameState.MAIN_MENU))
        {
            root.ChangeController(RootController.ControllerTypeEnum.Menu);
        }
        else
        {
            return;
        }
        Debug.Log("<color=green>Gamestate set to: </color>" + GameManager.Instance.GameState);

    }

    private void RefreshScoreBoard()
    {
        Debug.Log("Refreshing ScoreBoard");
        ClearScoreBoard();
        GameManager.Instance.RequestThread.RequestCall();
        InstantiatePlayers(GameManager.Instance.RequestThread.GetPlayers());
    }

    /// <summary>
    /// Original code by Lisa Do in <HighscoreController cref="HighscoreController"/>.
    /// Method for instantiating the players into a table 
    /// as rows (prefab)
    /// Also saves the players into an array (as gameobjects if they need to be destroyed later)
    /// </summary>
    /// <param name="players">that get instantiated per row</param> 
    public void InstantiatePlayers(List<Player> player)
    {
        playerRows = new GameObject[player.Count];

        tableHeader = GameObject.Instantiate<GameObject>(playerRowPrefab, headerRow.transform, false);

        PlayerRow headerElements = tableHeader.GetComponent<PlayerRow>();

        headerElements.Name_Text.text = "Name";
        headerElements.Score_Text.text = "Score";
        headerElements.Time_Text.text = "Time taken";
        headerElements.Date_Text.text = "Date";

        // instantiating rows into the highscorelist scene (name, score, time)
        for (int i = 0; i < player.Count; i++)
        {
            // instantiate new playerRow (child) per player into playerTable (parent object)
            GameObject playerRow = GameObject.Instantiate<GameObject>(playerRowPrefab, playerTable.transform, false);

            PlayerRow textElements = playerRow.GetComponent<PlayerRow>();

            textElements.Name_Text.text = player[i].Player_Name;
            textElements.Score_Text.text = player[i].Score.ToString();
            textElements.Time_Text.text = player[i].Play_Time.ToString();
            textElements.Date_Text.text = player[i].Date.ToString();

            // creating reference of row for example destroying the object later if needed
            playerRows[i] = playerRow;
        }
    }

    /// <summary>
    /// Method to modify existing Table to ensure optimal fit for entries
    /// </summary>
    /// <param name="playerTable">Existing empty table</param> 
    private void ModifyPlayerTable(GameObject playerTable)
    {
        tableLayout = playerTable.GetComponent<VerticalLayoutGroup>();
        tableLayout.spacing = -9;
        tableLayout.padding.top = 30;
    }

    private void ClearScoreBoard()
    {
        for (int i = 0; i < playerRows.Length; i++)
        {
            Destroy(playerRows[i]);
        }
        Destroy(tableHeader);
    }
}
