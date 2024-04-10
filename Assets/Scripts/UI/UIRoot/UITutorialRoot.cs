using UnityEngine;

/// <summary>
/// UI root class for tutorial controller.
/// </summary>
public class UITutorialRoot : UIRoot
{
    // Reference to menu view class.
    [SerializeField]
    private UITutorialView tutorialView;
    public UITutorialView TutorialView
    {
        get => tutorialView;
        set => tutorialView = value;
    }

    public override void ShowRoot()
    {
        base.ShowRoot();

        tutorialView.ShowView();
    }

    public override void HideRoot()
    {
        tutorialView.HideView();

        base.HideRoot();
    }
}
 