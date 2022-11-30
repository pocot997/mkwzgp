using System.Collections;
using UnityEngine;

public class WaterProtection : Effect
{
    [Header("Water Protection Settings")]
    [SerializeField] private Effect electrocuted;

    private GameObject protectedCharacter;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        protectedCharacter.GetComponent<CombatCharacter>().RemoveEffect(this);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        protectedCharacter = target;
        transform.gameObject.layer = LayerMask.NameToLayer("Shield");
        protectedCharacter.GetComponent<CombatCharacter>().AddEffect(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Spell spell = other.transform.gameObject.GetComponent<Spell>();
        if(spell)
        {
            // if i was casting projectile that hit the barrier
            if (spell.type == SpellType.PROJECTILE && spell.caster == protectedCharacter.name)
            {
                return;
            }

            if(spell.effect.element == Element.LIGHTNING)
            {
                Destroy(spell.gameObject);
                if (!protectedCharacter.GetComponent<CombatCharacter>().EffectExistance(electrocuted))
                {
                    Effect createdEffect = Instantiate(electrocuted, protectedCharacter.transform);
                    createdEffect.transform.position = protectedCharacter.transform.position;
                    createdEffect.ReleaseEffect(protectedCharacter);
                }
            }
            else if (spell.effect.element == Element.EARTH)
            {
                Destroy(spell.gameObject);
                protectedCharacter.GetComponent<CombatCharacter>().RemoveEffect(this);
            }
            else
            {
                Destroy(spell.gameObject);
            }
        }
    }
}
