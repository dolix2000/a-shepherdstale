using UnityEngine;

/// <summary>
/// Title screen animation logic for moving sheep and dog.
/// </summary>
public class TitleScreenAnimations : MonoBehaviour
{
    public Transform wayPoint0, wayPoint1; // Start + Endpoint for object to walk in between
    private Vector3 nextWaypoint;
    public float speed;
    public bool leftFaced; // changes the images' direction according to the object's facing direction.
    private Rigidbody2D body;
    private float waitTime; // artificial timer to have the objects wait some time beforce walking again.
    private Animator animator;

    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        if (leftFaced)
        {
            nextWaypoint = wayPoint0.position;
        }
        else
        {
            nextWaypoint = wayPoint1.position;
        } 

    }

    private void Update()
    {      
        waitTime -= Time.deltaTime;
        if (waitTime < 0)
        {
            MoveCharacter();
        }
    }

    private void MoveCharacter()
    {
        if(gameObject.CompareTag("Player"))
        {
            animator.SetFloat("Speed", 1);
        }

        if (transform.position == wayPoint0.position)
        {
            nextWaypoint = wayPoint1.position;
            SwitchFace();
        }
        if (transform.position == wayPoint1.position)
        {
            nextWaypoint = wayPoint0.position;
            SwitchFace();
        }

        transform.position = Vector3.MoveTowards(transform.position, nextWaypoint, speed * Time.deltaTime);
        
        if(transform.position == nextWaypoint)
        {
            waitTime = 5f;

            if (gameObject.CompareTag("Player"))
            {
                animator.SetFloat("Speed", 0);
            };
        }
    }

    private void SwitchFace()
    {
        Vector3 scale = body.transform.localScale;
      
        scale.x *= -1;
        
        body.transform.localScale = scale;
        leftFaced = !leftFaced;
    }
}
