using DVSN.GameManagment;
using System.Collections.Generic;

public class CombatEnemy : CombatCharacter
{
    internal delegate void EnterCombat(CombatEnemy enemy);
    internal static event EnterCombat onEnterCombat;

    void StartCombat() // Trzeba wywolac
    {
        if (onEnterCombat != null)
        {
            onEnterCombat(this);
        }
    }

    public override void Die()
    {
        Destroy(this.gameObject);
    }

    public override void ChangeHitPoints(float value)
    {
        if (value < 0)
        {
            value *= effectReduceDamage;
        }

        HitPoints += value;
        if (!CheckIsAlive())
        {
            Die();
        }
        else if (HitPoints > maxHitPoints)
        {
            HitPoints = maxHitPoints;
        }
    }
}
