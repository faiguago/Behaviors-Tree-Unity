using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private float lifeTime, speed;

    public ParticleSystem impactPS;

    // ---------------------------------------------------------------------
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // ---------------------------------------------------------------------
    private void Update()
    {
        float movementAmount = speed * Time.deltaTime;
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, movementAmount * 1.1f))
        {
            SendDamage(hit);

            Destroy(Instantiate(impactPS, hit.point, Quaternion.identity), 
                impactPS.main.startLifetimeMultiplier);
            Destroy(gameObject);
        }
        else
            transform.Translate(Vector3.forward * movementAmount);
    }

    // ---------------------------------------------------------------------
    private void SendDamage(RaycastHit hit)
    {
        IDamageable damageable = 
            hit.transform.gameObject.GetComponent<IDamageable>();

        if (damageable != null)
            damageable.ReceiveDamage(1, hit);
    }
}
