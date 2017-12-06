namespace AI.BT
{
    public class ActiveSelector : Selector
    {
        public ActiveSelector(Behavior behavior,
            params Behavior[] behaviors)
            : base(behavior, behaviors) { }

        public override void OnInitialize()
        {
            index = children.Count - 1;
            currentChild = children[index];
        }

        public override Status Update()
        {
            int previous = index;

            base.OnInitialize();
            Status result = base.Update();

            if (previous != children.Count && index != previous)
                children[previous].Abort();

            return result;
        }
    }
}