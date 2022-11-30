using System.Collections;
using UnityEngine;
using DVSN.GameManagment;
using DVSN.Player;

namespace DVSN.Spellcasting
{
    public class Shock : Effect
    {
        [Header("Shock Settings")]
        [SerializeField] private Effect electrocuted;


        public override IEnumerator EffectCoroutine(GameObject target)
        {
            CombatEnemy combatEnemy = target.transform.GetComponent<CombatEnemy>();
            if (combatEnemy)
            {
                combatEnemy.effectBlockMoving = true;
                while (Time.time < startTime + timeOfEffect)
                {
                    yield return new WaitForSeconds(frequencyOfEffect);
                }
                combatEnemy.effectBlockMoving = false;
            }
            else
            {
                CombatPlayer combatPlayer = target.transform.GetComponentInParent<CombatPlayer>();
                if (combatPlayer)
                {
                    Managers.Spellcasting.effectBlockMoving = true;
                    combatPlayer.effectBlockMoving = true;
                    while (Time.time < startTime + timeOfEffect)
                    {
                        yield return new WaitForSeconds(frequencyOfEffect);
                    }
                    combatPlayer.effectBlockMoving = false;
                    Managers.Spellcasting.effectBlockMoving = false;
                }
            }
            target.GetComponent<CombatCharacter>().RemoveEffect(this);
        }

        public override void ReleaseEffect(GameObject target)
        {
            startTime = Time.time;
            CombatCharacter combat = target.GetComponent<CombatCharacter>();
            combat.AddEffect(this);
            foreach (Effect effect in combat.activeEffects)
            {
                if (effect.gameObject.GetComponent<Wet>())
                {
                    Effect electric = Instantiate(electrocuted, target.transform);
                    electric.ReleaseEffect(target);
                    break;
                }
            }
        }
    }
}
