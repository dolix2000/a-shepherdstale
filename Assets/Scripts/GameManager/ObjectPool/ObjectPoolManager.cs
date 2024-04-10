using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object which we want to pool.
/// </summary>
[System.Serializable]
public class ObjectItem // Game Objects which we want to pool and the desired amount. Usable in the instructor.
{
    [SerializeField]
    private GameObject objectToPool;
    public GameObject ObjectToPool // Setter and Getter for Game Object.
    {
        get { return this.objectToPool; }
        set { this.objectToPool = value; }
    }

    [SerializeField]
    private int amountToPool;
    public int AmountToPool // Setter and Getter for desired amount.
    {
        get { return this.amountToPool; }
        set { this.amountToPool = value; }
    }
}

/// <summary>
/// ObjectPoolManager to pool objects which will be activated and deactivated as needed.
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField]
    private GameObject cloudParent;
    public GameObject CloudParent // Setter and Getter for our parent object.
    {
        get { return this.cloudParent; }
        set { this.cloudParent = value; }
    }

    [SerializeField]
    private List<GameObject> pooledObjects = new List<GameObject>(); // Displays Objects in the inspector when they are pooled.
    public List<GameObject> PooledObjects // Setter and Getter for our displayed objects.
    {
        get { return this.pooledObjects; }
        set { this.pooledObjects = value; }
    }

    [SerializeField]
    private List<ObjectItem> itemsToPool; // List to put in the Objects that we want to pool.
    public List<ObjectItem> ItemsToPool // Setter and Getter for Objets that we want to pool.
    {
        get { return this.itemsToPool; }
        set { this.itemsToPool = value; }
    }

    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance // Getter for the ObjectPoolManager. Returns instance.
    {
        get
        {
            if (instance == null)
            {
                GameObject pool = new GameObject("ObjectPooler");
                instance = pool.AddComponent<ObjectPoolManager>();
            }
            return instance;
        }
    }

    void Awake() // Allows other scripts to access
    {
        instance = this;
    }

    // Start is called before the first frame update. Referencing DeactivateObjects() method.
    void Start()
    {
        DeactivateObjects();
    }

    /// <summary>
    /// When items are pooled they are set to an inactive state, so that they will not all spawn on top of each other in the scene.
    /// </summary>
    public void DeactivateObjects() // Deactivating the GameObject when scene starts.
    {
        //Debug.LogWarning(itemsToPool);
        foreach (ObjectItem item in itemsToPool)
        {
            for (int i = 0; i < item.AmountToPool; i++) // Instantiates the GameObjects specified number under amountToPool.
            {
                GameObject desiredObject = (GameObject)Instantiate(item.ObjectToPool, cloudParent.transform);
                desiredObject.SetActive(false); // Deactivate the GameObject
                pooledObjects.Add(desiredObject); // Adding the GameObject to the pool
            }
        }
    }

    public void ResetObjects() // Resetting GameObjects when scene starts.
    {
        int count = 0;
            foreach (Transform objects in cloudParent.transform) // Looping through objects.
            {
                objects.gameObject.SetActive(false); // Deactivate the GameObject
                pooledObjects[count].SetActive(false);
                count++;
            }
    }

    public GameObject GetPooledObject(string tag) // Returntype GameObject
    {
        // Iterate through pooledObjects
        for (int i = 0; i < pooledObjects.Count; i++) 
        {
            int randomObjectOfList = Random.Range(0, pooledObjects.Count); ;
            // Check if item listed is currently inactive in scene.
            if (!pooledObjects[randomObjectOfList].activeInHierarchy && pooledObjects[randomObjectOfList].tag == tag)
            {
                // Spawn a random GameObject from the pooledObjects list instead of sequentially spawning them.
                return pooledObjects[randomObjectOfList];
            }
        }
        return null;
    }
}
