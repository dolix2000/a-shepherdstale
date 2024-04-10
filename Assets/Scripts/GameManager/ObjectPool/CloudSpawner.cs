using UnityEngine;

/// <summary>
/// Helper class to determine how fast the clouds spawn. 
/// </summary>
public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    private float speed;
    public float Speed // Setter and Getter for clouds' speed.
    {
        get { return this.speed; }
        set { this.speed = value; }
    }

    [SerializeField]
    private float positionX;
    public float PositionX // Setter and Getter for clouds' position on the x-axis. Viewable in inspector.
    {
        get { return this.positionX; }
        set { this.positionX = value; }
    }

    public void StartFloating() // Calculating the speed. Deactivating clouds and returning them to object pool.
    {
        transform.Translate(Vector3.right * (Time.deltaTime * speed));
        if (transform.position.x > positionX) { 
            gameObject.SetActive(false); // When clouds reach the endpoint, they will return to object pool.
        }
    }

    // Update is called once per frame. References StartFloating method.
    void Update()
    {
       StartFloating();
    }
}

