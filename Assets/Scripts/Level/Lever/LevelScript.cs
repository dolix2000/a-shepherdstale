using Assets.Scripts.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{

    Lever lever = new Lever();

    [SerializeField] private LeverType leverType;
    public LeverType LeverType
    {
        get { return leverType; }
        set { leverType = value; }
    }
    public GameObject ground;

    private Animator animator;



    //public GameObject levers;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Player.LevelMaxTime = 360;
        animator = gameObject.GetComponent<Animator>();
        

    }

    void Update()
    {
        if (GameManager.Instance.GameState != GameState.PAUSE_SCREEN)
        {
            RunTimer();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LeverCollision(collision);
        animator.SetBool("LeverActive", true);

    }



    private void OnCollisionExit2D(Collision2D collision)
    {
        LeverCollision(collision);
        animator.SetBool("LeverActive", false);
    }


    public void LeverCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Box"))

        {
            if (leverType == LeverType.LEVER_SHOW)
            {
                lever.MakePlatformVisibleorInvisbile(ground);
            }
            else if (leverType == LeverType.LEVER_HIDE)
            {
                lever.SetGroundActiveorInactive(ground);

            }
            else
            {
                lever.Move(ground);

            }
        }
    }


    //todo test method --> different name better probably

    /// <summary>
    /// Checks if the Level is completed and stops the Timer, after a restart 
    /// the boolan is set to false and the timer begins at 0
    /// </summary>
    public void RunTimer()
    {
        bool levelCompleted = GameManager.Instance.Player.LevelCompleted;
        if (levelCompleted)
        {
            GameManager.Instance.Player.LevelCompleted = false;
            GameManager.Instance.Player.TimeSpent = 0;
            return; // stop the timer if level is completed
        }
        else
        {
            GameManager.Instance.Player.TimeCounter();
        }
    }
}