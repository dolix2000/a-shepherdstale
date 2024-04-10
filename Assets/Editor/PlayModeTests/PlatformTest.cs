using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlatformTest
{

    private GameObject platform;
    private Lever lever;
    private Platform movingPlatformScript;
    private Animator animator;


    [SetUp]
    public void SetUp()
    {
        platform = new GameObject("platform");
        movingPlatformScript = platform.AddComponent<Platform>();
        lever = new Lever();

        movingPlatformScript.Pos1 = new GameObject().transform;
        movingPlatformScript.Pos2 = new GameObject().transform;
        movingPlatformScript.StartPos = new GameObject().transform;

        movingPlatformScript.StartPos.position = new Vector3(0, 0, 0);
        movingPlatformScript.Pos1.position = new Vector3(0, 0, 0);
        movingPlatformScript.Pos2.position = new Vector3(0, 5, 0);
        movingPlatformScript.Speed = 5f;

    }
    // A Test behaves as an ordinary method
    [Test]
    public void PlatformTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.



    [UnityTest]
    public IEnumerator PlatformIsNotActive()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        lever.Move(platform);
        yield return null;

        Assert.IsFalse(platform.activeSelf);
    }



    [UnityTest]
    public IEnumerator PlatformisActive()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        lever.Move(platform);
        lever.Move(platform);

        yield return null;

        Assert.IsTrue(platform.activeSelf);
    }
}
