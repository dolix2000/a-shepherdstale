using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Tutorial View - Button events
/// </summary>
public class UIOptionsView : UIView
{
    private void Awake()
    {
        gameObject.name = "OptionsView";
    }

    // Close Tutorial Button - call click event
    public UnityAction OnCloseOptionsClicked;

    /// <summary>
    /// Method called by CloseTutorialButton
    /// </summary>
    /// 
    public void CloseOptionsClicked()
    {
        OnCloseOptionsClicked?.Invoke();
    }
}
