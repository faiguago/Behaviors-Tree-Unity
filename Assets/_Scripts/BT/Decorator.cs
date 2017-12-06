using System;

namespace AI.BT
{
    public class Decorator : Behavior
    {
        protected Behavior child;

        public Decorator(Behavior child)
        {
            this.child = child;
        }

        public override void Reset()
        {
            base.Reset();
            child.Reset();
        }

        public override void Abort()
        {
            base.Abort();
            child.Abort();
        }

        public override Status Update()
        {
            throw new NotImplementedException();
        }
    }
}
