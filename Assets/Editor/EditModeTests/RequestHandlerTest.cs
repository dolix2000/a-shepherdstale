using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Assets.Scripts.Networking;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Test associated with class RequestHandler for testing different requests
/// </summary>
public class RequestHandlerTest
{
    private Player player;
    private PlayerPostData playerPostData;
    private PlayerPutData playerPutData;
    private RequestHandler requestHandler;
    private List<Player> players = new List<Player>();

    /// <summary>
    /// Setting up variables and initialising them before testing
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        this.player = new Player("Herbert", 32, "3:12");
        this.playerPostData = new PlayerPostData(this.player);
        this.playerPutData = new PlayerPutData(this.player);
        this.requestHandler = new RequestHandler("https://ashepherdstale.herokuapp.com/testplayers");
    }

    [Test]
    public void GetRequestReturnsPlayers()
    {
        string response = requestHandler.GetRequest();
        players = JsonConvert.DeserializeObject<List<Player>>(response);
        Debug.Log(response);
        Assert.IsNotEmpty(players);
    }

    [Test]
    public void GetPlayerById_ReturnsName()
    {
        Player player;
        string response = requestHandler.GetRequestId(1);
        response = response.Substring(1, response.Length - 2);
        player = JsonConvert.DeserializeObject<Player>(response);
        Debug.Log(response);
        Assert.IsNotEmpty(player.Player_Name);
    }

    [Test]
    public void PostPlayerReturnsId()
    {
        string response = requestHandler.PostRequest(playerPostData);
        response = response.Substring(1, response.Length - 2);
        Player player;
        player = JsonConvert.DeserializeObject<Player>(response);
        Assert.IsNotEmpty(player.Id.ToString());
    }

    [Test]
    public void PutPlayerReturnsId()
    {
        string response = requestHandler.PutRequest(playerPutData, 1);
        response = response.Substring(1, response.Length - 2);
        Player player;
        player = JsonConvert.DeserializeObject<Player>(response);
        Assert.IsNotEmpty(player.Id.ToString());
    }
}
