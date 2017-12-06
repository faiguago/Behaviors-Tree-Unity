using UnityEngine;
using AI.BT;

public class AEscape : Behavior
{
    private AIController controller;

    public AEscape(AIController controller)
    {
        this.controller = controller;
    }

    public override void OnInitialize()
    {
        controller._Agent.speed = 
            controller._Agent.speed * 3f;
        controller._Agent.destination = 
            controller._RefugePos;
    }

    public override Status Update()
    {
        if (Vector3.Distance(
            controller.transform.position, 
            controller._RefugePos) <= 1f)
            return Status.Success;
        else
            return Status.Running;
    }
}
