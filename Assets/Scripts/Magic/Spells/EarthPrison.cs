using UnityEngine;

public class EarthPrison : Projectile
{
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
            if (effect != null && !combatHit.EffectExistance(effect))
            {
                Effect createdEffect = Instantiate(effect, hitTarget.transform.position + Vector3.down, effect.transform.rotation);
                createdEffect.ReleaseEffect(combatHit.gameObject);
            }
        }
    }
}
