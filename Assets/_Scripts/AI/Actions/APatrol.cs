using AI.BT;

public class APatrol : Behavior
{
    private int index;

    private AIController controller;

    public APatrol(AIController controller)
    {
        this.controller = controller;
    }

    public override void OnInitialize()
    {
        controller._Agent.isStopped = false;
        controller._Agent.destination 
            = controller._WayPointsPos[index];
    }

    public override void OnTerminate(Status status) { }

    public override Status Update()
    {
        if (!controller._Agent.pathPending
            && controller._Agent.remainingDistance <= 1f)
        {
            index = (index + 1) % controller._WayPointsPos.Length;
            controller._Agent.destination = controller._WayPointsPos[index];
        }

        return Status.Running;
    }
}
