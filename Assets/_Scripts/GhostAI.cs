using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GhostAI : MonoBehaviour
{
    private int actualTarget = 0;
    private bool reversePath = false;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField]
    public RoomsFirstMapGenerator RoomManager;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (actualTarget == (RoomManager.roomCentersList.Count-1))
            reversePath = true;
        else if (actualTarget == 0)
            reversePath = false;

        if (reachedEndOfPath)
        {
            if (reversePath)
                actualTarget--;
            else
                actualTarget++;
        }

        if (seeker.IsDone())
          seeker.StartPath(rb.position, (Vector2)RoomManager.roomCentersList[actualTarget], OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }
}
