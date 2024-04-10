using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Model of MenuBuilder - will not be used. Manually generates menu structure instead of Unity's Editor.
/// </summary>
public class MenuBuilder : MonoBehaviour
{
    // References
    #region Controllers
    GameObject ui_RootController;
    GameObject ui_MenuController;
    GameObject ui_OptionsController;
    GameObject ui_TutorialController;
    GameObject ui_PlayingController;
    GameObject ui_HighScoreController;
    GameObject ui_WinningController;
    GameObject ui_CreditsController;
    #endregion

    #region Roots
    GameObject menuRoot;
    GameObject optionsRoot;
    GameObject tutorialRoot;
    GameObject playingRoot;
    GameObject highScoreRoot;
    GameObject winningRoot;
    GameObject creditsRoot;
    #endregion

    #region Views
    GameObject menuView;
    GameObject optionsView;
    GameObject tutorialView;
    GameObject playingView;
    GameObject highScoreView;
    GameObject winningView;
    GameObject creditsView;

    Object menuPrefab;
    #endregion

    private List<GameObject> subControllers;


    void Awake()
    {
        subControllers = new List<GameObject>();
        BuildUIRoot();
        GetControllerReferences();
        GetRootReferences();
        LoadViews();
        GetViewReferences();
    }

    private void Update()
    {
        foreach (var item in SubController.controllerInstances)
        {
            Debug.Log("ControllerList: " + item.ToString());
        }
    }

    private void BuildUIRoot()
    {
        ui_RootController = new GameObject("UI_RootController", typeof(RootController));

        ui_MenuController = new GameObject("MenuController", typeof(MenuController));
        menuRoot = new GameObject("MenuRoot", typeof(UIMenuRoot));
        menuRoot.transform.SetParent(ui_MenuController.transform);
        menuView = new GameObject("MenuView", typeof(UIMenuView));
        menuView.transform.SetParent(menuRoot.transform);
        subControllers.Add(ui_MenuController);

        ui_OptionsController = new GameObject("OptionsController", typeof(OptionsController));
        optionsRoot = new GameObject("OptionsRoot", typeof(UIOptionsRoot));
        optionsRoot.transform.SetParent(ui_OptionsController.transform);
        optionsView = new GameObject("OptionsView", typeof(UIOptionsView));
        optionsView.transform.SetParent(optionsRoot.transform);
        subControllers.Add(ui_OptionsController);

        ui_TutorialController = new GameObject("TutorialController", typeof(TutorialController));
        tutorialRoot = new GameObject("TutorialRoot", typeof(UITutorialRoot));
        tutorialRoot.transform.SetParent(ui_TutorialController.transform);
        tutorialView = new GameObject("TutorialView", typeof(UITutorialView));
        tutorialView.transform.SetParent(tutorialRoot.transform);
        subControllers.Add(ui_TutorialController);

        ui_PlayingController = new GameObject("PlayingController", typeof(PlayingController));
        playingRoot = new GameObject("PlayingRoot", typeof(UITutorialRoot));
        playingRoot.transform.SetParent(ui_PlayingController.transform);
        playingView = new GameObject("PlayingView", typeof(UIPlayingView));
        playingView.transform.SetParent(playingRoot.transform);
        subControllers.Add(ui_PlayingController);

        ui_HighScoreController = new GameObject("HighScoreController", typeof(HighScoreController));
        highScoreRoot = new GameObject("HighScoreRoot", typeof(UIHighScoreRoot));
        highScoreRoot.transform.SetParent(ui_HighScoreController.transform);
        highScoreView = new GameObject("HighScoreView", typeof(UIHighScoreView));
        highScoreView.transform.SetParent(highScoreRoot.transform);
        subControllers.Add(ui_HighScoreController);

        ui_WinningController = new GameObject("WinningController", typeof(WinningController));
        winningRoot = new GameObject("WinningRoot", typeof(UIWinningRoot));
        winningRoot.transform.SetParent(ui_WinningController.transform);
        winningView = new GameObject("WinningView", typeof(UIWinningView));
        winningView.transform.SetParent(winningRoot.transform);
        subControllers.Add(ui_WinningController);

        ui_CreditsController = new GameObject("CreditsController", typeof(CreditsController));
        creditsRoot = new GameObject("CreditsRoot", typeof(UICreditsRoot));
        creditsRoot.transform.SetParent(ui_CreditsController.transform);
        creditsView = new GameObject("CreditsView", typeof(UICreditsView));
        creditsView.transform.SetParent(creditsRoot.transform);
        subControllers.Add(ui_CreditsController);

        foreach(var subController in subControllers)
        {
            subController.transform.SetParent(ui_RootController.transform);
        }

        Debug.Log("Added Menu structure");
    }

    private void GetControllerReferences()
    {
        ui_RootController.GetComponent<RootController>().MenuController = ui_MenuController.GetComponent<MenuController>();
        ui_RootController.GetComponent<RootController>().OptionsController = ui_OptionsController.GetComponent<OptionsController>();
        ui_RootController.GetComponent<RootController>().TutorialController = ui_TutorialController.GetComponent<TutorialController>();
        ui_RootController.GetComponent<RootController>().PlayingController = ui_PlayingController.GetComponent<PlayingController>();
        ui_RootController.GetComponent<RootController>().HighScoreController = ui_HighScoreController.GetComponent<HighScoreController>();
        ui_RootController.GetComponent<RootController>().WinningController = ui_WinningController.GetComponent<WinningController>();
        ui_RootController.GetComponent<RootController>().CreditsController = ui_CreditsController.GetComponent<CreditsController>();

        Debug.Log("Added Controller nodes");
    }

    public void GetRootReferences()
    {
        ui_MenuController.GetComponent<MenuController>().UI = menuRoot.GetComponent<UIMenuRoot>();
        ui_OptionsController.GetComponent<OptionsController>().UI = optionsRoot.GetComponent<UIOptionsRoot>();
        ui_TutorialController.GetComponent<TutorialController>().UI = tutorialRoot.GetComponent<UITutorialRoot>();
        ui_PlayingController.GetComponent<PlayingController>().UI = playingRoot.GetComponent<UIPlayingRoot>();
        ui_HighScoreController.GetComponent<HighScoreController>().UI = highScoreRoot.GetComponent<UIHighScoreRoot>();
        ui_WinningController.GetComponent<WinningController>().UI = winningRoot.GetComponent<UIWinningRoot>();
        ui_CreditsController.GetComponent<CreditsController>().UI = creditsRoot.GetComponent<UICreditsRoot>();
    }

    private void GetViewReferences()
    {
        menuRoot.GetComponent<UIMenuRoot>().MenuView = menuView.GetComponent<UIMenuView>();
        optionsRoot.GetComponent<UIOptionsRoot>().OptionsView = optionsView.GetComponent<UIOptionsView>();
        tutorialRoot.GetComponent<UITutorialRoot>().TutorialView = tutorialView.GetComponent<UITutorialView>();
        playingRoot.GetComponent<UIPlayingRoot>().PlayingView = playingView.GetComponent<UIPlayingView>();
        highScoreRoot.GetComponent<UIHighScoreRoot>().HighScoreView = highScoreView.GetComponent<UIHighScoreView>();
        winningRoot.GetComponent<UIWinningRoot>().WinningView = winningView.GetComponent<UIWinningView>();
        creditsRoot.GetComponent<UICreditsRoot>().CreditsView = creditsView.GetComponent<UICreditsView>();

        Debug.Log("Added Menu views");
    }

    private void LoadViews()
    {
        menuPrefab = Resources.Load("UI/MainMenuView");
        menuView = (GameObject)Instantiate(menuPrefab);
    }

}