using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DVSN.Plot;
using DVSN.GameManagment;

namespace DVSN.Player
{
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] Transform interactionBox;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                InteractionComponent interactionComponent = CatchObject<InteractionComponent>();

                if (interactionComponent != null)
                {
                    interactionComponent.Interact();
                }
            }
        }
        
        T CatchObject<T>()
        {
            Collider[] hitColliders = Physics.OverlapBox(interactionBox.position, interactionBox.localScale); //Physics.OverlapSphere(interactionSphere.position, interactionSphere.localScale.x);
            
            foreach (Collider hitCollider in hitColliders)
            {
                T target = hitCollider.GetComponent<T>();
                if (target != null)
                {
                    return target;
                }
            }

            return default(T);
        }
    }
}
