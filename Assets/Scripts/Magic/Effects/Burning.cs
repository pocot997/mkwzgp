using System.Collections;
using UnityEngine;

public class Burning : Effect
{
    [Header("Burning Settings")]
    [SerializeField] private float damage;

    private CombatCharacter combatCharacter;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            combatCharacter.ChangeHitPoints(-damage);
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        target.GetComponent<CombatCharacter>().RemoveEffect(this);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        combatCharacter = target.GetComponent<CombatCharacter>();
        combatCharacter.AddEffect(this);
    }
}
