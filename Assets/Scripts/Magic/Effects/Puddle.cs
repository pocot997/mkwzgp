using System.Collections;
using UnityEngine;

public class Puddle : Effect
{
    [Header("Puddle Settings")]
    [SerializeField] private Effect Wet;


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
        transform.gameObject.layer = LayerMask.NameToLayer("Ground");
        StartCoroutine(EffectCoroutine(target));
    }

    private void OnTriggerEnter(Collider other)
    {
        CombatCharacter combatCharacter = other.transform.GetComponent<CombatCharacter>();
        if (combatCharacter)
        {
            InstantaiteEffect(other.gameObject, combatCharacter);
        }
        else
        {
            combatCharacter = other.transform.GetComponentInParent<CombatCharacter>();
            if (combatCharacter)
            { 
                InstantaiteEffect(other.gameObject, combatCharacter);
            }
        }
    }

    private void InstantaiteEffect(GameObject coliderObject, CombatCharacter combatCharacter)
    {
        Effect effect = Instantiate(Wet, coliderObject.transform);
        if (!combatCharacter.EffectExistance(effect))
        {
            effect.ReleaseEffect(combatCharacter.gameObject);
        }
        else
        {
            Destroy(effect);
        }
    }
}
