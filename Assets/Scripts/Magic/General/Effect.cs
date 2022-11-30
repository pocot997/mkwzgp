using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    FIRE = 0,
    WATER = 1,
    EARTH = 2,
    LIGHTNING = 3,
    WIND = 4
}

public abstract class Effect: MonoBehaviour
{
    [Header("Effect Settings")]
    public string name;
    public Element element;
    public float timeOfEffect;
    public float frequencyOfEffect;

    [HideInInspector] public float startTime;


    public void RefreshTime(float newStartTime)
    {
        startTime = newStartTime;
    }

    public abstract void ReleaseEffect(GameObject target);
    public abstract IEnumerator EffectCoroutine(GameObject target);
}

