using System;
using System.Collections.Generic;

namespace AI.BT
{
    public class Composite : Behavior
    {
        protected List<Behavior> children;

        protected Composite(Behavior behavior,
            params Behavior[] behaviors)
        {
            children = new List<Behavior>();

            if (behavior != null)
            {
                children.Add(behavior);
            }

            if (behaviors != null)
            {
                for (int i = 0; i < behaviors.Length; i++)
                    children.Add(behaviors[i]);
            }
        }

        public override void Reset()
        {
            base.Reset();

            for (int i = 0; i < children.Count; i++)
                children[i].Reset();
        }

        public override void Abort()
        {
            base.Abort();

            for (int i = 0; i < children.Count; i++)
                children[i].Abort();
        }

        public override Status Update()
        {
            throw new NotImplementedException();
        }
    }
}