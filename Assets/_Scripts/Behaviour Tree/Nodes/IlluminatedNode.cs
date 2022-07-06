using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IlluminatedNode : Node
{
    private EnemyAI ai;
    private Transform target;
    private Transform origin;

    public IlluminatedNode(EnemyAI ai, Transform target, Transform origin)
    {
        this.ai = ai;
        this.target = target;        
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        if(ai.isIlluminated())
        {
            float x = 0;
            float y = 0;

            if(target.position.x >= origin.position.x) x = origin.position.x - 1f;
            else x = origin.position.x + 1f;

            if(target.position.y >= origin.position.y) x = origin.position.y - 1f;
            else y = origin.position.y + 1f;

            ai.newPos = new Vector2(x, y);
            return NodeState.SUCCESS;
        }
        else
        {
            return NodeState.FAILURE;
        }
    }
}
