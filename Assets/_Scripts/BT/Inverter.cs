namespace AI.BT
{
    public class Inverter : Decorator
    {
        public Inverter(Behavior child)
            : base(child) { }

        public override Status Update()
        {
            Status childStatus = child.Update();

            if (childStatus == Status.Success)
                return Status.Failure;
            else
                return Status.Success;
        }
    }
}
