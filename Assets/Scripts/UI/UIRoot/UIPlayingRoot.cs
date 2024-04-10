using UnityEngine;

/// <summary>
/// UI root class for in game 'playing' controller.
/// </summary>
public class UIPlayingRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UIPlayingView playingView;
    public UIPlayingView PlayingView
    {
        get => playingView;
        set => playingView = value;
    }

    public override void ShowRoot()
    {
        base.ShowRoot();

        playingView.ShowView();
    }

    public override void HideRoot()
    {
        playingView.HideView();

        base.HideRoot();
    }
}
 