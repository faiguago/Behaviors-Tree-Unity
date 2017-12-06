namespace AI.BT
{
    public class Monitor : Parallel
    {
        private class ToMonitoringMode : Decorator
        {
            public ToMonitoringMode(Behavior child)
                : base(child) { }

            public override Status Update()
            {
                Status childStatus = child.Update();

                if (childStatus == Status.Success)
                    return Status.Running;
                else
                    return Status.Failure;
            }
        }

        public Monitor(Behavior condition, Behavior action)
            : base(Policy.RequireOne, Policy.RequireOne)
        {
            children.Add(new ToMonitoringMode(condition));
            children.Add(action);
        }
    }
}
