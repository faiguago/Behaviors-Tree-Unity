namespace AI.BT
{
    public class Sequence : Composite
    {
        private int index;
        protected Behavior currentChild;

        public Sequence(Behavior behavior,
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

                if (s != Status.Success)
                    return s;

                if (++index == children.Count)
                    return Status.Success;

                currentChild = children[index];
            }
        }
    }
}