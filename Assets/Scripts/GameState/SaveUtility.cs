using System.IO;
using System;
using UnityEngine;

/// <summary>
/// This class is responsible for serializing the data of the player as a JSON File.
/// </summary>
[Serializable]
public class SaveUtility
{
    /// <summary>
    /// Method to save an object of type player as a json file
    /// </summary>
    /// <param name="player">, that gets saved</param>
    public static void SavePlayer(Player player)
    {
        string path = Application.persistentDataPath + "/SaveData/";
        string filename = "save.txt";

        // create path if it doesn't exist yet
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path); 
        }
        string json = JsonUtility.ToJson(player);
        File.WriteAllText(path + filename, json); // write to json file 
        Debug.Log("Saving game...");
    }

    /// <summary>
    /// This method allows us to load a player. Loading the JSON File back as a player. 
    /// With an if-statement we check if a save file already exists. If a file exists, the player/gamestate will load.
    /// </summary>
    /// <returns>Player</returns>
    public static Player LoadPlayer()
    {
        string path = Application.persistentDataPath + "/SaveData/";
        string filename = "save.txt";
        string fullPath = path + filename;

        // check if file exists, then save player/game
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            Player player = JsonUtility.FromJson<Player>(json); // parse json save file in playerdata
            Debug.Log("Loading game...");
            return player;
        }
        else
        {
            // else return null, and file doesn't exists
            Debug.LogError("Save file doesn't exist.");
            return null;
        } 
    }
}