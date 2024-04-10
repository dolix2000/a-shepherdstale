using Assets.Scripts.Movement;
using UnityEngine;

public class ChangePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject shepherd;
    public GameObject Shepherd
    {
        get { return this.shepherd; }
        set { this.shepherd = value; }
    }

    [SerializeField]
    private GameObject dog;
    public GameObject Dog
    {
        get { return this.dog; }
        set { this.dog = value; }
    }

    private PlayerType currentPlayer;

    public PlayerType CurrentPlayer
    {
        get { return this.currentPlayer; }
        set { this.currentPlayer = value; }
    }

    public void Awake()
    {
        gameObject.name = "Player";
    }

    // Start is called before the first frame update
    void Start()
    {
        DisableDog();
        currentPlayer = PlayerType.SHEPHERD;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchPlayer();
        }
    }

    public void DisableDog()
    {
        dog.GetComponent<PlayerMovement>().enabled = false;
    }

    public PlayerType SwitchPlayer()
    {
        PlayerMovement shepherdPlayer = shepherd.GetComponent<PlayerMovement>();
        PlayerMovement dogPlayer = dog.GetComponent<PlayerMovement>();
                
        if (currentPlayer == PlayerType.SHEPHERD)
        {
            Debug.Log("Player is Dog now");
            currentPlayer = PlayerType.DOG;
            shepherdPlayer.enabled = false;
            dogPlayer.enabled = true;
            StartCoroutine(GameManager.Instance.PlaySound(nameof(SoundFiles.SFX_DogBark)));
        } else
        {
            Debug.Log("Player is Shepherd now");
            currentPlayer = PlayerType.SHEPHERD;
            shepherdPlayer.enabled = true;
            dogPlayer.enabled = false;
            StartCoroutine(GameManager.Instance.PlaySound(nameof(SoundFiles.SFX_Shepherd)));
        } 
        return currentPlayer;
    }
}

