using UnityEngine;

public abstract class Instant : Spell
{
    public override void ReleaseSpell()
    {
        if (target == Target.SELF)
        {
            OnHit(GameObject.Find(caster).transform.gameObject);
        }
    }

    public abstract void OnHit(GameObject hitTarget);

    public abstract void OnVanish();
}
