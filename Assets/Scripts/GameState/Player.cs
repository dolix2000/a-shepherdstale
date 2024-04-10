using UnityEngine;
using System;

/// <summary>
/// Class of player which holds basic setters and getters
/// Implements the Serializable Attribute so the object of the player can be saved
/// </summary>

[Serializable] // needs to be serializable for JSONUtlity
public class Player 
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string player_name;
    [SerializeField]
    private int score; // score that gets send to the database later
    [SerializeField]
    private string play_time;
    [SerializeField]
    private string created_at;

    // variables for counting the playtime
    private int nextUpdate = 1;
    private int timeSpent;
    private int levelMaxTime;

    // private score which gets calculated later
    private int level_score;
    private int collectible_count;

    // checks if player completed the level
    private bool levelCompleted;
    private bool isNewGame;

    // variables for creating (post request) or updating an playre (put request)
    private PlayerPostData playerPostData;
    private PlayerPutData playerPutData;

    // default constructor
    public Player() 
    {
        this.level_score = 20;
    }

    public Player(string name, int score, string time)
    {
        this.player_name = name;
        this.score = score;
        this.play_time = time;
    }

    // Basic seters and getters of the player
    public int Id
    {
        get { return this.id; }
        set { id = value; }
    }

    public string Player_Name
    {
        get { return this.player_name; }
        set { this.player_name = value; }
    }

    public int Score
    {
        get { return this.score; }
        set { this.score = value; }
    }

    public int Level_Score
    {
        get { return this.level_score; }
        set { this.level_score = value; }
    }

    public int TimeSpent
    {
        get { return this.timeSpent; }
        set { this.timeSpent = value; }
    }

    public int LevelMaxTime
    {
        get { return this.levelMaxTime; }
        set { this.levelMaxTime = value; }
    }

    public string Play_Time
    {
        get { return this.play_time; }
        set { this.play_time = value; }
    }

    public string Date
    {
        get { return this.created_at; }
        set { this.created_at = value; }
    }

    public int CollectibleCount
    {
        get { return this.collectible_count; }
        set { this.collectible_count = value; }
    }

    public bool LevelCompleted
    {
        get { return this.levelCompleted; }
        set { this.levelCompleted = value; }
    }

    public bool IsNewGame
    {
        get { return this.isNewGame; }
        set { this.isNewGame = value; }
    }

    /// <summary>
    /// Method for adding the score of the player.
    /// In this case the score the player collected in the current level
    /// </summary>
    /// <param name="score"></param>
    public void AddLevelScore(int score)
    {
        this.score += score;
    }

    /// <summary>
    /// Method for counting the play time of the player
    /// </summary>
    public void TimeCounter()
    {
        if (Time.time >= nextUpdate)
        {
            nextUpdate = Mathf.FloorToInt(Time.time) + 1;
            this.timeSpent++;
            //Debug.Log("Time: " + timeSpent);
        }  
    }

    /// <summary>
    /// Method to calculate the players score (this score gets send as a post or put request)
    /// </summary>
    public void CalculateEndScore()
    {
        this.score += Math.Max(0, this.levelMaxTime - timeSpent) * level_score;
    }

    /// <summary>
    /// Method to reset the highscore of the player when the level is done
    /// </summary>
    public void ResetPlayerStats()
    {
        this.level_score = 0;
        this.score = 0;
        this.timeSpent = 0;
        this.play_time = "";
        this.created_at = "";
        this.collectible_count = 0;
    }

    /// <summary>
    /// convert the time spent into the format 0:00 | 0:00, to show ingame, as well as insert into the database
    /// </summary>
    /// <returns>the formated timetext</returns>
    public string GetConvertedTimeSpent()
    {
        TimeSpan time = TimeSpan.FromSeconds(this.timeSpent);
        string timeText = string.Format("{0:00}:{1:00}", time.Minutes, time.Seconds);
        return timeText;
    }

    public PlayerPostData PlayerPostData
    {
        get { return this.playerPostData; }
        set { this.playerPostData = value; }
    }

    public PlayerPutData PlayerPutData
    {
        get { return this.playerPutData; }
        set { this.playerPutData = value; }
    }
}

/// <summary>
/// With the class JsonUtility we can generate a JSON representation of the public fields of an object (or private fields with the SerializeField tag).
/// Since the HTTP requests variables need to correspond to the query paramaters from our endpoints, there are two classes "PlayerPostData" and
/// "PlayerPutData" created, which get parsed into a JSON representation and send to the server, with the specific parameters that are needed.
/// </summary>
[Serializable] // needs to be serializable for JSONUtlity
public class PlayerPostData  // class for sending post requests
{
    public PlayerPostData(Player player)
    {
        this.player_name = player.Player_Name;
        this.score = player.Score;
        this.play_time = player.Play_Time;
    }

    [SerializeField]
    private string player_name;
    public string Player_Name
    {
        get { return this.player_name; }
    }

    [SerializeField]
    private int score;
    public int Score
    {
        get { return this.score; }
    }

    [SerializeField]
    private string play_time;
    public string Play_Time
    {
        get { return this.play_time; }
    }
}

/// <summary>
/// With the class JsonUtility we can generate a JSON representation of the public fields of an object (or private fields with the SerializeField tag).
/// Since the HTTP requests variables need to correspond to the query paramaters from our endpoints, there are two classes "PlayerPostData" and
/// "PlayerPutData" created, which get parsed into a JSON representation and send to the server, with the specific parameters that are needed.
/// </summary>
[Serializable] // needs to be serializable for JSONUtlity
public class PlayerPutData // class for sending put requests
{
    public PlayerPutData(Player player)
    {
        this.score = player.Score;
        this.play_time = player.Play_Time;
    }

    [SerializeField]
    private int score;
    public int Score
    {
        get { return this.score; }
    }

    [SerializeField]
    private string play_time;
    public string Play_Time
    {
        get { return this.play_time; }
    }
}
