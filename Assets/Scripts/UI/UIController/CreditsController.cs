using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Controller for Credits Scene after winning the game.
/// </summary>
public class CreditsController : SubController<UICreditsRoot>
{
    public override void UseController()
    {
        // UI events getting detached
        ui.CreditsView.OnReturnToMenuClicked += ReturnToMenu;

        base.UseController();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.CreditsView.OnReturnToMenuClicked -= ReturnToMenu;

    }

    private void OnEnable()
    {
        GameManager.Instance.SetGameState(GameState.CREDITS);
        Debug.Log("<color=green>Gamestate set to: </color>" + GameManager.Instance.GameState);
    }

    private void ReturnToMenu()
    {
        GameManager.Instance.LevelLoadController.SwitchScene(SceneManager.GetSceneByName("MainScene"));
        GameManager.Instance.LevelLoadController.UnloadLevel((int)LoadScene.LevelName.LevelOne);
        GameManager.Instance.SetGameState(GameState.MAIN_MENU);
        root.ChangeController(RootController.ControllerTypeEnum.Menu);
        GameManager.Instance.PlaySound(nameof(SoundFiles.TitleMusic));
    }
}
