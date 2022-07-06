using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Succeeder : Node
{
    protected Node node;

    public Succeeder(Node node)
    {
        this.node = node;
    }

    public override NodeState Evaluate()
    {        
        switch (node.Evaluate())
        {
            case NodeState.RUNNING:
                _nodeState = NodeState.RUNNING;
                break;
            case NodeState.SUCCESS:
                _nodeState = NodeState.SUCCESS;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                break;
            default:
                break;
        }
        return _nodeState;
    }
}
