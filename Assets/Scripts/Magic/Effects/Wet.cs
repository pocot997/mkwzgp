using System.Collections;
using UnityEngine;

public class Wet : Effect
{
    [Header("Wet Settings")]
    [SerializeField] private Effect heal;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        target.GetComponent<CombatCharacter>().RemoveEffect(this);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        AddEffectsAndDependencies(target.GetComponent<CombatCharacter>());
    }
    private void AddEffectsAndDependencies(CombatCharacter combatCharacter)
    {
        combatCharacter.AddEffect(this);
        foreach (Effect effect in combatCharacter.activeEffects)
        {
            if (effect.gameObject.GetComponent<Burning>() || effect.gameObject.GetComponent<Ablaze>())
            {
                combatCharacter.RemoveEffect(effect);
                break;
            }
        }
        foreach (Effect effect in combatCharacter.activeEffects)
        {
            if (effect.gameObject.GetComponent<StoneProtection>())
            {
                Effect electric = Instantiate(heal, combatCharacter.transform);
                electric.ReleaseEffect(combatCharacter.gameObject);
                break;
            }
        }
    }
}
