using System.Collections;
using UnityEngine;

public class Imprisonment : Effect
{
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
        target.GetComponent<CombatCharacter>().AddEffect(this);
    }
}
