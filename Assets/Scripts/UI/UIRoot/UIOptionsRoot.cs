using UnityEngine;

/// <summary>
/// UI root class for options controller.
/// </summary>
public class UIOptionsRoot : UIRoot
{
    // Reference to options menu view class.
    [SerializeField]
    private UIOptionsView optionsView;
    public UIOptionsView OptionsView
    {
        get => optionsView;
        set => optionsView = value;
    }

    public override void ShowRoot()
    {
        base.ShowRoot();

        optionsView.ShowView();
    }

    public override void HideRoot()
    {
        optionsView.HideView();

        base.HideRoot();
    }
}
 