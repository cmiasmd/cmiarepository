using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PatrolNode : Node
{
    private EnemyAI ai;
    
    public PatrolNode(EnemyAI ai)
    {
        this.ai = ai;
    }

    public override NodeState Evaluate()
    {
        ai.SetColor(Color.white);

        if(ai.path == null) return NodeState.FAILURE;

        if(ai.currentWaypoint >= ai.path.vectorPath.Count)
        {
            ai.newPos = new Vector2(ai.rb.position.x + Random.Range(-4, 4), ai.rb.position.y + Random.Range(-4, 4));
            ai.reachedEndOfPath = true;
            return NodeState.SUCCESS;
        }
        else
        {
            ai.reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)ai.path.vectorPath[ai.currentWaypoint] - ai.rb.position).normalized;
        Vector2 force = direction * ai.speed * Time.deltaTime;

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
