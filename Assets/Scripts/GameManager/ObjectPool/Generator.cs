using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generator to spawn clouds.
/// </summary>
public class Generator : MonoBehaviour
{
    [SerializeField]
    float interval; // Interval on how often clouds spawn. Set in the inspector.
    public float Interval
    {
        get { return this.interval; }
        set { this.interval = value; }
    }

    [SerializeField]
    GameObject endPoint; // Point where clouds will return to ObjectPool themselves.
    public GameObject Endpoint
    {
        get { return this.endPoint; }
        set { this.endPoint = value; }
    }

    List<GameObject> clouds = new List<GameObject>();

    private Vector3 startPosition; // Where clouds start to spawn
    public Vector3 StartPosition
    {
        get { return this.startPosition; }
        set { this.startPosition = value; }
    }

    // Start is called before the first frame update. Invokes SpawnClouds method with given interval.
    void Start()
    {
        startPosition = transform.position;
        InvokeRepeating("SpawnClouds", 0, interval); // Using the interval to repeatedly spawn the clouds.
    }

    /// <summary>
    /// Variables for spawning the clouds.
    /// </summary>
    void SpawnClouds()
    {
        GameObject cloud = ObjectPoolManager.Instance.GetPooledObject("Cloud"); // References the ObjectPoolManager
        cloud.transform.position = startPosition; // Setting the startposition to the point where the clouds spawn.
        clouds.Add(cloud); // Add clouds to the new List
        Debug.Log("Grabbing <color=cyan>clouds</color> from ObjectPool.");
        if (cloud != null) // When there are no clouds active in the scene..
        {
            int randomInterval = UnityEngine.Random.Range(0, clouds.Count); // .. use a random cloud of the pool..
            if (!clouds[randomInterval].activeSelf)
            {
                clouds[randomInterval].SetActive(true); // ..and set them active according to the interval.
                // Varying position on the y-axis, so that clouds will not spawn in one straight line.
                startPosition.y = UnityEngine.Random.Range(startPosition.y - 0.5f, startPosition.y + 0.5f);
                clouds[randomInterval].transform.position = startPosition;

                // Varying the size of clouds, so that they will not have the same size.
                float scale = UnityEngine.Random.Range(1.0f, 1.5f);
                clouds[randomInterval].transform.localScale = new Vector2(scale, scale);

                // Varying speed in which clouds spawn.
                float spawnTimer = UnityEngine.Random.Range(0.8f, 1.5f);
                clouds[randomInterval].GetComponent<CloudSpawner>().Speed = spawnTimer;
                clouds[randomInterval].GetComponent<CloudSpawner>().PositionX = endPoint.transform.position.x;
                clouds[randomInterval].GetComponent<CloudSpawner>().StartFloating();
            }         
        }
    }
}
