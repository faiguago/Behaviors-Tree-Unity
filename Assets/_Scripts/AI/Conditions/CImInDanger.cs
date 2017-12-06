using AI.BT;

public class CImInDanger : Behavior
{
    private AIController controller;

    public CImInDanger(AIController controller)
    {
        this.controller = controller;
    }

    public override Status Update()
    {
        if (controller.GetHealth() <= 2f)
            return Status.Success;
        else
            return Status.Failure;
    }
}
