using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System;

namespace Assets.Scripts.Database
{
    public class DBController 
    {

        private Player[] players;

        public Player[] Players
        {
            get { return this.players; }
        }

        private string connectionString;
        public DBController()
        {
            connectionString = "URI=file:" + Application.dataPath + "/HighscoreDB.sqlite";
 
            CreateDB();
           
            players = new Player[GetRowCount()];
            GetScores();
          
        }

        /// <summary>
        /// Method for Creating a local database
        /// </summary>
        public void CreateDB()
        {
            //create the Connection to the database
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                //To read the Data we need to Open teh Connection
                dbConnection.Open();

                //set up object called "command" to allow acess to the database
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = "CREATE TABLE IF NOT EXISTS HighscoreDB (" +
                                             "playerID  INTEGER PRIMARY KEY AUTOINCREMENT ," +
                                             "name  TEXT NOT NULL," +
                                            "score INTEGER, " +
                                              "time REAL NOT NULL DEFAULT 0,"+
                                             "date  DATETIME NOT NULL DEFAULT CURRENT_DATE)";

                    //Execute a Query that does not return any data it returns teh number of rows affected
                    dbCmd.ExecuteNonQuery();
                }
                   
                dbConnection.Close();
            }
        }

      
        /// <summary>
        /// Method for avoid a new Database entry for the same Player
        /// </summary>
        /// <param name="player"> Is the current player </param>
        public void SetPlayerID(Player player)
        {  
            if (GameManager.Instance.Player.Id == 0) 
            { 
                GameManager.Instance.Player.Id = GetId();
            }
        }


        /// <summary>
        /// Method for insert a new Player Entry
        /// </summary>
        /// <param name="player">Is the current player</param>
        public void InsertScore(Player player)
        {
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                  
                    dbCmd.CommandText = string.Format("INSERT INTO HighscoreDB ( name, score,time, date) VALUES(\"{0}\", \"{1}\", \"{2}\", CURRENT_DATE )", player.Player_Name, player.Score , player.Play_Time);
                    
                    dbCmd.ExecuteNonQuery();
                    dbConnection.Close();


                }
            }
        }


        /// <summary>
        /// Method for update a Database Entry for an already existing Player 
        /// </summary>
        /// <param name="player">Is the current player</param>
        public void UpdateScore(Player player) 
        {
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {

                    dbCmd.CommandText = string.Format("UPDATE HighscoreDB SET score = " + player.Score +  ", time = " + player.Play_Time + ",date = CURRENT_DATE WHERE playerID = " + player.Id);

                    dbCmd.ExecuteNonQuery();
                    dbConnection.Close();


                }
            }
        }

       
        /// <summary>
        /// Method for shwoing the whole Database Entrys
        /// </summary>
        public void GetScores()
        {
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {
                dbConnection.Open();

                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = "SELECT * FROM HighscoreDB ORDER BY score DESC";
                    dbCmd.ExecuteNonQuery();

                    using (IDataReader reader = dbCmd.ExecuteReader())
                    {
                        

                        int i = 0;
                        while (reader.Read())
                        {
                            Debug.Log("PlayerID: " + reader.GetInt32(0) + "- Name:" + reader.GetString(1) + "- Score : " + reader.GetInt32(2)  + "Time: " + reader.GetFloat(3) + "- Date :" + reader.GetDateTime(4));
                            players[i] = new Player();
                            players[i].Player_Name = reader.GetString(1);
                            players[i].Score = reader.GetInt32(2);
                            //players[i].Time = reader.GetFloat(3); 
                            players[i].Date = reader.GetDateTime(4).ToString();
                            i++;
                        }
                        dbConnection.Close();
                        reader.Close();
                    }

                }
            }
        }

 
        /// <summary>
        /// Method for getting the ID
        /// </summary>
        /// <returns> Returns the highest ID-Entry in the database </returns>
        public int GetId()
        {
            int maxId;
            //create the Connection to the database
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {

                dbConnection.Open();

                //set up object called "command" to allow acess to the database
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = "SELECT MAX(playerID) FROM HighscoreDB";

                    //Executes the query, and returns the first column of the first row in the result set returned by the query
                    maxId = Convert.ToInt32(dbCmd.ExecuteScalar());

                }
                dbConnection.Close();
            }
            //if(maxId > 1)
            //{
            //    //  GameManager.Instance.Player.Id = maxId + 1;
            //    maxId++;
            //    Debug.Log("Id of player: " + maxId);
            //}
            return maxId;
        }


        
        /// <summary>
        /// Method for getting the number of Entrys
        /// </summary>
        /// <returns>Return the number of Rows for the Highscore View </returns>
        public int GetRowCount()
        {
            int rowCount;
            //create the Connection to the database
            using (IDbConnection dbConnection = new SqliteConnection(connectionString))
            {

                dbConnection.Open();

                //set up object called "dbCmd" to allow acess to the database
                using (IDbCommand dbCmd = dbConnection.CreateCommand())
                {
                    dbCmd.CommandText = "SELECT COUNT(*) FROM HighscoreDB";
                    rowCount = Convert.ToInt32(dbCmd.ExecuteScalar());

                }
                dbConnection.Close();
            }
            return rowCount;
        }
    }


}