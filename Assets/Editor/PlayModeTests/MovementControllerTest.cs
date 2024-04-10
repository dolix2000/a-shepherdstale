using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;


public class MovementControllerTest
{
    private PlayerMovement movement;
    private GameObject player;
    private GameObject shepherd;
    private RootController rootController;
    private Rigidbody2D body;

    private Animator animator;
    private LayerMask layerMask;
    private Transform feet;

    [SetUp]
    public void SetUp()
    {
        SceneManager.LoadScene("MainScene");
        SceneManager.LoadSceneAsync("LevelOne", LoadSceneMode.Additive);
    }

    [UnityTest]
    public IEnumerator MovementControllerMove()
    {
        rootController = GameObject.Find("UI_RootController").GetComponent<RootController>();
        rootController.ChangeController(RootController.ControllerTypeEnum.Playing);

        player = GameObject.Find("Player");
        shepherd = player.transform.Find("Shepherd").gameObject;
        
        movement = GameObject.Find("Player").GetComponentInChildren<PlayerMovement>();
        body = shepherd.GetComponent<Rigidbody2D>();

        var originPos = shepherd.transform.position;

        movement.Movement.Move(body, 1, 10);

        yield return new WaitForSeconds(3f);

        var newPos = shepherd.transform.position;
        Assert.AreNotEqual(originPos, newPos);
    }

    [UnityTest]
    public IEnumerator MovmentControllerIsGrounded()
    {
        rootController = GameObject.Find("UI_RootController").GetComponent<RootController>();
        rootController.ChangeController(RootController.ControllerTypeEnum.Playing);

        movement = GameObject.Find("Player").GetComponentInChildren<PlayerMovement>();

        feet = movement.Feet;
        layerMask = movement.LayerMask;
        animator = movement.Animator;
        
        yield return new WaitForSeconds(3f);

        Assert.True(movement.Movement.IsGrounded(feet, layerMask, animator));
    }
}

