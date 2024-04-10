using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// To make sure the ObjectPoolTest is running first, we changed the name.
/// </summary>
public class AAObjectPoolTest
{
    private ObjectPoolManager manager;
    private ObjectItem objectItem;
    private GameObject pool;

    [SetUp]
    public void SetUpOPTest()
    {
        objectItem = new ObjectItem();
    }


    /// <summary>
    /// Testing if clouds get deactivated when they are added to the ObjectPool.
    /// </summary>
    [UnityTest]
    public IEnumerator CloudsNotActive()
    {
        pool = new GameObject();
        manager = pool.AddComponent<ObjectPoolManager>();
        manager.CloudParent = new GameObject();
        GameObject prefab = (GameObject)GameObject.Instantiate(Resources.Load("Prefab/Clouds/CloudBigLong"));
        objectItem.ObjectToPool = prefab;
        objectItem.AmountToPool = 1;
        manager.ItemsToPool = new List<ObjectItem>();
        manager.PooledObjects = new List<GameObject>();

        manager.ItemsToPool.Add(objectItem);
        yield return new WaitForSeconds(2);
        Assert.IsFalse(manager.PooledObjects[0].activeSelf);
    }
}