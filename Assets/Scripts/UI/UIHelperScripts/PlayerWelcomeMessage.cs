using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Helper script for greeting the player in main menu.
/// Changes from universal welcome to a personalized one after setting up a playername.
/// Added "welcome back" after the player has an ID, eg. completed a level.
/// </summary>
public class PlayerWelcomeMessage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerName;
    [SerializeField]
    private TextMeshProUGUI welcomeMessage;

    private void OnEnable()
    {
        InvokeRepeating("SetWelcomeText", 0f, 2f);
    }

    private void SetWelcomeText()
    {
        if (GameManager.Instance.Player.Id > 0)
        {
            welcomeMessage.text = "Welcome back,";
            playerName.text = GameManager.Instance.Player.Player_Name;
        }
        else if (GameManager.Instance.Player.Id == 0)
        {
            welcomeMessage.text = "Welcome,";

            if (GameManager.Instance.Player.Player_Name != null)
            {
                playerName.text = GameManager.Instance.Player.Player_Name;
            }
            else
            {
                playerName.text = "New Shepherd!";
            }
        }
    }
}
