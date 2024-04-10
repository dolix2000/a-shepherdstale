using Assets.Scripts.Database;
using Assets.Scripts.Networking;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScript : MonoBehaviour
{
    RequestHandler requestHandler;
    private void Awake()
    {
        requestHandler = new RequestHandler("https://ashepherdstale.herokuapp.com/players");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Winning();
        }
    }
    /// <summary>
    /// Change the game state, set level complete true and 
    /// invokes the method InsertPlayer after the a collision
    /// </summary>
    public void Winning()
    {
        InsertPlayer();
        GameManager.Instance.Player.LevelCompleted = true; // set level completed to true
        GameManager.Instance.SetGameState(GameState.WIN_SCENE);
        Debug.Log("<color=green>Gamestate set to: </color>" + GameManager.Instance.GameState);
        Debug.Log("You won!");
    }

    /// <summary>
    /// Invokes every Method from the Player class which is used to calculate the Highscore and 
    /// checks if the player has already a Player Id. If the Player has already an Id it sends 
    /// a put Request to the Server which updated the old score, but if there is no Id a 
    /// new Entry in the database is made.
    /// </summary>
    private void InsertPlayer()
    {
        int id = GameManager.Instance.Player.Id;
        // should be checked after a level is done
        GameManager.Instance.Player.CalculateEndScore();
        GameManager.Instance.Player.Play_Time = GameManager.Instance.Player.GetConvertedTimeSpent();
        GameManager.Instance.Player.PlayerPostData = new PlayerPostData(GameManager.Instance.Player);
        GameManager.Instance.Player.PlayerPutData = new PlayerPutData(GameManager.Instance.Player);

        // should be checked after a level is done
        if (id == 0)
        {
            GameManager.Instance.Player.Id = requestHandler.GetHighscoreId(requestHandler.PostRequest(GameManager.Instance.Player.PlayerPostData));
            Debug.Log("Player has the id: " + GameManager.Instance.Player.Id);
            
        }
        else
        {
            string responseString = requestHandler.GetRequestId(id);
            responseString = responseString.Substring(1, responseString.Length - 2);
            Player player = JsonConvert.DeserializeObject<Player>(responseString);
            if (player.Score > GameManager.Instance.Player.Score)
            {
                requestHandler.PutRequest(GameManager.Instance.Player.PlayerPutData, GameManager.Instance.Player.Id); ;
            }
            
        }
        SaveUtility.SavePlayer(GameManager.Instance.Player);
        GameManager.Instance.Player.ResetPlayerStats(); // reset for next level or restart level
    }
}