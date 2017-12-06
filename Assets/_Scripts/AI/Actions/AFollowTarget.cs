using AI.BT;
using UnityEngine;

public class AFollowTarget : Behavior
{
    private AIController controller;

    // --------------------------------------------------
    public AFollowTarget(AIController controller)
    {
        this.controller = controller;
    }

    // --------------------------------------------------
    public override void OnInitialize()
    {
        controller._Agent.isStopped = false;
        controller._Agent.destination = controller._TargetPos;
    }
    
    // --------------------------------------------------
    public override Status Update()
    {
        if (DistanceToTarget() <= controller._MaxDstToTarget)
        {
            controller._Agent.isStopped = true;
            return Status.Success;
        }

        if (!controller._Agent.pathPending)
            controller._Agent.destination = controller._TargetPos;
        
        return Status.Running;
    }

    private float DistanceToTarget()
    {
        return Vector3.Distance(
            controller._TargetPos, controller.transform.position);
    }
}
