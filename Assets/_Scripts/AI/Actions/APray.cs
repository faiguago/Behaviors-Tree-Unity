using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BT;

public class APray : Behavior
{
    private AIController controller;

    public APray(AIController controller)
    {
        this.controller = controller;
    }

    public override Status Update()
    {
        Debug.Log("Praying!");

        return Status.Running;
    }
}
