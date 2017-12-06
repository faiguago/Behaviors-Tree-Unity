using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    private Vector3 _TargetPos
    {
        get
        {
            if (target)
                return target.position;
            else
                return Vector3.zero;
        }
    }

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - _TargetPos;
    }

    private void LateUpdate()
    {
        transform.position = _TargetPos + offset;
    }
}
