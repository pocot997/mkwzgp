using System.Collections.Generic;
using UnityEngine;

public abstract class CombatCharacter : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float hitPoints = 100f;
    [SerializeField] private float maxHitPoints = 100f;
    [SerializeField] private float overheat = 0f;
    [SerializeField] private float maxOverheat = 100f;

    [Header("Effects")]
    public List<Effect> activeEffects;

    [HideInInspector] public float effectReduceDamage = 1f;
    [HideInInspector] public bool effectBlockCasting = false;
    [HideInInspector] public bool effectBlockMoving = false;

    internal float HitPoints { get => hitPoints; set => hitPoints = value; }

    public void ChangeHitPoints(float value)
    {
        if(value < 0)
        {
            value *= effectReduceDamage;
        }

        HitPoints += value;
        if(!CheckIsAlive())
        {
            Die();
        }
        else if (HitPoints > maxHitPoints)
        {
            HitPoints = maxHitPoints;
        }
    }

    public bool CheckIsAlive()
    {
        if (HitPoints <= 0)
            return false;
        return true;
    }

    public void ChangeOverheat(float value)
    {
        overheat += value;
        if (!CheckIsOverheating())
        {
            // add Overheat effect
        }
        else if (overheat > maxOverheat)
        {
            overheat = maxOverheat;
        }
    }

    public bool CheckIsOverheating()
    {
        if (overheat >= maxOverheat)
            return true;
        return false;
    }

    public void AddEffect(Effect effect)
    {
        foreach(Effect check in activeEffects)
        {
            if (effect.name == check.name)
            {
                activeEffects[activeEffects.IndexOf(check)].RefreshTime(Time.time);
                return;
            }
        }
        activeEffects.Add(effect);
        StartCoroutine(activeEffects[activeEffects.Count - 1].EffectCoroutine(gameObject));
    }

    public void RemoveEffect(Effect effect)
    {
        if (activeEffects.Contains(effect))
        {
            effect.startTime = 0f;
            activeEffects.Remove(effect);
            Destroy(effect.gameObject);
        }
    }

    public bool EffectExistance(Effect effect)
    {
        foreach (Effect check in activeEffects)
        {
            if (effect.name == check.name)
            {
                activeEffects[activeEffects.IndexOf(check)].RefreshTime(Time.time);
                return true;
            }
        }
        return false;
    }

    public abstract void Die();
}
