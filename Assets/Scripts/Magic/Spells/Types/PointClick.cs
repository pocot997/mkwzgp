using UnityEngine;

public abstract class PointClick : Spell
{
    [HideInInspector] public RaycastHit hitPlace;


    public override void ReleaseSpell()
    {
        OnHit(hitPlace.transform.gameObject);
    }

    public abstract void OnHit(GameObject hitTarget);
    public abstract void OnVanish();
}
