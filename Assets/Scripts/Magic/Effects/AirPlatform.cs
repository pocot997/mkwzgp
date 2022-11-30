using System.Collections;
using UnityEngine;

public class AirPlatform : Effect
{
    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        Destroy(gameObject);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        StartCoroutine(EffectCoroutine(target));
    }
}
