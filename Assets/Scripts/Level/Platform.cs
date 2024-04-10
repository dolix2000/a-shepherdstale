using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform pos1, pos2;

    public Transform Pos1
    {
        get { return this.pos1; }
        set { this.pos1 = value; }
    }

    public Transform Pos2
    {
        get { return this.pos2; }
        set { this.pos2 = value; }
    }


    public float speed;
    public float Speed
    {
        get { return this.speed; }
        set { this.speed = value; }
    }


    public Transform startPos;

    public Transform StartPos
    {
        get { return this.startPos; }
        set { this.startPos = value; }
    }



    private Vector3 nextPos;

    public Vector3 NextPos
    {
        get { return this.nextPos; }
        set { this.nextPos = value; }
    }



    void Start()
    {
        nextPos = startPos.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
}
