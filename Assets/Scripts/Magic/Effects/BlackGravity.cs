using System.Collections;
using UnityEngine;

public class BlackGravity : Effect
{
    [Header("Black Gravity Settings")]
    [SerializeField] private float radiusOfEffect;
    [SerializeField] private float force;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            RaycastHit[] hitTargets = Physics.SphereCastAll(transform.position, radiusOfEffect, transform.up);
            foreach (RaycastHit hit in hitTargets)
            {
                Rigidbody rg = hit.collider.transform.GetComponent<Rigidbody>();
                if (rg)
                {
                    rg.AddForce(-(hit.transform.position - transform.position) * force * Time.deltaTime * 60, ForceMode.Impulse);
                }
                else
                {
                    rg = hit.collider.transform.GetComponentInParent<Rigidbody>();
                    if (rg)
                    {
                        rg.AddForce(-(hit.transform.position - transform.position) * force * Time.deltaTime * 60, ForceMode.Impulse);
                    }
                }
            }
            yield return new WaitForSeconds(frequencyOfEffect);
        }
        Destroy(gameObject);
    }

    public override void ReleaseEffect(GameObject target)
    {
        startTime = Time.time;
        StartCoroutine(EffectCoroutine(target));
    }
}
