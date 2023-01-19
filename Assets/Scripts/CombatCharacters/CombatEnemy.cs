using DVSN.Enemy;
using DVSN.GameManagment;
using System.Collections.Generic;

public class CombatEnemy : CombatCharacter
{
    internal delegate void EnterCombat(CombatEnemy enemy);
    internal static event EnterCombat onEnterCombat;

    public void StartCombat() // Trzeba wywolac
    {
        if (onEnterCombat != null)
        {
            Managers.BattleLoader.isInBattle = true;
            onEnterCombat(this);
        }
    }

    public override void Die()
    {
        GetComponent<EnemyAi>().Die();
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
