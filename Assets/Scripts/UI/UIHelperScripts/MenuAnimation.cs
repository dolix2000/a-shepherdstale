using UnityEngine;

/// <summary>
/// Animates specific UI elements by moving their coordinates over a certain amount of time.
/// </summary>
public class MenuAnimation : MonoBehaviour
{
    public float speed;

    private Vector3 destination;

    void Start()
    {
        SetDestination(gameObject.transform.position);
    }

    void Update()
    {
        if (destination != gameObject.transform.position)
        {
            IncrementPosition();
        }
    }

    void IncrementPosition()
    {
        float delta = speed * Time.deltaTime;
        Vector3 currentPosition = gameObject.transform.position;
        Vector3 nextPosition = Vector3.MoveTowards(currentPosition, destination, delta);
        gameObject.transform.position = nextPosition;
    }

    public void SetDestination(Vector3 value)
    {
        destination = value;
    }
}
