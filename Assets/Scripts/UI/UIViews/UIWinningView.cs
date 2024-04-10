using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Post game 'Winning' View - Button events
/// </summary>
public class UIWinningView : UIView
{
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

    // Continue to credits scene - call click event
    public UnityAction OnContinueClicked;

    /// <summary>
    /// Method called by Continue to credits
    /// </summary>
    /// 
    public void ContinueToCredits()
    {
        OnContinueClicked?.Invoke();
    }
}
