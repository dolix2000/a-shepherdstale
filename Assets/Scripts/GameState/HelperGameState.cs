using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperGameState : MonoBehaviour
{
    
    void Awake()
    {
        GameManager.Instance.SetGameState(GameState.GAME_SCENE);
    }
}