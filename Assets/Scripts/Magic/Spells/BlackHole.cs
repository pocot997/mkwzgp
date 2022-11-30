using UnityEngine;

public class BlackHole : PointClick
{
    public override void OnHit(GameObject hitTarget)
    {
        string hitLayer = checkLayers(hitTarget.layer);
        switch (hitLayer)
        {
            case "Enemy":
                CharacterInstantiateEffect(hitTarget);
                break;

            case "Ground":
                InstantiateEffect(hitTarget, hitPlace.point);
                break;

            case "Player":
                CharacterInstantiateEffect(hitTarget);
                break;

            default:
                InstantiateEffect(hitTarget, hitPlace.point);
                break;
        }
        OnVanish();
    }

    public override void OnVanish()
    {
        Destroy(gameObject);
    }

    private void InstantiateEffect(GameObject hitTarget, Vector3 hitPosition)
    {
        if (effect != null)
        {
            GameObject createdEffect = Instantiate(effect.gameObject, hitPosition + Vector3.up, effect.transform.rotation);
            createdEffect.GetComponent<Effect>().ReleaseEffect(hitTarget);
        }
    }

    private void CharacterInstantiateEffect(GameObject hitTarget)
    {
        RaycastHit hit;
        if (Physics.Raycast(hitTarget.transform.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.NameToLayer("Ground"), QueryTriggerInteraction.Collide))
        {
            InstantiateEffect(hitTarget, hit.point);
        }
    }
}
