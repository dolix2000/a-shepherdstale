using UnityEngine;

/// <summary>
/// Controller for Tutorial pages
/// </summary>
public class TutorialController : SubController<UITutorialRoot>
{
    public override void UseController()
    {
        // UI events getting attached
        ui.TutorialView.OnCloseTutorialClicked += CloseTutorial;

        base.UseController();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.TutorialView.OnCloseTutorialClicked -= CloseTutorial;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc pressed");
            CloseTutorial();
        }
    }

    private void CloseTutorial()
    {
        if (GameManager.Instance.GameState.Equals(GameState.PAUSE_SCREEN))
        {
            root.ChangeController(RootController.ControllerTypeEnum.Playing);
        }
        else
        {
            root.ChangeController(RootController.ControllerTypeEnum.Menu);
        }
    }
}
