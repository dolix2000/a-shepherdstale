using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Menu View - Button events
/// </summary>
public class UIMenuView : UIView
{
    private void Awake()
    {
        gameObject.name = "MainMenuView";
    }

    // StartGame Button - call click event
    public UnityAction OnStartGameClicked;

    /// <summary>
    /// Method called by StartGameButton
    /// </summary>
    /// 
    public void StartGameClicked()
    {
        OnStartGameClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    // NewGame Button - call click event
    public UnityAction OnReturnToMenuClicked;

    /// <summary>
    /// Method called by ReturnToMenu
    /// </summary>
    /// 
    public void ReturnToMenuClicked()
    {
        OnReturnToMenuClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    // NewGame Button - call click event
    public UnityAction OnNewGameClicked;

    /// <summary>
    /// Method called by NewGameButton
    /// </summary>
    /// 
    public void NewGameClicked()
    {
        OnNewGameClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    // ContinueGame Button - call click event
    public UnityAction OnContinueFromSaveClicked;

    /// <summary>
    /// Method called by Continue From Save button
    /// </summary>
    /// 
    public void ContinueFromSaveClicked()
    {
        OnContinueFromSaveClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    /// <summary>
    /// Method called by Options button
    /// </summary>
    /// 

    // Options Button - call click event
    public UnityAction OnOptionsClicked;

    public void OptionsClicked()
    {
        OnOptionsClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    // Tutorial Button - call click event
    public UnityAction OnTutorialClicked;

    public void TutorialClicked()
    {
        OnTutorialClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    // HighScore Button - call click event
    public UnityAction OnHighScoresClicked;

    public void ShowHighScore()
    {
        OnHighScoresClicked?.Invoke();
    }

    //
    // - - - - - - - - - - - - - - - - - - - - 
    //

    // QuitGame Button - call click event
    public UnityAction OnQuitGameClicked;

    /// <summary>
    /// Method called by StartGameButton
    /// </summary>
    /// 
    public void QuitGameClicked()
    {
        OnQuitGameClicked?.Invoke();
    }
}
