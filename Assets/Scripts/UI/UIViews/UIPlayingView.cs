using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// InGame 'Playing' View - Button events
/// </summary>
public class UIPlayingView : UIView
{
    private void Awake()
    {
        gameObject.name = "PlayingView";
    }

    // StartGame Button - call click event
    public UnityAction OnContinueGameClicked;

    /// <summary>
    /// Method called by ContinueGameButton
    /// </summary>
    /// 
    public void ContinueGameClicked()
    {
        OnContinueGameClicked?.Invoke();
    }

    public UnityAction OnRestartClicked;
    /// <summary>
    /// Method called by Restart Button
    /// </summary>
    /// 
    public void RestartClicked()
    {
        OnRestartClicked?.Invoke();
    }

    // ShowOptions Button - call click event
    public UnityAction OnShowOptionsClicked;

    /// <summary>
    /// Method called by Options button
    /// </summary>
    /// 
    public void ShowOptionsClicked()
    {
        OnShowOptionsClicked?.Invoke();
    }

    // ShowTutorial Button - call click event
    public UnityAction OnTutorialClicked;

    /// <summary>
    /// Method called by Tutorial button
    /// </summary>
    /// 
    public void ShowTutorialClicked()
    {
        OnTutorialClicked?.Invoke();
    }

    // QuitGame Button - call click event
    public UnityAction OnQuitGameClicked;

    /// <summary>
    /// Method called by Quit Game Button
    /// </summary>
    /// 
    public void QuitGameClicked()
    {
        OnQuitGameClicked?.Invoke();
    }

    // ReturnToMenu Button - call click event
    public UnityAction OnReturnToMenuClicked;

    /// <summary>
    /// Method called by Return to menu button
    /// </summary>
    /// 
    public void ReturnToMenuClicked()
    {
        OnReturnToMenuClicked?.Invoke();
    }

    public UnityAction ModalYesAction;
    /// <summary>
    /// Modal window to confirm quit action
    /// </summary>
    public void YesClicked()
    {
        ModalYesAction?.Invoke();
    }

    public UnityAction ModalNoAction;
    /// <summary>
    /// Modal window to revoke quit action
    /// </summary>
    /// 
    public void NoClicked()
    {
        ModalNoAction?.Invoke();
    }
}
