using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeNode : Node
{
    private float range;
    private EnemyAI ai;
    private Transform target;
    private Transform origin;


    public RangeNode(float range, EnemyAI ai, Transform target, Transform origin)
    {
        this.range = range;
        this.ai = ai;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector2.Distance(target.position, origin.position);
        if(distance <= range)
        {
            ai.SetColor(Color.yellow);
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
