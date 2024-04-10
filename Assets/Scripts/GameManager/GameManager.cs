using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Networking;
using UnityEngine;

public enum GameState // Checking different GameStates.
    {
        INTRO,
        MAIN_MENU,
        PAUSE_SCREEN,
        GAME_SCENE,
        WIN_SCENE,
        HIGHSCORE_DISPLAY,
        CREDITS
}

/// <summary>
/// Managing ingame music and scene changes. Uses DontDestroyOnLoad to make sure certain instances persist through gameplay.
/// </summary>
[Serializable]
public class GameManager : MonoBehaviour 
{
    protected GameManager() { }
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (GameManager.instance == null)
            {
                GameObject manager = GameObject.FindGameObjectWithTag("GameManager");
                GameManager.instance = manager.GetComponent<GameManager>();
                DontDestroyOnLoad(manager); // DontDestroyOnLoad is called so that the instance does not get destroyed between scenes.              
            }
            return GameManager.instance;
        }

        set  { instance = value; }
    }// Getter for GameManager. Returns instance.

    public GameState GameState { get; private set; } // Getter for current GameState.

    private Player player; 
    public Player Player
    {
        get
        {
            return this.player;
        }
        set
        {
            if (SaveUtility.LoadPlayer() != null)
            {
                Debug.Log("No new Player");
                player = SaveUtility.LoadPlayer();
            }
            else
            {
                Debug.Log("New Player");
                player = new Player();
            }
        }
    }// Getter for current Player.

    private LoadScene levelLoadController;
    public LoadScene LevelLoadController
    {
        get { return this.levelLoadController; }

        private set { this.levelLoadController = value; }
    }// Getter for current LevelLoadController.

    private RequestThread requestThread;

    public RequestThread RequestThread
    {
        get { return this.requestThread; }
    }


    private void Awake()
    {
        Player = SaveUtility.LoadPlayer();
        this.requestThread = new RequestThread();
        Debug.Log("<color=green>GameManager:</color> initialized");

        QualitySettings.vSyncCount = 0;  
        Application.targetFrameRate = 60;

    }

    private void Start()
    {
        this.levelLoadController = GameManager.Instance.gameObject.transform.GetChild(0).gameObject.GetComponent<LoadScene>();
        this.requestThread.StartThread(25d);
    }

    /// <summary>
    /// Sets GameState of the game in triggering an event to change the scene.
    /// </summary>
    /// <param name="state"></param>
    public void SetGameState(GameState state) // Used to change GameState by passing enum variables.
    {
        this.GameState = state;
    }

    // Audio references

    /// <summary>
    /// Passing "PlaySound" order to Audiomanager.
    /// </summary>
    /// <param name="soundfile">Name of file to play</param>
    public IEnumerator PlaySound(string soundfile)
    {
        AudioManager.Instance.PlaySound(soundfile.ToString());
        yield return new WaitForSeconds(0);
        Debug.Log("Now playing " + soundfile);
    }

    /// <summary>
    /// Passing "StopSound" order to Audiomanager.
    /// </summary>
    /// <param name="soundfile">Name of file to play</param>
    /// 
    public IEnumerator StopTitleMusic(string soundfile) 
    {
        AudioManager.Instance.StopSound(soundfile.ToString());
        yield return new WaitForSeconds(0);
        Debug.Log("Stopped playing " + soundfile);
    }

    // End Game:
    public void OnApplicationQuit() // If Application is closed the instance is set to null.
    {
        GameManager.instance = null;
    }
}