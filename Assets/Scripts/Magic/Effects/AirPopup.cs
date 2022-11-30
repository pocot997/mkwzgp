using System.Collections;
using UnityEngine;

public class AirPopup : Effect
{
    [Header("Air Popup Settings")]
    [SerializeField] private Effect airPlatfrom;
    [SerializeField] private float force;


    public override IEnumerator EffectCoroutine(GameObject target)
    {
        while (Time.time < startTime + timeOfEffect)
        {
            RaycastHit[] hitTargets = Physics.SphereCastAll(transform.position + Vector3.up * 3.5f, 1.5f, -transform.up);
            foreach (RaycastHit hit in hitTargets)
            {
                Rigidbody rg = hit.collider.transform.GetComponent<Rigidbody>();
                if (rg)
                {
                    rg.AddForce(Vector3.up * force * Time.deltaTime * 100, ForceMode.Impulse);
                }
                else
                {
                    rg = hit.collider.transform.GetComponentInParent<Rigidbody>();
                    if (rg)
                    {
                        rg.AddForce(Vector3.up * force * Time.deltaTime * 100, ForceMode.Impulse);
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

    private void OnTriggerExit(Collider other)
    {
        if(other.transform.GetComponent<CombatCharacter>())
        {
            Effect createdEffect = Instantiate(airPlatfrom, transform.position + new Vector3(0, transform.localScale.y, 0), transform.rotation);
            createdEffect.ReleaseEffect(other.gameObject);
        }
        else
        {
            if(other.transform.GetComponentInParent<CombatCharacter>())
            {
                Effect createdEffect = Instantiate(airPlatfrom, transform.position + new Vector3(0, transform.localScale.y, 0), transform.rotation);
                createdEffect.ReleaseEffect(other.gameObject);
            }
        }
    }
}
