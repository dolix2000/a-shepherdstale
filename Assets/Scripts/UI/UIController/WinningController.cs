using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

/// <summary>
/// Controller for Winning screen - Level completed UI.
/// Grants access to view Scoreboard and continue to credit scene.
/// </summary>
public class WinningController : SubController<UIWinningRoot>
{
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI playerScore;
    [SerializeField]
    private TextMeshProUGUI playerTime;


    public override void UseController()
    {
        // UI events getting attached
        ui.WinningView.OnShowScoreBoardClicked += ShowScoreBoard;
        ui.WinningView.OnContinueClicked += ContinueToCredits;

        base.UseController();
    }

    public override void ReleaseController()
    {
        base.ReleaseController();

        // UI events getting detached
        ui.WinningView.OnShowScoreBoardClicked -= ShowScoreBoard;
        ui.WinningView.OnContinueClicked -= ContinueToCredits;

    }

    private void OnEnable()
    {
        playerName.text = GameManager.Instance.Player.Player_Name;
        playerScore.text = "Score: " + GameManager.Instance.Player.PlayerPostData.Score.ToString();
        playerTime.text = "Time: "+ GameManager.Instance.Player.PlayerPostData.Play_Time.ToString();
    }

    private void ShowScoreBoard()
    {
        root.ChangeController(RootController.ControllerTypeEnum.HighScore);
    }

    private void ContinueToCredits()
    {
        root.ChangeController(RootController.ControllerTypeEnum.Credits);
    }
}
