using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : LivingEntity
{
    public float speed = 7f;
    public float timeBetweenShoots = 0.5f;

    private Transform cameraT;

    [SerializeField]
    private GameObject projectileGO;

    [SerializeField]
    private Transform spawnerT;
    
    private float nextShootTimer;

    private Rigidbody rb;

    // ------------------------------------------
    private void Start()
    {
        cameraT = Camera.main.transform;

        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    // ------------------------------------------
    private void Update()
    {
        Move();

        if (Input.GetButton("Fire1"))
            Shoot();

        LookAtMousePos();
    }
    
    // ------------------------------------------
    private void Shoot()
    {
        nextShootTimer += Time.deltaTime;

        if (nextShootTimer >= timeBetweenShoots)
        {
            nextShootTimer = 0f;
            InstantiateProjectile();
        }
    }

    // ------------------------------------------
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        if (x == 0 && z == 0)
            return;

        Vector3 dir = cameraT.transform
            .TransformDirection(new Vector3(x, 0, z));
        dir.y = 0;
        dir.Normalize();

        rb.MovePosition(rb.position + dir * Time.deltaTime * speed);
    }

    // ------------------------------------------
    private void InstantiateProjectile()
    {
        Instantiate(projectileGO,
            spawnerT.position, Quaternion.Euler(0, spawnerT.rotation.eulerAngles.y, 0));
    }

    // ------------------------------------------
    private void LookAtMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask.GetMask("Ground")))
        {
            Vector3 dir = (new Vector3(hit.point.x,
                spawnerT.position.y, hit.point.z) - spawnerT.position);
            GLDebug.DrawRay(spawnerT.position, dir.magnitude * spawnerT.up);

            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
        }
    }

    // ------------------------------------------
    public override void ReceiveDamage(
        int damageAmount, RaycastHit hit)
    {
        base.ReceiveDamage(damageAmount, hit);
    }

    // ------------------------------------------
    public override void Die()
    {
        Destroy(gameObject);
    }
}