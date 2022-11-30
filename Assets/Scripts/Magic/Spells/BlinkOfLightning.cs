using System.Collections;
using UnityEngine;

public class BlinkOfLightning : PointClick
{
    public override void OnHit(GameObject hitTarget)
    {
        string hitLayer = checkLayers(hitTarget.layer);
        switch (hitLayer)
        {
            case "Enemy":
                CharacterInstantiateEffect(hitTarget, hitTarget.GetComponent<CombatCharacter>());
                break;

            case "Ground":
                InstantiateEffect();
                break;

            case "Player":
                CharacterInstantiateEffect(hitTarget, hitTarget.GetComponentInParent<CombatCharacter>());
                break;

            default:
                break;
        }
        OnVanish();
    }

    public override void OnVanish()
    {
        StartCoroutine(Strike());
    }

    private void InstantiateEffect()
    {
        Transform casterTransform = GameObject.Find(caster).transform;
        casterTransform.position = transform.position + Vector3.up;
    }

    private void CharacterInstantiateEffect(GameObject hitTarget, CombatCharacter combatHit)
    {
        if (combatHit)
        {
            Transform casterTransform = GameObject.Find(caster).transform;
            Vector3 tmpPosition = hitTarget.transform.position;
            hitTarget.transform.position = casterTransform.position;
            casterTransform.position = tmpPosition;
        }
    }

    private IEnumerator Strike()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
