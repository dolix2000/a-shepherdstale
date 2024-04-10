using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Networking;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Test class for the class RequestThread
/// </summary>
public class RequestThreadTest
{
    private RequestHandler requestHandler;
    private RequestThread requestThread;
    private List<Player> players = new List<Player>();

    /// <summary>
    /// Setting up variables and initialising them before testing
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        this.requestHandler = new RequestHandler("https://ashepherdstale.herokuapp.com/testplayers");
        this.requestThread = new RequestThread();
        // send get request
        
    }

    /// <summary>
    /// Setting up variables and initialising them before testing
    /// </summary>
    [SetUp]
    public void ThreadStarts()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        requestThread.StartThread(0.5);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ReturnsRightPlayers()
    {
        string response = requestHandler.GetRequest();
        players = JsonConvert.DeserializeObject<List<Player>>(response);
        Debug.Log(response);
        Assert.AreEqual(requestThread.GetPlayers()[0].Player_Name, players[0].Player_Name); 
    }
}
