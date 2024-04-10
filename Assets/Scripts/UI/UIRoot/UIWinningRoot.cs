using UnityEngine;

/// <summary>
/// UI root class for Winning controller.
/// </summary>
public class UIWinningRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UIWinningView winningView;
    public UIWinningView WinningView
    {
        get => winningView;
        set => winningView = value;
    }

    public override void ShowRoot()
    {
        base.ShowRoot();

        winningView.ShowView();
    }

    public override void HideRoot()
    {
        winningView.HideView();

        base.HideRoot();
    }
}
