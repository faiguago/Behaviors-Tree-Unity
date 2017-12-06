using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using AI.BT;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : LivingEntity
{
    [SerializeField]
    private float maxAngleView = 90f;
    public float _MaxAngleView
    {
        get
        {
            return maxAngleView;
        }
    }

    [SerializeField]
    private float maxDstView = 15f;
    public float _MaxDstView
    {
        get
        {
            return maxDstView;
        }
    }

    [SerializeField]
    private float maxDstToTarget = 3f;
    public float _MaxDstToTarget
    {
        get
        {
            return maxDstToTarget;
        }
    }

    [SerializeField]
    private float isInRangeDst = 10f;
    public float _IsInRangeDst
    {
        get
        {
            return isInRangeDst;
        }
    }

    [SerializeField]
    private GameObject projectileGO;
    public GameObject _Projectile
    {
        get
        {
            return projectileGO;
        }
    }

    [SerializeField]
    private Transform spawnerT;
    public Vector3 _SpawnerPos
    {
        get
        {
            return spawnerT.position;
        }
    }
    public Quaternion _SpawnerRot
    {
        get
        {
            return Quaternion.Euler(
                0, spawnerT.eulerAngles.y + 180f, 0);
        }
    }

    private Transform targetT;
    public Vector3 _TargetPos
    {
        get
        {
            if (targetT)
                return targetT.position;
            else
                return Vector3.zero;
        }
    }

    [SerializeField]
    private Transform refugeT;
    public Vector3 _RefugePos
    {
        get
        {
            return refugeT.position;
        }
    }

    private NavMeshAgent agent;
    public NavMeshAgent _Agent
    {
        get
        {
            return agent;
        }
    }

    [SerializeField]
    private Transform[] wayPointsT;
    private Vector3[] wayPointsPos;
    public Vector3[] _WayPointsPos
    {
        get
        {
            return wayPointsPos;
        }
    }

    private Behavior root;

    // -------------------------------------------------------------
    private void Start ()
    {
        InitializeVars();
	}

    // -------------------------------------------------------------
    private void InitializeVars()
    {
        agent = GetComponent<NavMeshAgent>();
        wayPointsPos = wayPointsT.Select(p => p.position).ToArray();
        targetT = FindObjectOfType<PlayerController>().transform;

        root = new ActiveSelector(
                    new Sequence(
                        new CImInDanger(this),
                        new AEscape(this),
                        new APray(this)
                        ),
                    new Sequence(
                        new CIsTargetVisible(this),
                        new Monitor(
                            new CIsTargetVisible(this),
                            new Parallel(Parallel.Policy.RequireAll, Parallel.Policy.RequireAll,
                                new UntilSuccess(0,
                                    new Sequence(
                                        new CIsInRange(this),
                                        new AWait(0.5f),
                                        new AFire(this))),
                                new UntilSuccess(0,
                                    new Sequence(
                                        new Inverter(new CIsInRange(this)),
                                        new AFollowTarget(this)))))),
                    new APatrol(this));
    }

    // -------------------------------------------------------------
    private void Update ()
    {
        root.Tick();
	}

    // -------------------------------------------------------------
    public override void ReceiveDamage(
        int damageAmount, RaycastHit hit)
    {
        base.ReceiveDamage(damageAmount, hit);
    }

    // -------------------------------------------------------------
    public override void Die()
    {
        Destroy(gameObject);
    }
}
