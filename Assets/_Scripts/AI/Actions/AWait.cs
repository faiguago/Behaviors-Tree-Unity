using UnityEngine;
using AI.BT;

public class AWait : Behavior
{
    private float timer;
    private float timeToWait;
    
    public AWait(float timeToWait)
    {
        this.timeToWait = timeToWait;
    }

    public override void OnInitialize()
    {
        timer = 0f;
    }

    public override Status Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeToWait) //<
            return Status.Success;
        else
            return Status.Running;
    }
}
