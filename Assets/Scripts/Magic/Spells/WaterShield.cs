using UnityEngine;

public class WaterShield : Instant
{
    public override void OnHit(GameObject hitTarget)
    {
        CombatCharacter combatCharacter = hitTarget.GetComponent<CombatCharacter>();
        CharacterInstantiateEffect(hitTarget, combatCharacter);
        OnVanish();
    }

    public override void OnVanish()
    {
        Destroy(gameObject);
    }

    private void CharacterInstantiateEffect(GameObject hitTarget, CombatCharacter combatHit)
    {
        if (!combatHit.EffectExistance(effect))
        {
            Effect createdEffect = Instantiate(effect, hitTarget.transform);
            createdEffect.transform.position += Vector3.down;
            createdEffect.ReleaseEffect(combatHit.gameObject);
        }
    }
}
