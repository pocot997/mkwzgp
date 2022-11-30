using UnityEngine;

public class Fireball : Projectile
{
    [Header("Fireball Settings")]
    [SerializeField] private float damage = 30f;


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

    private void CharacterInstantiateEffect(GameObject hitTarget, CombatCharacter combatHit)
    {
        if (combatHit)
        {
            combatHit.ChangeHitPoints(-(damage));

            if (effect != null && !combatHit.EffectExistance(effect))
            {
                Effect createdEffect = Instantiate(effect, hitTarget.transform);
                createdEffect.transform.position = hitTarget.transform.position;
                createdEffect.ReleaseEffect(combatHit.gameObject);
            }
        }
    }
}
