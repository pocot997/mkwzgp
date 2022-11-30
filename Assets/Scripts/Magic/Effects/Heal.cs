using System.Collections;
using UnityEngine;

public class Heal : Effect
{
    [Header("Heal Settings")]
    [SerializeField] private float healAmount;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            target.GetComponent<CombatCharacter>().ChangeHitPoints(healAmount);
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        target.GetComponent<CombatCharacter>().RemoveEffect(this);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        target.GetComponent<CombatCharacter>().AddEffect(this);
    }
}
