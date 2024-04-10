using System.Collections;
using Assets.Scripts.ItemFactory;
using Handler;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Test class for testing the boosts (all scripts related to the boost)
/// </summary>
public class BoostTests
{
    private GameObject parentObj;

    /// <summary>
    /// Setting up variables and initialising them before testing
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        parentObj = new GameObject();
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BoostsCreated()
    {
        // Use the Assert class to test conditions
        IBoostHandler boostHandler = GetMock();
        BoostFactory boostFactory = new BoostFactory();
        BoostController boostController = new BoostController(boostHandler, boostFactory);
        boostController.CreateBoosts();

        yield return new WaitForSeconds(3f); // waiting for the boost to generate

        Assert.True(boostHandler.BoostEntry[0].Boost.BoostObj.activeSelf &&
            boostHandler.BoostEntry[1].Boost.BoostObj.activeSelf);
    }

    [UnityTest]
    public IEnumerator RightBoostType()
    {
        // Use the Assert class to test conditions
        IBoostHandler boostHandler = GetMock();
        BoostFactory boostFactory = new BoostFactory();
        BoostController boostController = new BoostController(boostHandler, boostFactory);
        boostController.CreateBoosts();

        yield return new WaitForSeconds(3f); // waiting for the boost to generate

        Assert.AreEqual(BoostType.SPEEDBOOST, boostHandler.BoostEntry[0].Boost.BoostType);
    }

    [UnityTest]
    public IEnumerator ReturnsCorrectAmount()
    {
        IBoostHandler boostHandler = GetMock();
        BoostFactory boostFactory = new BoostFactory();
        BoostController boostController = new BoostController(boostHandler, boostFactory);
        boostController.CreateBoosts();

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(2, boostHandler.Quantity);
    }

    /// <summary>
    /// Creating a mock of IBoostHandler for testing (refer to HOP)
    /// </summary>
    /// <returns>Mock of IBoostHandler</returns>
    private IBoostHandler GetMock()
    {
        var boostHandler = Substitute.For<IBoostHandler>();
        boostHandler.Quantity = 2;
        boostHandler.BoostEntry = new BoostEntry[boostHandler.Quantity];
        boostHandler.BoostType = BoostType.SPEEDBOOST;
        boostHandler.Parent = parentObj.transform;

        return boostHandler;
    }
}
