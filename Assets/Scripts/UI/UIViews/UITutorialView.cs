using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Tutorial View - Button events
/// </summary>
public class UITutorialView : UIView
{
    // Close Tutorial Button - call click event
    public UnityAction OnCloseTutorialClicked;

    /// <summary>
    /// Method called by CloseTutorialButton
    /// </summary>
    /// 
    public void CloseTutorialClicked()
    {
        OnCloseTutorialClicked?.Invoke();
    }
}
