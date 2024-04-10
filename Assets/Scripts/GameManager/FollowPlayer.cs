using Assets.Scripts.Movement;
using UnityEngine;

/// <summary>
/// The FollowPlayer script will enable the Camera to follow the player. 
/// The position of the Camera is locked onto the position of the player. 
/// This script is bound to the Main Camera.
/// </summary>
public class FollowPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    private Transform playerMovement;
    public float offset;
    private ChangePlayer changePlayer;


    // Start is called before the first frame update
    void Start()
    {
        changePlayer = player.GetComponent<ChangePlayer>(); 
    }

    // Update is called once per frame.
    void Update()
    {
        SetPlayer();
    }

    // LateUpdate is called after all other Update Methods have been called.
    void LateUpdate()
    {
        PlayerPosition();
    }

    public void SetPlayer() // Checking which player is currently used.
    {
        if(changePlayer.CurrentPlayer == PlayerType.SHEPHERD)
        {
            playerMovement = player.transform.GetChild(0); // Follows Shepherd
        } 
        else
        {
            playerMovement = player.transform.GetChild(1); // Follows Dog
        }
    }

    private void PlayerPosition()
    {
        Vector3 position = transform.position; // Stores current position of camera
        position.x = playerMovement.position.x; // Camera position is set to be equal to players position on the x axis
        position.y = playerMovement.position.y; // Camera position is set to be equal to players position on the y axis

        // Change camera position if a non-center view is desired.
        position.x += offset;
        position.y += offset;

        transform.position = position; // Cameras temporary position is set to cameras current position 
    }
}
