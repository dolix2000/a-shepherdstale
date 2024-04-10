using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// RootController changes states for game UI with their respective SubController
/// </summary>
public class RootController : MonoBehaviour
{
    // SubController types
    public enum ControllerTypeEnum
    {
        Menu,
        Options,
        Tutorial,
        Playing,
        HighScore,
        Winning,
        Credits
    }

    private static RootController instance;
    public static RootController Instance
    {
        get => RootController.instance;
        set => instance = value;
    }

    // References to SubController of every type
    [Header("Controllers")]
    [SerializeField]
    private MenuController menuController;
    public MenuController MenuController
    {
        get => menuController;
        set => menuController = value;
    }

    [SerializeField]
    private OptionsController optionsController;
    public OptionsController OptionsController
    {
        get => optionsController;
        set => optionsController = value;
    }

    [SerializeField]
    private TutorialController tutorialController;
    public TutorialController TutorialController
    {
        get => tutorialController;
        set => tutorialController = value;
    }

    [SerializeField]
    private PlayingController playingController;
    public PlayingController PlayingController
    {
        get => playingController;
        set => playingController = value;
    }


    [SerializeField]
    private HighScoreController highScoreController;
    public HighScoreController HighScoreController
    {
        get => highScoreController;
        set => highScoreController = value;
    }

    [SerializeField]
    private WinningController winningController;
    public WinningController WinningController
    {
        get => winningController;
        set => winningController = value;
    }

    [SerializeField]
    private CreditsController creditsController;
    public CreditsController CreditsController
    {
        get => creditsController;
        set => creditsController = value;
    }

    [SerializeField]
    private GameObject misc;
    public GameObject Misc
    {
        get => misc;
        set => misc = value;
    }

    public static List<RootController> rootInstance = new List<RootController>(10);

    public void Awake()
    {
        rootInstance.Add(this);
        gameObject.name = "UI_RootController";
    }

    /// <summary>
    /// Start method of Unity's MonoBehavior class gets called on first frame.
    /// </summary>
    private void Start()
    {
        Debug.Log("<color=white>Controller loaded: </color>" + this.GetType().Name);

        menuController.root = this;
        optionsController.root = this;
        tutorialController.root = this;
        playingController.root = this;
        highScoreController.root = this;
        winningController.root = this;
        creditsController.root = this;

        ChangeController(ControllerTypeEnum.Menu); // First Controller to engage is Menu controller

    }

    /// <summary>
    /// Change phases with SubControllers
    /// </summary>
    /// <param name="controller">Controller type</param>
    public void ChangeController(ControllerTypeEnum controller)
    {
        // Reset controller allocation
        ResetControllers();

        // Switching controllers based on type
        switch (controller)
        {
            case ControllerTypeEnum.Menu:
                menuController.UseController();
                break;
            case ControllerTypeEnum.Options:
                optionsController.UseController();
                break;
            case ControllerTypeEnum.Tutorial:
                tutorialController.UseController();
                break;
            case ControllerTypeEnum.Playing:
                playingController.UseController();
                break;
            case ControllerTypeEnum.HighScore:
                highScoreController.UseController();
                break;
            case ControllerTypeEnum.Winning:
                winningController.UseController();
                break;
            case ControllerTypeEnum.Credits:
                creditsController.UseController();
                break;
            default:
                break;
        }
    }

    public void ResetControllers()
    {
        menuController.ReleaseController();
        optionsController.ReleaseController();
        tutorialController.ReleaseController();
        playingController.ReleaseController();
        highScoreController.ReleaseController();
        winningController.ReleaseController();
        creditsController.ReleaseController();
    }
}
