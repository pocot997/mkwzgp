using System.Collections;
using UnityEngine;

public class Ablaze : Effect
{
    [Header("Ablaze Settings")]
    [SerializeField] private float damage;

    private CombatCharacter combatCharacter;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            combatCharacter.ChangeHitPoints(-damage);
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        combatCharacter.RemoveEffect(this);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        combatCharacter = target.GetComponent<CombatCharacter>();
        combatCharacter.AddEffect(this);
    }
}
