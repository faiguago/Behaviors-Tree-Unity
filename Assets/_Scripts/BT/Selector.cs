namespace AI.BT
{
    public class Selector : Composite
    {
        protected int index;
        protected Behavior currentChild;

        public Selector(Behavior behavior,
            params Behavior[] behaviors)
            : base(behavior, behaviors) { }

        public override void OnInitialize()
        {
            index = 0;
            currentChild = children[index];
        }

        public override Status Update()
        {
            while (true)
            {
                Status s = currentChild.Tick();

                if (s != Status.Failure)
                    return s;

                if (++index == children.Count)
                    return Status.Failure;

                currentChild = children[index];
            }
        }
    }
}