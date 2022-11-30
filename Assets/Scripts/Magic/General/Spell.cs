using UnityEngine;

public enum SpellType
{
    POINT_CLICK = 0,
    PROJECTILE = 1,
    INSTANT = 2
}

public enum Target
{
    ENEMY = 0,
    SELF = 1,
    TERRAIN = 2,
    ALL = 3
}
public abstract class Spell: MonoBehaviour
{
    [Header("Spell Options")]
    [SerializeField] protected int id;
    [SerializeField] protected new string name;
    [SerializeField] internal float castTime = 3f;
    [SerializeField] protected Target target;
    public Effect effect;
    public SpellType type;
    [HideInInspector] public string caster;

    
    public abstract void ReleaseSpell();

    public string checkLayers(LayerMask layer)
    {
        if (layer == LayerMask.NameToLayer("Enemy"))
        {
            return "Enemy";
        }
        else if (layer == LayerMask.NameToLayer("Ground"))
        {
            return "Ground";
        }
        else if (layer == LayerMask.NameToLayer("Player"))
        {
            return "Player";
        }
        else return "";
    }
}
