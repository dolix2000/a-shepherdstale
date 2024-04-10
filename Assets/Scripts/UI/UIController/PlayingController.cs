using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Controller for InGame - 'Playing' Scenes
/// </summary>
public class PlayingController : SubController<UIPlayingRoot>
{
    public override void UseController()
    {
        // UI events getting attached
        ui.PlayingView.OnContinueGameClicked += ContinueGame;
        ui.PlayingView.OnRestartClicked += RestartRun;
        ui.PlayingView.OnShowOptionsClicked += ShowOptions;
        ui.PlayingView.OnTutorialClicked += ShowTutorial;
        ui.PlayingView.OnReturnToMenuClicked += ReturnToMenu;
        ui.PlayingView.ModalYesAction += ModalYesAction;
        ui.PlayingView.ModalNoAction += ModalNoAction;

        base.UseController();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.PlayingView.OnContinueGameClicked -= ContinueGame;
        ui.PlayingView.OnRestartClicked -= RestartRun;
        ui.PlayingView.OnShowOptionsClicked -= ShowOptions;
        ui.PlayingView.OnTutorialClicked -= ShowTutorial;
        ui.PlayingView.OnReturnToMenuClicked -= ReturnToMenu;
        ui.PlayingView.ModalYesAction -= ModalYesAction;
        ui.PlayingView.ModalNoAction -= ModalNoAction;
    }

    [SerializeField]
    private MenuAnimation menuAnimation;
    private Vector3 menuOrigin;
    [SerializeField]
    private GameObject slidableMenu;
    [SerializeField]
    private GameObject PlayerText;
    [SerializeField]
    private TextMeshProUGUI PlayerScore;
    [SerializeField]
    private TextMeshProUGUI PlayerTime;
    [SerializeField]
    private GameObject modalWindow;

    public static bool isPaused = false;

    private void Start()
    {
        Debug.Log("<color=white>Controller loaded: </color>" + this.GetType().Name);
        GameManager.Instance.SetGameState(GameState.GAME_SCENE);
        Debug.Log("<color=green>Gamestate set to: </color>" + GameManager.Instance.GameState);

        StartCoroutine(GameManager.Instance.PlaySound(nameof(SoundFiles.GameMusic)));

        modalWindow.SetActive(false);
        menuOrigin = new Vector3(slidableMenu.transform.position.x, slidableMenu.transform.position.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !modalWindow.activeSelf) // Open Slide-In Menu on ESC-Keypress
        {
            Debug.Log("Esc for menu pressed");

            ShowHidePanel();
        }

        if (Input.GetKeyDown(KeyCode.R)) // Restart current run with R-Keypress
        {
            Debug.Log("Restart Run");
            RestartRun();
        }

        PlayerScore.text = GameManager.Instance.Player.CollectibleCount.ToString();                                         // Updating Sheep counter
        PlayerTime.text = GameManager.Instance.Player.GetConvertedTimeSpent();                                              // Updating Time counter

        if (GameManager.Instance.Player.LevelCompleted && GameManager.Instance.GameState.Equals(GameState.WIN_SCENE))       // Check for Winning conditions to end level.
        {
            root.ChangeController(RootController.ControllerTypeEnum.Winning);
        }
    }
    /// <summary>
    /// Pausing the game by setting pause boolean to true
    /// </summary>
    private void PauseGame()
    {
        Debug.Log("Pausing game..");
        isPaused = true;
    }

    /// <summary>
    /// Unpausing the game by setting pause boolean to false
    /// </summary>
    private void UnPauseGame()
    {
        Debug.Log("Unpausing game..");
        isPaused = false;
    }

    /// <summary>
    /// Continue game by closing Slide-In menu.
    /// </summary>
    private void ContinueGame()
    {
        Debug.Log("Continuing game..");
        ShowHidePanel();
    }

    /// <summary>
    /// Restart current run - reloads whole level and resets menu position.
    /// </summary>
    private void RestartRun()
    {
        GameManager.Instance.Player.ResetPlayerStats();
        GameManager.Instance.LevelLoadController.RestartLevel((int)LoadScene.LevelName.LevelOne);
        if (isPaused) { ShowHidePanel(); }
    }

    /// <summary>
    /// Accessing Options in pause mode.
    /// </summary>
    private void ShowOptions()
    {
        Debug.Log("Accessing Options..");
        root.ChangeController(RootController.ControllerTypeEnum.Options);
    }

    /// <summary>
    /// Accessing Tutorial in pause mode.
    /// </summary>
    private void ShowTutorial()
    {
        Debug.Log("Accessing Tutorial..");
        root.ChangeController(RootController.ControllerTypeEnum.Tutorial);
    }

    /// <summary>
    /// Return to main menu from pause mode.
    /// </summary>
    private void ReturnToMenu()
    {
        Debug.Log("Accessing Main menu..");
        ShowHideModal();
        ResetPanel();
    }

    /// <summary>
    /// Slide-In menu function. Calls animation, calls pause/unpause according to previous state.
    /// </summary>
    public void ShowHidePanel()
    {
        Debug.Log("ShowHideMenu");
        if (slidableMenu != null)
        {
            if (!isPaused)
            {
                Debug.Log("Show Menu");
                menuAnimation.SetDestination(new Vector3(0, slidableMenu.transform.position.y));
                PauseGame();
                GameManager.Instance.SetGameState(GameState.PAUSE_SCREEN);
            }
            else 
            {
                Debug.Log("Hide Menu");
                menuAnimation.SetDestination(menuOrigin);
                UnPauseGame();
                GameManager.Instance.SetGameState(GameState.GAME_SCENE);
            }
        }
    }

    /// <summary>
    /// Reset menu position to origin.
    /// </summary>
    private void ResetPanel()
    {
        menuAnimation.SetDestination(menuOrigin);
    }

    /// <summary>
    /// Modal window to ask for action yes/no.
    /// </summary>
    private void ShowHideModal()
    {
        if (!modalWindow.activeSelf)
        {
            modalWindow.SetActive(true);
        } else
        {
           modalWindow.SetActive(false);
        }   
    }
    /// <summary>
    /// Modal window action YES - ends current run.
    /// </summary>
    public void ModalYesAction()
    {
        ShowHideModal();
        UnPauseGame();
        GameManager.Instance.Player.ResetPlayerStats();
        GameManager.Instance.LevelLoadController.SwitchScene(SceneManager.GetSceneByName("MainScene"));
        GameManager.Instance.LevelLoadController.UnloadLevel((int)LoadScene.LevelName.LevelOne);
        GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
    }

    /// <summary>
    /// Modal window action NO - continues current run, closes menu.
    /// </summary>
    public void ModalNoAction()
    {
        ShowHideModal();
        ShowHidePanel();
    }
}
