using UnityEngine;

/// <summary>
/// UI root class for Winning controller.
/// </summary>
public class UICreditsRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UICreditsView creditsView;
    public UICreditsView CreditsView
    {
        get => creditsView;
        set => creditsView = value;
    }

    public override void ShowRoot()
    {
        base.ShowRoot();

        creditsView.ShowView();
    }

    public override void HideRoot()
    {
        creditsView.HideView();

        base.HideRoot();
    }
}
