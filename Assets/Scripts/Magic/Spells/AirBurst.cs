using UnityEngine;

public class AirBurst : PointClick
{
    public override void OnHit(GameObject hitTarget)
    {
        string hitLayer = checkLayers(hitTarget.layer);
        switch(hitLayer)
        {
            case "Enemy":
                InstantiateEffect(hitTarget);
                break;

            case "Ground":
                InstantiateEffect(hitTarget);
                break;

            case "Player":
                InstantiateEffect(hitTarget);
                break;

            default:
                break;
        }
        OnVanish();
    }

    public override void OnVanish()
    {
        Destroy(gameObject);
    }

    private void InstantiateEffect(GameObject hitTarget)
    {
        if (effect != null)
        {
            Effect createdEffect = Instantiate(effect, hitPlace.point, effect.transform.rotation);
            createdEffect.ReleaseEffect(hitTarget);
        }
    }
}
