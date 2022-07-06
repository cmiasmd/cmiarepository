using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloserNode : Node
{
    private float range;
    private Transform target;
    private Transform origin;


    public CloserNode(float range, Transform target, Transform origin)
    {
        this.range = range;
        this.target = target;
        this.origin = origin;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector2.Distance(target.position, origin.position);
        return distance <= range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
