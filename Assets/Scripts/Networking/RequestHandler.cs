using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System;
using System.IO;
using System.Text;

namespace Assets.Scripts.Networking
{
    /// <summary>
    /// This class sends HTTP Requests to a Database Server with GET, POST, PUT and DELETE Methods and handles the responses.
    /// </summary>
    public class RequestHandler
    {
        private string apiURL;

        public RequestHandler(string apiURL)
        {
            this.apiURL = apiURL;
        }

        /// <summary>
        /// Get Method for getting all the players as a json string
        /// </summary>
        /// <returns>response</returns>
        public string GetRequest()
        {
            // creating get request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL);
            request.Method = "GET";
            // setting headers
            request.Headers.Add("Authorization", "Basic bGltb2xlbGlsdTpqdWRhZG9nZWts");
            request.Timeout = 10000;

            // get the response
            string responseString;
            // getting the response from the server and reading response through a stream and saving it to a string
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                responseString = reader.ReadToEnd();
            }
            Debug.Log(DateTime.Now.ToString("dd.MM.yy") + "/ <color=green>GET Request</color>");
            return responseString;
        }

        /// <summary>
        /// Get Method for getting a specific player by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Player</returns>
        public string GetRequestId(int id)
        {
            // creating get request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + "/" + id);
            request.Method = "GET";
            // setting headers
            // auth header for authenticating
            request.Headers.Add("Authorization", "Basic bGltb2xlbGlsdTpqdWRhZG9nZWts");
            // wait 10 sec for response, if not responding after 10 sec --> requests stop
            request.Timeout = 10000;

            // save response in a string
            string responseString;
            // getting the response from the server and reading response through a stream and saving it to a string
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                responseString = reader.ReadToEnd();
            }
            Debug.Log(DateTime.Now.ToString("dd.MM.yy") + "/ <color=green>GET Request</color>");
            return responseString;
        }

        /// <summary>
        /// Method for posting a player --> sending data to the database and save it
        /// </summary>
        /// <param name="player"></param>
        /// <returns>response: id of posted player</returns>
        public string PostRequest(PlayerPostData player)
        {
            string jsonData = JsonUtility.ToJson(player);
            var data = Encoding.ASCII.GetBytes(jsonData);
            Debug.Log(jsonData);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + "/create");
            request.Method = "POST";

            // setting headers
            // auth header for authenticating
            request.Headers.Add("Authorization", "Basic bGltb2xlbGlsdTpqdWRhZG9nZWts");
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = data.Length;
            // wait 10 sec for response, if not responding after 10 sec --> requests stop
            request.Timeout = 10000;

            // writing data (sending post request)
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            string responseString; // save response in a string
            // getting the response from the server and reading response through a stream and saving it to a string
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                responseString = reader.ReadToEnd();
            }
            Debug.Log(DateTime.Now.ToString("dd.MM.yy") + "/ <color=yellow>POST Request</color>");
            return responseString;
        }

        /// <summary>
        /// Put Method to update player by his id in the database
        /// </summary>
        /// <param name="player"></param>
        /// <param name="id"></param>
        /// <returns>response: Id of updated player</returns>
        public string PutRequest(PlayerPutData player, int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + "/" + id);
            request.Method = "PUT";

            string jsonData = JsonUtility.ToJson(player);
            Debug.Log(jsonData);

            var data = Encoding.ASCII.GetBytes(jsonData);

            // setting headers
            // auth header for authenticating
            request.Headers.Add("Authorization", "Basic bGltb2xlbGlsdTpqdWRhZG9nZWts");
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";
            request.ContentLength = data.Length;
            // wait 10 sec for response, if not responding after 10 sec --> requests stop
            request.Timeout = 10000;

            // writing data (sending post request)
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            // save response in a string
            string responseString; 
            // getting the response from the server and reading response through a stream and saving it to a string
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                responseString = reader.ReadToEnd();
            }
            Debug.Log(DateTime.Now.ToString("dd.MM.yy") + "/ <color=blue>PUT Request</color>");
            return responseString;
        }

        /// <summary>
        /// Delete Method to delete a player by his id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>respone: body of the player that got deleted</returns>
        public string DeleteRequest(int id)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(apiURL + "/" + id);
            request.Method = "DELETE";

            // setting headers
            // auth header for authenticating
            request.Headers.Add("Authorization", "Basic bGltb2xlbGlsdTpqdWRhZG9nZWts");
            // wait 10 sec for response, if not responding after 10 sec --> requests stop
            request.Timeout = 10000;

            // save response in a string
            string responseString;
            // getting the response from the server and reading response through a stream and saving it to a string
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using var stream = response.GetResponseStream();
                using var reader = new StreamReader(stream);
                responseString = reader.ReadToEnd();
            }
            Debug.Log(DateTime.Now.ToString("dd.MM.yy") + "/ <color=red>DELETE Request</color>");

            return responseString;
        }

        /// <summary>
        /// Method for providing the player with his id
        /// </summary>
        /// <param name="responseString"></param>
        /// <returns>id of the player</returns>
        public int GetHighscoreId(string responseString)
        {
            responseString = responseString.Substring(1, responseString.Length - 2);
            Player currentPlayer = JsonConvert.DeserializeObject<Player>(responseString);
            Debug.Log("Assigned player id <color=cyan>" + currentPlayer.Id + "</color>");
            return currentPlayer.Id;
        }
    }
}
