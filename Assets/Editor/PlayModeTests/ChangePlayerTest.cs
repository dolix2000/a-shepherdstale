using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assets.Scripts.Movement;
using UnityEngine.SceneManagement;

public class ChangePlayerTest
{
    [SetUp]
    public void SetUp()
    {
    }

    [UnityTest]
    public IEnumerator CheckPlayerType()
    {
        GameObject gm = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/GameMaster/_GameMaster"));
        gm.GetComponent<ObjectPoolManager>().CloudParent = new GameObject();

        GameObject player = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Player/Player"));
        ChangePlayer currentPlayer = player.GetComponent<ChangePlayer>();

        currentPlayer.SwitchPlayer();

        Assert.AreEqual(PlayerType.DOG, currentPlayer.CurrentPlayer);
        yield return new WaitForSeconds(0f);
    }
}