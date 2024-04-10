using UnityEngine;

/// <summary>
/// Controller for options
/// </summary>
public class OptionsController : SubController<UIOptionsRoot>
{
    public override void UseController()
    {
        // UI events getting attached
        ui.OptionsView.OnCloseOptionsClicked += CloseOptions;
        
        base.UseController();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.OptionsView.OnCloseOptionsClicked -= CloseOptions;

    }
    private void Start()
    {
        Debug.Log("<color=white>Controller loaded: </color>" + this.GetType().Name);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc pressed");
            CloseOptions();
        }
    }

    private void CloseOptions()
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
