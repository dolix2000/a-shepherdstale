using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PrefabInstatiateTest
{
    private GameObject parent;
    private PrefabInstatiate prefabInstatiate;
    private Vector3 vector;

    [SetUp]
    public void SetUp()
    {

        parent = new GameObject("parent");
        prefabInstatiate = new PrefabInstatiate();
        vector = new Vector3(0, 0, 0);

    }

    // A Test behaves as an ordinary method
    [Test]
    public void PrefabInstatiateSimplePasses()
    {
        // Use the Assert class to test conditions
    }




    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PrefabCheckRightPosition()
    {
        GameObject prefab = prefabInstatiate.InstantiatePrefab(parent, vector, "Prefab/Sheep/Sheep");


        // Use the Assert class to test conditions.
        // Use yield to skip a frame.

        yield return new WaitForSeconds(3f);

        Assert.AreEqual(prefab.transform.position, vector);


    }

    [UnityTest]
    public IEnumerator PrefabCheckRightParent()
    {
        GameObject prefab = prefabInstatiate.InstantiatePrefab(parent, vector, "Prefab/Sheep/Sheep");


        yield return new WaitForSeconds(3f);

        Assert.AreEqual(prefab.transform.parent.name, parent.name);
    }
}
