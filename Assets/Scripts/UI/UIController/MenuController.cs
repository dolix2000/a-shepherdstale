using UnityEngine;
using TMPro;

/// <summary>
/// Controller for Main Menu in Title Scene
/// </summary>
public class MenuController : SubController<UIMenuRoot>
{
    #region Binding events
    public override void UseController()
    {
        // UI events getting attached
        ui.MenuView.OnStartGameClicked += StartGame;
        ui.MenuView.OnContinueFromSaveClicked += ContinueFromSave;
        ui.MenuView.OnOptionsClicked += ShowOptions;
        ui.MenuView.OnTutorialClicked += ShowTutorial;
        ui.MenuView.OnNewGameClicked += ShowSecondaryItems;
        ui.MenuView.OnReturnToMenuClicked += ShowPrimaryItems;
        ui.MenuView.OnHighScoresClicked += ShowHighscores;
        ui.MenuView.OnQuitGameClicked += QuitGame;

        base.UseController();
    }
    #endregion

    #region Unbinding events
    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.MenuView.OnStartGameClicked -= StartGame;
        ui.MenuView.OnContinueFromSaveClicked -= ContinueFromSave;
        ui.MenuView.OnOptionsClicked -= ShowOptions;
        ui.MenuView.OnTutorialClicked -= ShowTutorial;
        ui.MenuView.OnNewGameClicked -= ShowSecondaryItems;
        ui.MenuView.OnReturnToMenuClicked -= ShowPrimaryItems;
        ui.MenuView.OnHighScoresClicked -= ShowHighscores;
        ui.MenuView.OnQuitGameClicked -= QuitGame;
    }
    #endregion

    [SerializeField]
    private GameObject primaryMenuItems; // first and primary items in Main Menu
    [SerializeField]
    private GameObject secondaryMenuItems; // second set of menu elements after Start Game
    private Vector3 menuOrigin;
    [SerializeField]
    private MenuAnimation menuAnimationPrimary;
    [SerializeField]
    private MenuAnimation menuAnimationSecondary;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private TMP_InputField playerNameInput;
    [SerializeField]
    private GameObject background;
    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private GameObject generator;

    private void OnEnable()
    {
        ShowPrimaryItems(); // Reset view
        background.SetActive(true);
        mainCamera.SetActive(true);

    }

    private void Start()
    {
        Debug.Log("<color=white>Controller loaded: </color>" + this.GetType().Name);
        
        GameManager.Instance.SetGameState(GameState.MAIN_MENU); 
        Debug.Log("<color=green>Gamestate set to: </color>" + GameManager.Instance.GameState);

        menuOrigin = primaryMenuItems.transform.position;

        playerNameInput.gameObject.name = "PlayerNameInput";

        InvokeRepeating("CheckForSave", 0, 2f);

        StartCoroutine(GameManager.Instance.PlaySound(nameof(SoundFiles.TitleMusic)));

    }

    private void StartGame()
    {
        // reset player if new game is started
        GameManager.Instance.Player = new Player();
        GameManager.Instance.Player.Id = 0;
        GameManager.Instance.Player.IsNewGame = true;

        Debug.Log("Accessing Start Game..");
        var textInput = playerNameInput.text;

        if(textInput.Length != 0)
        {
            GameManager.Instance.Player.Player_Name = textInput;
        } else
        {
            GameManager.Instance.Player.Player_Name = "Anonymous";
        }
        
        GameManager.Instance.LevelLoadController.LoadLevel((int)LoadScene.LevelName.LevelOne);
        background.SetActive(false);
        mainCamera.SetActive(false);

        Generator gen = generator.GetComponent<Generator>();
        gen.transform.position = new Vector3(26, 15, -22);
        gen.StartPosition = gen.transform.position;
        GameManager.Instance.GetComponent<ObjectPoolManager>().ResetObjects();

        root.ChangeController(RootController.ControllerTypeEnum.Playing);
        SaveUtility.SavePlayer(GameManager.Instance.Player);
        GameManager.Instance.SetGameState(GameState.GAME_SCENE);
    }

    private void ShowPrimaryItems()
    {
        Debug.Log("Accessing Primary Items..");
        menuAnimationSecondary.SetDestination(new Vector3(-400, secondaryMenuItems.transform.position.y));
        menuAnimationPrimary.SetDestination(menuOrigin);
    }

    private void ShowSecondaryItems()
    {
        Debug.Log("Accessing Secondary Items..");
        menuAnimationPrimary.SetDestination(new Vector3(-400, primaryMenuItems.transform.position.y));
        menuAnimationSecondary.SetDestination(menuOrigin);
    }

    private void ContinueFromSave()
    {
        Debug.Log("Accessing Continue from save..");
        GameManager.Instance.Player.IsNewGame = false;

        GameManager.Instance.LevelLoadController.LoadLevel((int)LoadScene.LevelName.LevelOne);
        background.SetActive(false);

        generator.GetComponent<Generator>().StartPosition = new Vector3(26, 15, -22);
        GameManager.Instance.GetComponent<ObjectPoolManager>().ResetObjects();

        root.ChangeController(RootController.ControllerTypeEnum.Playing);
        GameManager.Instance.SetGameState(GameState.GAME_SCENE);
    }

    private void ShowOptions()
    {
        Debug.Log("Accessing Options menu..");
        root.ChangeController(RootController.ControllerTypeEnum.Options);
    }
    private void ShowTutorial()
    {
        Debug.Log("Accessing Tutorial..");
        root.ChangeController(RootController.ControllerTypeEnum.Tutorial);
    }

    private void ShowHighscores()
    {
        Debug.Log("Accessing Highscores..");
        root.ChangeController(RootController.ControllerTypeEnum.HighScore);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Quitting game..");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("Quitting game..");
		Application.Quit ();
#endif
    }

    private void CheckForSave()
    {
        Debug.Log("Checking For SaveGame");
        if (GameManager.Instance.Player.Player_Name != null && GameManager.Instance.Player.Player_Name != "Anonymous")
        {
            continueButton.SetActive(true);
        }
        else
        {
            continueButton.SetActive(false);
        }
    }
}
