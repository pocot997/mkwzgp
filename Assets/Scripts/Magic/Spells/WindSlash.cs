using UnityEngine;

public class WindSlash : Projectile
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

            case "Ground":
                return;

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
            Rigidbody rb = hitTarget.transform.GetComponentInParent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(150000f, transform.position, 2f);
            }

            if (effect != null && !combatHit.EffectExistance(effect))
            {
                foreach (Effect ef in combatHit.activeEffects)
                {
                    if (ef.gameObject.GetComponent<Burning>())
                    {
                        combatHit.RemoveEffect(ef);
                        Effect createdEffect = Instantiate(effect, hitTarget.transform);
                        createdEffect.transform.position = hitTarget.transform.position;
                        createdEffect.ReleaseEffect(combatHit.gameObject);
                        break;
                    }
                }
            }
        }
    }
}
