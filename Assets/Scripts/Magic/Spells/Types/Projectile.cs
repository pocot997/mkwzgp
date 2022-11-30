using UnityEngine;

public abstract class Projectile : Spell
{
    [Header("Projectile Options")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float range;
    [SerializeField] protected float projectileSafeTime;

    [HideInInspector] public Vector3 hitPlace;

    protected float projectileReleaseTime;

    private Rigidbody rb;
    private Vector3 spawnPoint;


    public override void ReleaseSpell()
    {
        spawnPoint = transform.position;
        projectileReleaseTime = Time.realtimeSinceStartup;
        rb = GetComponent<Rigidbody>();
    }

    public abstract void OnHit(GameObject hitTarget);
    public abstract void OnVanish();

    private void Update()
    {
        rb.AddForce(transform.forward.normalized * moveSpeed * Time.deltaTime * 60, ForceMode.Acceleration);
        if(Vector3.Distance(transform.position, spawnPoint) > range)
        {
            OnVanish();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        hitPlace = other.ClosestPoint(this.transform.position);
        OnHit(other.gameObject);        
    }

    public bool CheckSelfHit(GameObject hitTarget, string hitLayer)
    {
        if (hitTarget.name == caster || hitLayer == caster || (hitTarget.transform.parent != null && hitTarget.transform.parent.name == caster))
        {
            if (projectileSafeTime + projectileReleaseTime > Time.time)
            {
                return true;
            }
        }
        return false;
    }
}
