using UnityEngine;

public class SplashOfWater : Projectile
{
    [Header("Splash of Water Settings")]
    [SerializeField] private Effect wet;


    public override void OnHit(GameObject hitTarget)
    {
        string hitLayer = checkLayers(hitTarget.layer);
        if (CheckSelfHit(hitTarget, hitLayer))
        {
            return;
        }

        switch (hitLayer)
        {
            case "Enemy":
                CharacterInstantiateEffect(hitTarget, hitTarget.GetComponent<CombatCharacter>());
                break;

            case "Ground":
                InstantiateEffect(hitTarget);
                break;

            case "Player":
                CharacterInstantiateEffect(hitTarget, hitTarget.GetComponentInParent<CombatCharacter>());
                break;

            default:
                break;
        }
        OnVanish();
    }

    public override void OnVanish()
    {
        Destroy(gameObject);
    }

    private void InstantiateEffect(GameObject hitTarget)
    {
        if (effect != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(hitPlace + new Vector3(0, 100, 0), Vector3.down, out hit, Mathf.Infinity))
            {
                Effect createdEffect = Instantiate(effect, hit.point + new Vector3(0, -gameObject.GetComponent<Collider>().bounds.size.y + 0.12f, 0), effect.transform.rotation);
                createdEffect.ReleaseEffect(hitTarget);
            }
        }
    }

    private void CharacterInstantiateEffect(GameObject hitTarget, CombatCharacter combatHit)
    {
        if (combatHit)
        {
            if (wet != null && !combatHit.EffectExistance(wet))
            {
                Effect createdEffect = Instantiate(wet, hitTarget.transform);
                createdEffect.transform.position = hitTarget.transform.position;
                createdEffect.ReleaseEffect(combatHit.gameObject);
            }
        }
    }
}
