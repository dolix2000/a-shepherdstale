using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TMPro;

public class ControllerTest
{
    private RootController rootController;
    private UICreditsView creditsView;
    private UIMenuView menuView;
    private TMP_InputField playerNameInput;

    // SetUp for Playtesting - Loading necessary scenes.
    [SetUp]
    public void InitControllerTest()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.LoadSceneAsync("LevelOne", LoadSceneMode.Additive);
    }

    // Testing, if UI Controller gets switched properly.
    // Testing is successful, if game state has changed accordingly.
    [UnityTest]
    public IEnumerator SwitchToPlayingController()
    {
        rootController = GameObject.Find("UI_RootController").GetComponent<RootController>();
        rootController.ChangeController(RootController.ControllerTypeEnum.Playing);
        yield return new WaitForSeconds(1);

        Assert.True(GameManager.Instance.GameState.Equals(GameState.GAME_SCENE));
    }

    // Testing, if game switches back to main menu after the level has been finished.
    // Testing is successful, if game state has changed accordingly.
    [UnityTest]
    public IEnumerator ReturnToMenuAfterLevelFinished()
    {
        rootController = GameObject.Find("UI_RootController").GetComponent<RootController>();
        rootController.ChangeController(RootController.ControllerTypeEnum.Credits);
        creditsView = GameObject.Find("CreditsView").GetComponent<UICreditsView>();

        yield return new WaitForSeconds(1);
        creditsView.OnReturnToMenuClicked();

        Assert.That(GameManager.Instance.GameState, Is.EqualTo(GameState.MAIN_MENU));
    }

    // Testing, if game switches back to main menu after the level has been finished.
    // Testing is successful, if game state has changed accordingly.
    [UnityTest]
    public IEnumerator PlayerNameGetsPassedFromInputField()
    {
        GameObject gm = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/GameMaster/_GameMaster"));
        gm.GetComponent<ObjectPoolManager>().CloudParent = new GameObject();
        playerNameInput = GameObject.Find("PlayerNameInput").GetComponent<TMP_InputField>();
        playerNameInput.text = "TestName123";

        Assert.That(GameManager.Instance.Player.Player_Name, Is.EqualTo("TestName123")); 
        
        yield return new WaitForSeconds(1);
    }
}

