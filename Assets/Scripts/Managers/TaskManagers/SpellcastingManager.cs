using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace DVSN.Spellcasting
{
    public class SpellcastingManager : MonoBehaviour, IGameManager
    {
        public ManagerStatus status { get; private set; }

        [Header("Spells & Effects")]
        public List<GameObject> spellbook;

        [HideInInspector] public Vector3 startCastingPosition;
        [HideInInspector] public bool isSpellReady = false;
        [HideInInspector] public bool isCasting = false;
        [HideInInspector] public bool isAbleToCast = true;
        [HideInInspector] public bool effectBlockCasting = false;
        [HideInInspector] public bool effectBlockMoving = false;

        public void Startup()
        {
            status = ManagerStatus.Started;
        }

        public void ReleaseBasedOnType(int spellNumber, Vector3 instantiatePosition, Quaternion instantiateRotation, Transform owner, string caster)
        {
            Spell mySpell = spellbook[spellNumber].GetComponent<Spell>();
            GameObject newSpell;
            switch (mySpell.type)
            {
                case SpellType.POINT_CLICK:
                    RaycastHit hit;
                    if (Physics.Raycast(instantiatePosition, owner.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                    {
                        newSpell = Instantiate(spellbook[spellNumber], hit.point, instantiateRotation);
                        newSpell.GetComponent<PointClick>().hitPlace = hit;
                        newSpell.GetComponent<Spell>().caster = caster;
                        newSpell.GetComponent<Spell>().ReleaseSpell();
                    }
                    break;

                case SpellType.PROJECTILE:
                    newSpell = Instantiate(spellbook[spellNumber], instantiatePosition, instantiateRotation);
                    newSpell.GetComponent<Spell>().caster = caster;
                    newSpell.GetComponent<Spell>().ReleaseSpell();
                    break;

                case SpellType.INSTANT:
                    newSpell = Instantiate(spellbook[spellNumber], instantiatePosition, instantiateRotation);
                    newSpell.GetComponent<Spell>().caster = caster;
                    newSpell.GetComponent<Spell>().ReleaseSpell();
                    break;
            }
        }

        internal float GetSpellCastingTime(int spellNumber)
        {
            return spellbook[spellNumber].GetComponent<Spell>().castTime;
        }

        public void InstantiateSpell(int spellNumber, Transform caster)
        {
            Vector3 instantiatePosition = caster.position + caster.transform.forward;
            Quaternion instantiateRotation = caster.rotation;
            ReleaseBasedOnType(spellNumber, instantiatePosition, instantiateRotation, caster, caster.name);
        }

        public void InstantiateSpell(int spellNumber, string caster)
        {
            if (caster == "Player")
            {
                isSpellReady = true;
            }
            else
            {
                Transform owner = GameObject.Find(caster).transform;
                Vector3 instantiatePosition = owner.position + owner.transform.forward;
                Quaternion instantiateRotation = owner.rotation;
                ReleaseBasedOnType(spellNumber, instantiatePosition, instantiateRotation, owner, caster);
            }
        }
    }
}
