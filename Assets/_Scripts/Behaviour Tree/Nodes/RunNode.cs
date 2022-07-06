using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RunNode : Node
{
    private EnemyAI ai;
    
    public RunNode(EnemyAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.SetColor(Color.blue);

        if(ai.path == null) return NodeState.FAILURE;

        if(ai.currentWaypoint >= ai.path.vectorPath.Count)
        {
            ai.reachedEndOfPath = true;
            return NodeState.SUCCESS;
        }
        else
        {
            ai.reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)ai.path.vectorPath[ai.currentWaypoint] - ai.rb.position).normalized;
        Vector2 force = direction * 800f * Time.deltaTime;

        ai.rb.AddForce(force);

        float distance = Vector2.Distance(ai.rb.position, ai.path.vectorPath[ai.currentWaypoint]);
        if (distance < ai.nextWaypointDistance)
        {
            ai.currentWaypoint++;
        }
        
        return NodeState.RUNNING;
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            ai.path = p;
            ai.currentWaypoint = 0;
        }
    }
}
