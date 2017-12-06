using UnityEngine;
using AI.BT;

public class CIsTargetVisible : Behavior
{
    private AIController controller;

    public CIsTargetVisible(AIController controller)
    {
        this.controller = controller;
    }

    public override Status Update()
    {
        Vector3 dir = (controller._TargetPos 
            - controller.transform.position).normalized;

        Ray ray = new Ray(controller.transform.position, dir);
        RaycastHit hit;

        bool isInAngleView = Vector3.Angle(
            controller.transform.forward, dir) <= controller._MaxAngleView;

        GLDebug.DrawRay(ray.origin, ray.direction * controller._MaxDstView,
            isInAngleView ? Color.green : Color.red);

        if (isInAngleView && Physics.Raycast(ray, out hit, controller._MaxDstView)
            && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("Target is visible!");

            return Status.Success;
        }
        else
        {
            //Debug.Log("Target isn't visible!");

            return Status.Failure;
        }
    }
}
