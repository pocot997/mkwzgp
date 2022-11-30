using System.Collections;
using UnityEngine;
using DVSN.GameManagment;
using DVSN.Player;

namespace DVSN.Spellcasting
{
    public class Electrocuted : Effect
    {
        public override IEnumerator EffectCoroutine(GameObject target)
        {
            CombatCharacter combatCharacter = target.transform.GetComponent<CombatEnemy>();
            if (combatCharacter)
            {
                combatCharacter.effectBlockCasting = true;
                while (Time.time < startTime + timeOfEffect)
                {
                    yield return new WaitForSeconds(frequencyOfEffect);
                }
                combatCharacter.effectBlockCasting = false;
            }
            else
            {
                combatCharacter = target.transform.GetComponentInParent<CombatPlayer>();
                if (combatCharacter)
                {
                    Managers.Spellcasting.effectBlockCasting = true;
                    combatCharacter.effectBlockCasting = true;
                    while (Time.time < startTime + timeOfEffect)
                    {
                        yield return new WaitForSeconds(frequencyOfEffect);
                    }
                    combatCharacter.effectBlockCasting = false;
                    Managers.Spellcasting.effectBlockCasting = false;
                }
            }
            target.GetComponent<CombatCharacter>().RemoveEffect(this);
        }

        public override void ReleaseEffect(GameObject target)
        {
            startTime = Time.time;

            target.GetComponent<CombatCharacter>().AddEffect(this); ;
        }
    }
}
