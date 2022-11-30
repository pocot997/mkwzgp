using System.Collections;
using UnityEngine;

public class StoneProtection : Effect
{
    public override IEnumerator EffectCoroutine(GameObject target)
    {
        CombatCharacter combatCharacter = target.transform.GetComponent<CombatCharacter>();
        if (combatCharacter)
        {
            combatCharacter.effectReduceDamage = 0.4f;
            while (Time.time < startTime + timeOfEffect)
            {
                yield return new WaitForSeconds(frequencyOfEffect);
            }
            combatCharacter.effectReduceDamage = 1.0f;
        }
        target.GetComponent<CombatCharacter>().RemoveEffect(this);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        target.GetComponent<CombatCharacter>().AddEffect(this);
    }
}
