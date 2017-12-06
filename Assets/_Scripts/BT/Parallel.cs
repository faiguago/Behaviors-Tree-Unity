namespace AI.BT
{
    public class Parallel : Composite
    {
        public enum Policy
        {
            RequireOne, 
            RequireAll
        }

        protected Policy successPolicy;
        protected Policy failurePolicy;

        protected Parallel(Policy forSuccess, Policy forFailure)
            : this(forSuccess, forFailure, null, null) { }

        public Parallel(Policy forSuccess, Policy forFailure,
            Behavior behavior, params Behavior[] behaviors)
            : base(behavior, behaviors)
        {
            successPolicy = forSuccess;
            failurePolicy = forFailure;
        }
        
        public override Status Update()
        {
            int successCount = 0;
            int failureCount = 0;

            for (int i = 0; i < children.Count; i++)
            {
                Behavior b = children[i];

                if (!b.IsTerminated())
                    b.Tick();

                if (b.GetStatus() == Status.Success)
                {
                    ++successCount;
                    if (successPolicy == Policy.RequireOne)
                        return Status.Success;
                }

                if (b.GetStatus() == Status.Failure)
                {
                    ++failureCount;
                    if (failurePolicy == Policy.RequireOne)
                        return Status.Failure;
                }
            }

            if (failurePolicy == Policy.RequireAll
                && failureCount == children.Count)
                return Status.Failure;

            if (successPolicy == Policy.RequireAll
                && successCount == children.Count)
                return Status.Success;

            return Status.Running;
        }

        public override void OnTerminate(Status status)
        {
            for (int i = 0; i < children.Count; i++)
            {
                Behavior b = children[i];

                if (b.IsRunning())
                    b.Abort();
            }
        }
    }
}
