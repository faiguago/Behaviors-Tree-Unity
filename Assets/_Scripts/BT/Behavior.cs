namespace AI.BT
{
    public enum Status
    {
        Invalid,
        Success,
        Failure,
        Running,
        Aborted
    }

    public abstract class Behavior
    {
        private Status status;

        public abstract Status Update();

        public virtual void OnInitialize() { }
        public virtual void OnTerminate(Status status) { }

        public Behavior()
        {
            status = Status.Invalid;
        }

        public Status Tick()
        {
            if (status != Status.Running)
                OnInitialize();

            status = Update();

            if (status != Status.Running)
                OnTerminate(status);

            return status;
        }

        public virtual void Reset()
        {
            status = Status.Invalid;
        }

        public virtual void Abort()
        {
            OnTerminate(Status.Aborted);
            status = Status.Aborted;
        }

        public bool IsTerminated()
        {
            return status == Status.Success
                || status == Status.Failure;
        }

        public bool IsRunning()
        {
            return status == Status.Running;
        }

        public Status GetStatus()
        {
            return status;
        }
    }

}