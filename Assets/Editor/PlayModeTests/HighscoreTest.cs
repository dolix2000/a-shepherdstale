using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Highscore;
using NSubstitute;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

/// <summary>
/// Test class for all the scripts related to the Highscore
/// </summary>
public class HighscoreTests
{
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator HighscoreLayoutBuilt()
    {
        // Use the Assert class to test conditions.
        IHighscoreView highscoreView = GetMock();
        HighscoreController highscoreController = new HighscoreController(highscoreView);
        highscoreController.BuildHighscoreLayout(highscoreView.Canvas, "HighscoreTest", highscoreView.Font);

        // Use yield to skip a frame.
        yield return new WaitForSeconds(3f);

        // This returns the local active state of this GameObject, which is set using GameObject.SetActive.
        // Note that a GameObject may be inactive because a parent is not active, even if this returns true.
        // Means --> parent is also active right now

        // assert true if canvas is active
        Assert.True(highscoreView.Canvas.isActiveAndEnabled);
        foreach (Transform child in highscoreView.Canvas.transform)
        {
            // assert true if the children of the canvas are enabled
            Assert.True(child.gameObject.activeSelf);
        }
    }

    [UnityTest]
    public IEnumerator HeadColumnsActive()
    {
        IHighscoreView highscoreView = GetMock();
        HighscoreController highscoreController = new HighscoreController(highscoreView);

        highscoreController.BuildHighscoreLayout(highscoreView.Canvas, "HighscoreTest", highscoreView.Font);

        // Use yield to skip a frame.
        yield return new WaitForSeconds(3f);

        Assert.True(highscoreView.PlayerRow.activeSelf);
    }

    [UnityTest]
    public IEnumerator HasCorrectFont()
    {
        IHighscoreView highscoreView = GetMock();
        HighscoreController highscoreController = new HighscoreController(highscoreView);

        highscoreController.BuildHighscoreLayout(highscoreView.Canvas, "HighscoreTest", highscoreView.Font);

        TMP_FontAsset font = Resources.Load<TMP_FontAsset>("/UI/Font/Civane Cond Bold SDF.asset");

        // Use yield to skip a frame.
        yield return new WaitForSeconds(3f);

        Assert.True(highscoreView.Font == font);
    }

    /// <summary>
    /// Creating a mock of IHighscoreView for testing (refer to HOP)
    /// </summary>
    /// <returns>Mock of IHighscoreView</returns>
    private IHighscoreView GetMock()
    {
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Player_Row.prefab", typeof(GameObject));
        var highscoreView = Substitute.For<IHighscoreView>();
        highscoreView.Font = Resources.Load<TMP_FontAsset>("/UI/Font/Civane Cond Bold SDF.asset");
        highscoreView.PlayerRow = (GameObject)prefab;
        highscoreView.Canvas = new GameObject().AddComponent<Canvas>();

        return highscoreView;
    }
}

