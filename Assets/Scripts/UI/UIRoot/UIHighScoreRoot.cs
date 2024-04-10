using UnityEngine;

/// <summary>
/// UI root class for HighScore controller.
/// </summary>
public class UIHighScoreRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UIHighScoreView highScoreView;
    public UIHighScoreView HighScoreView
    {
        get => highScoreView;
        set => highScoreView = value;
    }

    public override void ShowRoot()
    {
        base.ShowRoot();

        highScoreView.ShowView();
    }

    public override void HideRoot()
    {
        highScoreView.HideView();

        base.HideRoot();
    }
}
 