namespace AI.BT
{
    public class Repeat : Decorator
    {
        protected int limit;
        protected int counter;

        public Repeat(int limit, Behavior child) 
            : base(child)
        {
            this.limit = limit;
        }
        
        public override void OnInitialize()
        {
            counter = 0;
        }

        public override Status Update()
        {
            while (true)
            {
                Status childStatus = child.Tick();

                if (childStatus == Status.Running) return Status.Running;
                if (childStatus == Status.Failure) return Status.Failure;
                if (++counter == limit) return Status.Success;
            }
        }
    }
}
