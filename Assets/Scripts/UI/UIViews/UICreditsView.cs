using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Post game 'Credits' View - Button events
/// </summary>
public class UICreditsView : UIView
{
    private void Awake()
    {
        gameObject.name = "CreditsView";
    }

    // Show Scoreboard Button from winning screen - call click event
    public UnityAction OnShowScoreBoardClicked;

    /// <summary>
    /// Method called by ShowScoreBoardButton
    /// </summary>
    /// 
    public void ShowScoreBoardClicked()
    {
        OnShowScoreBoardClicked?.Invoke();
    }

    // Return to Menu Button - call click event
    public UnityAction OnReturnToMenuClicked;

    /// <summary>
    /// Method called by Return to Menu Button
    /// </summary>
    /// 
    public void ReturnToMenuClicked()
    {
        OnReturnToMenuClicked?.Invoke();
    }
}
