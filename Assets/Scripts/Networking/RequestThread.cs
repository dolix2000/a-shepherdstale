using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Networking
{
    /// <summary>
    /// This script handles the response in order to display it in the HighscoreView later.
    /// By starting a thread that starts a request every x minutes and saving it to a file, so 
    /// only necessary get requests get sent (every x minutes)
    /// </summary>
    public class RequestThread
    {
        private RequestHandler requestHandler;
        private List<Player> players;
        private string data;
        private Timer timer;

        private string path = Application.persistentDataPath + "/SaveData/";

        public RequestThread()
        {
            requestHandler = new RequestHandler("https://ashepherdstale.herokuapp.com/players");
            players = new List<Player>();
        }

        /// <summary>
        /// Method for sending a get request and saving (also parsing them into players)
        /// </summary>
        public void RequestCall()
        { 
            data = requestHandler.GetRequest();
            if (!Directory.Exists(path))
            {
                // create path if it doesn't exist yet
                Directory.CreateDirectory(path); 
            }
            File.WriteAllText(path + "highscore.txt", data);
            Debug.Log("Saved Highscore...");
            Debug.Log(data);
        }

        /// <summary>
        /// Method for starting a thread.
        /// This method calls RequestCall() after every x minutes for our server purposes (Heroku)
        /// Also saves every x minutes (RequestCall()) the highscore list, so no Get Requests need to be sent all the time to server
        /// if the player accesses the highscore board
        /// </summary>
        /// <param name="duration">Timeinterval of the timer to call this method again</param>
        public void StartThread(double duration)
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromMinutes(duration);

            // start method after 0 seconds and repeat after duration 
            timer = new System.Threading.Timer((e) =>
            {
                RequestCall();
                Debug.Log("Sending request... ");

            }, null, startTimeSpan, periodTimeSpan);
            timer.Dispose(); // dispose the timer
        }

        /// <summary>
        /// Method for returning the top ten players, by reading a saved highscore file.
        /// </summary>
        /// <returns>Top ten players</returns>
        public List<Player> GetPlayers()
        {
            if (File.Exists(path + "highscore.txt"))
            {           
                string playersJson = File.ReadAllText(path + "highscore.txt");
                // parsing String into player list
                players = JsonConvert.DeserializeObject<List<Player>>(playersJson);
                Debug.Log("Loading Highscore...");
                return players;
            } 
            else
            {
                Debug.Log("Highscore file doesn't exist.");
                return null;
            }
        }
    }
}