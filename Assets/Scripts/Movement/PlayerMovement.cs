using Assets.Scripts.ItemFactory;
using Assets.Scripts.Movement;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private MovementController movement;
    public MovementController Movement
    {
        get { return this.movement; }
    }

    [SerializeField]
    private PlayerType playerType;

    [SerializeField]
    private Transform feet;
    public Transform Feet
    {
        get { return this.feet; }
        set { this.feet = value; }
    }

    [SerializeField]
    private LayerMask layerMask;
    public LayerMask LayerMask
    {
        get { return this.layerMask; }
        set { this.layerMask = value; }
    }

    [SerializeField]
    private float speed;
    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    [SerializeField] private float jumpForce;
    public float JumpForce
    {
        get { return this.jumpForce; }
        set { this.jumpForce = value; }
    }

    [SerializeField] private float fallMultiplier;
    [SerializeField] private float lowJumpMultiplier;

    private Rigidbody2D body;
    
    [SerializeField]
    private float horizontalMovement;
    public float HorizontalMovement
    {
        get { return this.horizontalMovement; }
        set { this.horizontalMovement = value;}
    }

    private Animator animator;
    public Animator Animator
    {
        get { return this.animator; }
        set { this.animator = value; }
    }

    private GameObject box;
    
    private ChangePlayer change;


    [SerializeField]
    private UnityEvent OnLandingEvent;
   
    
    private void Awake()
    {
        if (OnLandingEvent == null)
        {
            OnLandingEvent = new UnityEvent();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = new MovementController();
        body = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        change = this.gameObject.transform.parent.GetComponent<ChangePlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.GAME_SCENE)
        {
            movement.Jump(body, feet, layerMask, jumpForce, animator);

            movement.ControlJumping(body, fallMultiplier, lowJumpMultiplier);
            movement.ChangeDirection(body, horizontalMovement);
            
        }

        if (GameManager.Instance.GameState == GameState.PAUSE_SCREEN || GameManager.Instance.GameState == GameState.WIN_SCENE)
        {
            animator.SetBool("IsJumping", false);
            animator.SetFloat("Speed", 0);     
        }

        movement.IgnoreCharacterCollision();

        animator.SetFloat("Speed", Mathf.Abs(horizontalMovement));
    }
    
    private void FixedUpdate()
    { 

        if(GameManager.Instance.GameState == GameState.GAME_SCENE )
        {
            horizontalMovement = Input.GetAxisRaw("Horizontal");
            movement.Move(body, horizontalMovement, speed);
        }

        if (GameManager.Instance.GameState == GameState.WIN_SCENE)
        {
            movement.Move(body, 0, 0);
            animator.SetFloat("Speed", Mathf.Abs(0));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Item") || other.gameObject.CompareTag("Sheep"))
        {
            if (other.gameObject.CompareTag("Sheep"))
            {
                GameManager.Instance.Player.AddLevelScore(50);
                Debug.Log(GameManager.Instance.Player.Score);
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(DestroyObject(other));
                GameManager.Instance.Player.CollectibleCount++;
                Debug.Log("Picked up <color=pink>SHEEP</color>.");

            }
            else if (other.gameObject.GetComponent<BoostBehaviour>().BoostType == BoostType.TIMEBOOST)
            {
                GameManager.Instance.Player.TimeSpent -= 15; // subtracte 15 seconds from the players time
                Debug.Log("Picked up Clock <color=yellow>TIMEBOOST</color>.");
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(DestroyObject(other));
            }
            else
            {
                GameManager.Instance.Player.AddLevelScore(15); // add score to players score
                Debug.Log(GameManager.Instance.Player.Score);
                Debug.Log("Picked up Branch <color=brown>SPEEDBOOST</color>.");
                other.GetComponent<Renderer>().enabled = false;
                other.GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(DestroyObject(other));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            box = collision.gameObject;
            if (change.CurrentPlayer == PlayerType.DOG)
            {
                box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
            else
            {
                box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void OnDisable()
    {
        movement.Move(body, 0, 0);
        animator.SetFloat("Speed", 0);
        animator.SetBool("IsJumping", false);
    }

    public  IEnumerator DestroyObject(Collider2D other)
    {
        yield return new  WaitForSeconds(3);
        Destroy(other.gameObject);
    }
}


