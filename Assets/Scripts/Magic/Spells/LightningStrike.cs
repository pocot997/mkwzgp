using System.Collections;
using UnityEngine;

public class LightningStrike : PointClick
{
    [Header("LightningStrike Settings")]
    [SerializeField] private float damage = 5f;


    public override void OnHit(GameObject hitTarget)
    {
        string hitLayer = checkLayers(hitTarget.layer);

        switch (hitLayer)
        {
            case "Enemy":
                CharacterInstantiateEffect(hitTarget, hitTarget.GetComponent<CombatCharacter>());
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

    private void CharacterInstantiateEffect(GameObject hitTarget, CombatCharacter combatHit)
    {
        if (combatHit)
        {
            combatHit.ChangeHitPoints(-(damage));

            if (effect != null && !combatHit.EffectExistance(effect))
            { 
                Effect createdEffect = Instantiate(effect, hitTarget.transform);
                createdEffect.transform.position = hitTarget.transform.position;
                createdEffect.ReleaseEffect(combatHit.gameObject);
            }
        }
    }

    private IEnumerator Strike()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
