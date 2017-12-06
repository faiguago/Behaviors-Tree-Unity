namespace AI.BT
{
    public class UntilSuccess : Repeat
    {
        public UntilSuccess(int limit, Behavior child)
            : base(limit, child) { }
        
        public override Status Update()
        {
            Status status = base.Update();

            if (status != Status.Success)
                return Status.Running;
            else
                return Status.Success;
        }
    }
}