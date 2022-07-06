using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GUObject : MonoBehaviour
{
    public void updateGUO(Bounds bounds)
    {
        var guo = new GraphUpdateObject(bounds);
        guo.updatePhysics = true;
        AstarPath.active.UpdateGraphs(guo);
    }
}
