using UnityEngine;
using AI.BT;

public class AFire : Behavior
{
    private AIController controller;
    
    public AFire(AIController controller)
    {
        this.controller = controller;
    }
    
    public override Status Update()
    {
        LookAtTarget();
        InstantiateProjectile();

        return Status.Success;
    }

    private void LookAtTarget()
    {
        controller.transform.LookAt(controller._TargetPos);
    }

    private void InstantiateProjectile()
    {
        Object.Instantiate(controller._Projectile, 
            controller._SpawnerPos, controller._SpawnerRot);
    }
}
