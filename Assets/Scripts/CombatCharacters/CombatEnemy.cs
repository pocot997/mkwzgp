using DVSN.GameManagment;

public class CombatEnemy : CombatCharacter
{
    public override void Die()
    {
        Destroy(this.gameObject);
    }
}
