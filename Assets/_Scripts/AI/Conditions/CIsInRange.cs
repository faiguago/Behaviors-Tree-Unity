using UnityEngine;
using AI.BT;

public class CIsInRange : Behavior
{
    private AIController controller;

    // --------------------------------------------------
    public CIsInRange(AIController controller)
    {
        this.controller = controller;
    }

    // --------------------------------------------------
    public override Status Update()
    {
        if (DistanceToTarget() 
            <= controller._IsInRangeDst)
            return Status.Success;
        else
            return Status.Failure;
    }

    private float DistanceToTarget()
    {
        return Vector3.Distance(
            controller.transform.position, controller._TargetPos);
    }
}
