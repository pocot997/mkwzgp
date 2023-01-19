using System.Collections;
using UnityEngine;
using DVSN.GameManagment;

namespace DVSN.Player
{
    public class CombatPlayer : CombatCharacter
    {
        [SerializeField] private Transform orientation;

        int chosenSpell = -1;
        float chosenSpellCastTime = 0;
        bool isAlreadyCasting = false;
        bool isSpellReleased = false;

        void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            if (Managers.Spellcasting.isAbleToCast && !effectBlockCasting && !Managers.Spellcasting.effectBlockCasting)
            {
                if (!Managers.Spellcasting.isSpellReady && !Managers.Spellcasting.isCasting)
                {
                    ChooseSpell();
                    if (chosenSpell != -1)
                    {
                        isSpellReleased = false;
                        chosenSpellCastTime = Managers.Spellcasting.GetSpellCastingTime(chosenSpell);
                        Managers.Spellcasting.isCasting = true;
                    }
                }
                else if (!Managers.Spellcasting.isSpellReady && Managers.Spellcasting.isCasting && !isAlreadyCasting)
                {
                    StartCoroutine(CastSpell());
                }
                else if (Managers.Spellcasting.isSpellReady && !isSpellReleased)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        isSpellReleased = true;
                        SpellRelease();
                        Managers.Spellcasting.isSpellReady = false;
                        chosenSpell = -1;
                    }
                }
            }
        }

        void ChooseSpell()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                chosenSpell = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                chosenSpell = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                chosenSpell = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                chosenSpell = 3;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                chosenSpell = 4;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                chosenSpell = 5;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                chosenSpell = 6;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                chosenSpell = 7;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                chosenSpell = 8;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                chosenSpell = 9;
            }
        }

        IEnumerator CastSpell()
        {
            isAlreadyCasting = true;
            yield return new WaitForSeconds(chosenSpellCastTime);

            chosenSpellCastTime = 0;
            Managers.Spellcasting.isCasting = false;
            Managers.Spellcasting.isSpellReady = true;
            isAlreadyCasting = false;
        }

        public void SpellRelease()
        {
            Transform owner = orientation;
            Vector3 instantiatePosition = owner.position + owner.transform.forward;
            Quaternion instantiateRotation = owner.rotation;
            Managers.Spellcasting.ReleaseBasedOnType(chosenSpell, instantiatePosition, instantiateRotation, orientation, "Player");
        }
        public override void Die()
        {
            Debug.Log("You Die");
        }

        public override void ChangeHitPoints(float value)
        {
            value = value / 2;

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
}
